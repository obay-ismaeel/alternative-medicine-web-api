using AlternativeMedicine.App.Domain.Entities;

namespace AlternativeMedicine.App.Controllers.Dtos.Incoming;

public class CreateAttachmentDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public IFormFile Attachment { get; set; }
}
