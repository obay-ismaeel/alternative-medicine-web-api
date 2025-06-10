using AlternativeMedicine.App.Domain.Entities;

namespace AlternativeMedicine.App.Controllers.Dtos.Incoming;

public class CreateUpdateAttachmentDto
{
    public int Id { get; set; }

    public IFormFile Attachment { get; set; }
}
