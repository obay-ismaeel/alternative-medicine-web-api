using AlternativeMedicine.App.Controllers.Dtos;
using AlternativeMedicine.App.Controllers.Dtos.Generic;
using AlternativeMedicine.App.Controllers.Dtos.Incoming;
using AlternativeMedicine.App.Controllers.Dtos.Outgoing;
using AlternativeMedicine.App.DataAccess;
using AlternativeMedicine.App.Domain.Entities;
using AlternativeMedicine.App.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

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

        var dataDto = data.Select(p => _mapper.Map<ProductDto>(p)).ToList();

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

        return CreatedAtAction(nameof(Get), new { id = product.Id }, _mapper.Map<ProductDto>(product));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateUpdateProductDto productDto)
    {
        if (id != productDto.Id)
        {
            return BadRequest("Ids don't match");
        }

        var product = await _unitOfWork.Products.GetByIdAsync(id);

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

        foreach(var  attachment in product.Attachments)
        {
            _storage.Delete(attachment.Path);
        }

        _unitOfWork.Products.Delete(product);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

}
