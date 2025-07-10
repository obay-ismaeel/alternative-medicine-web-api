using AlternativeMedicine.App.Controllers.Dtos.Generic;
using AlternativeMedicine.App.Controllers.Dtos.Incoming;
using AlternativeMedicine.App.Controllers.Dtos.Outgoing;
using AlternativeMedicine.App.DataAccess;
using AlternativeMedicine.App.Domain.Entities;
using AlternativeMedicine.App.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace AlternativeMedicine.App.Controllers;

public class ProductsController : BaseController
{
    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService storage) : base(unitOfWork, mapper, storage)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10, string searchQuery = "")
    {
        Expression<Func<Product, bool>> criteria = (p) => (EF.Functions.Like(p.Name, $"%{searchQuery}%") || EF.Functions.Like(p.Description, $"%{searchQuery}%"));

        var data = string.IsNullOrWhiteSpace(searchQuery) ? await _unitOfWork.Products.Paginate(pageNumber, pageSize, ["Attachments"])
            : await _unitOfWork.Products.FindAllAsync(criteria, pageNumber, pageSize, ["Attachments"]);

        var rate = (await _unitOfWork.Currencies.FindAsync(x => true)).Rate;

        var dataDto = data.Select(p => _mapper.Map<ProductDto>(p)).Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name, 
            Description = p.Description, 
            CategoryId = p.CategoryId, 
            Price = p.Price, 
            SyrianPoundPrice = $"{rate * Convert.ToDouble(p.Price)}",
        }).ToList();

        var result = new PageResult<ProductDto>
        {
            Data = dataDto,
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = dataDto.Count,
            TotalCount = await _unitOfWork.Products.CountAsync()
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _unitOfWork.Products.FindAsync(p => p.Id == id, ["Attachments"]);

        if (product == null)
        {
            return NotFound();
        }

        var result = new Result<ProductDto>
        {
            Data = _mapper.Map<ProductDto>(product)
        };

        var rate = (await _unitOfWork.Currencies.FindAsync(x => true)).Rate;

        result.Data.SyrianPoundPrice = $"{Convert.ToDouble(product.Price) * rate}";

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUpdateProductDto productDto)
    {
        productDto.Id = 0;

        var product = _mapper.Map<Product>(productDto);

        await _unitOfWork.Products.AddAsync(product);

        await _unitOfWork.CompleteAsync();

        foreach (var image in productDto.Images)
        {
            var path = await _storage.StoreAsync(image);

            var attachment = new Attachment { ProductId = product.Id, Path = path };

            product.Attachments.Add(attachment);
        }

        await _unitOfWork.CompleteAsync();

        var rate = (await _unitOfWork.Currencies.FindAsync(x => true)).Rate;

        var returnDto = _mapper.Map<ProductDto>(product);

        returnDto.SyrianPoundPrice = $"{rate * Convert.ToDouble(returnDto.Price)}";

        return CreatedAtAction(nameof(Get), new { id = product.Id }, returnDto);
    }

    [HttpPut]
    public async Task<IActionResult> Put(CreateUpdateProductDto productDto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(productDto.Id);

        if (product is null)
        {
            return NotFound();
        }

        _mapper.Map(productDto, product);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _unitOfWork.Products.FindAsync(p => p.Id == id, ["Attachments"]);

        if (product is null)
        {
            return NotFound();
        }

        foreach (var attachment in product.Attachments)
        {
            _storage.Delete(attachment.Path);
        }

        _unitOfWork.Products.Delete(product);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpPost(nameof(UpdateWithAttachments))]
    public async Task<IActionResult> UpdateWithAttachments(UpdateProductWithAttachments input, [FromServices] IFileComparerService fileComparerService)
    {
        var product = await _unitOfWork.Products.FindAsync(p => p.Id == input.Id, ["Attachments"]);
        if (product is null)
            return NotFound();

        if (string.IsNullOrEmpty(input.Name) &&
            string.IsNullOrEmpty(input.Description) &&
            string.IsNullOrEmpty(input.Price) &&
            input.Images?.Count == 0)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(input.Name) is false)
            product.Name = input.Name!;

        if (string.IsNullOrEmpty(input.Description) is false)
            product.Description = input.Description!;

        if (string.IsNullOrEmpty(input.Price) is false)
            product.Price = input.Price!;

        if (input.Images?.Count > 0)
        {
            // Convert new images to bytes for comparison
            var newImagesData = new List<byte[]>();
            foreach (var image in input.Images!)
            {
                var bytes = await GetBytesFromIFormFileAsync(image);
                if (bytes is null) return BadRequest("Invalid image file");
                newImagesData.Add(bytes);
            }

            // Categorize attachments
            var attachmentsToRemove = new List<Attachment>();
            var attachmentsToKeep = new List<Attachment>();
            var newImagesToAdd = new List<IFormFile>(input.Images);

            foreach (var attachment in product.Attachments)
            {
                var existingImageBytes = _storage.GetImageBytes(attachment.Path);
                if (existingImageBytes is null)
                {
                    attachmentsToRemove.Add(attachment); // Invalid file, mark for removal
                    continue;
                }

                bool imageExistsInNew = false;

                // Check if this existing image exists in the new set
                foreach (var newImageBytes in newImagesData)
                {
                    if (fileComparerService.AreImagesIdentical(newImageBytes, existingImageBytes))
                    {
                        imageExistsInNew = true;
                        break;
                    }
                }

                if (imageExistsInNew)
                {
                    attachmentsToKeep.Add(attachment);

                    // Remove from new images (since it's already existing)
                    var index = newImagesData.FindIndex(b => fileComparerService.AreImagesIdentical(b, existingImageBytes));
                    if (index >= 0)
                    {
                        newImagesToAdd.RemoveAt(index);
                        newImagesData.RemoveAt(index);
                    }
                }
                else
                {
                    attachmentsToRemove.Add(attachment);
                }
            }

            // Process deletions
            foreach (var attachment in attachmentsToRemove)
            {
                _storage.Delete(attachment.Path);
                product.Attachments.Remove(attachment);
            }

            // Process additions
            foreach (var newImage in newImagesToAdd)
            {
                var path = await _storage.StoreAsync(newImage);

                var attachment = new Attachment { ProductId = product.Id, Path = path };

                product.Attachments.Add(attachment);
            }
        }


        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    private async Task<byte[]?> GetBytesFromIFormFileAsync(IFormFile file)
    {
        if (file is null || file.Length is 0)
            return null;

        using var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }
}
