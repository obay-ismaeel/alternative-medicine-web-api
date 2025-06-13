using AlternativeMedicine.App.Controllers.Dtos;
using AlternativeMedicine.App.Controllers.Dtos.Generic;
using AlternativeMedicine.App.Controllers.Dtos.Incoming;
using AlternativeMedicine.App.Controllers.Dtos.Outgoing;
using AlternativeMedicine.App.DataAccess;
using AlternativeMedicine.App.Domain.Entities;
using AlternativeMedicine.App.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlternativeMedicine.App.Controllers;

public class CategoriesController : BaseController
{
    public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService storage) : base(unitOfWork, mapper, storage)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var data = (await _unitOfWork.Categories.Paginate(pageNumber, pageSize))
            .ToList();

        var dataDto = data.Select(c => _mapper.Map<CategoryDto>(c)).ToList();

        var result = new PageResult<CategoryDto>
        {
            Data = dataDto,
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = dataDto.Count,
            TotalCount = await _unitOfWork.Categories.CountAsync()
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var category = await _unitOfWork.Categories.FindAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        var result = new Result<CategoryDto>
        {
            Data = _mapper.Map<CategoryDto>(category),
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUpdateCategoryDto categoryDto)
    {
        categoryDto.Id = 0;

        var category = _mapper.Map<Category>(categoryDto);

        await _unitOfWork.Categories.AddAsync(category);

        var path = await _storage.StoreAsync(categoryDto.Image);

        category.ImagePath = path;

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = category.Id }, _mapper.Map<CategoryDto>(category));
    }

    [HttpPut]
    public async Task<IActionResult> Put(CreateUpdateCategoryDto categoryDto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(categoryDto.Id);

        if (category is null)
        {
            return NotFound();
        }

        _mapper.Map(categoryDto, category);

        if(categoryDto.Image is not null)
        {
            var newPath = await _storage.StoreAsync(categoryDto.Image);
            
            _storage.Delete(category.ImagePath);

            category.ImagePath = newPath;
        }

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        _unitOfWork.Categories.Delete(category);

        await _unitOfWork.CompleteAsync();

        _storage.Delete(category.ImagePath);

        return NoContent();
    }

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetProducts(int id, int pageNumber = 1, int pageSize = 10)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        var data = await _unitOfWork.Products.FindAllAsync(p => p.CategoryId == id, pageNumber, pageSize);

        var dataDto = data.Select(p => _mapper.Map<ProductDto>(p)).ToList();

        var result = new PageResult<ProductDto>
        {
            Data = dataDto,
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = dataDto.Count(),
            TotalCount = await _unitOfWork.Products.CountAsync()
        };

        return Ok(result);
    }
}
