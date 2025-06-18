using AlternativeMedicine.App.DataAccess;
using AlternativeMedicine.App.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlternativeMedicine.App.Controllers;

public class CurrenciesController : BaseController
{
    public CurrenciesController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService storage) : base(unitOfWork, mapper, storage)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Currencies.FindAsync(x => true));
    }

    [HttpPut]
    public async Task<IActionResult> Put(double rate)
    {
        if (rate <= 0)
            return BadRequest("Invalid value");

        var currency = await _unitOfWork.Currencies.FindAsync(x => true);

        currency.Rate = rate;

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
