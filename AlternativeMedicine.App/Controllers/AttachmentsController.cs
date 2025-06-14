using AlternativeMedicine.App.Controllers.Dtos.Generic;
using AlternativeMedicine.App.Controllers.Dtos.Incoming;
using AlternativeMedicine.App.Controllers.Dtos.Outgoing;
using AlternativeMedicine.App.DataAccess;
using AlternativeMedicine.App.Domain.Entities;
using AlternativeMedicine.App.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace AlternativeMedicine.App.Controllers;

public class AttachmentsController : BaseController
{
    public AttachmentsController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService storage) : base(unitOfWork, mapper, storage)
    {
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var attachment = await _unitOfWork.Attachments.FindAsync(c => c.Id == id);

        if (attachment == null)
        {
            return NotFound();
        }

        var result = new Result<AttachmentDto>
        {
            Data = _mapper.Map<AttachmentDto>(attachment),
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAttachmentDto attachmentDto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(attachmentDto.ProductId);

        if(product is null)
        {
            return BadRequest("There is no such product");
        }

        attachmentDto.Id = 0;

        var attachment = _mapper.Map<Attachment>(attachmentDto);

        await _unitOfWork.Attachments.AddAsync(attachment);

        var path = await _storage.StoreAsync(attachmentDto.Attachment);

        attachment.Path = path;

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = attachment.Id }, _mapper.Map<AttachmentDto>(attachment));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var attachment = await _unitOfWork.Attachments.GetByIdAsync(id);

        if (attachment is null)
        {
            return NotFound();
        }

        _unitOfWork.Attachments.Delete(attachment);

        await _unitOfWork.CompleteAsync();

        _storage.Delete(attachment.Path);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(List<int> ids)
    {
        foreach(var id in ids)
        {
            var attachment = await _unitOfWork.Attachments.GetByIdAsync(id);

            if (attachment is null)
                continue;

            _unitOfWork.Attachments.Delete(attachment);

            await _unitOfWork.CompleteAsync();

            _storage.Delete(attachment.Path);
        }

        return NoContent();
    }
}
