using AlternativeMedicine.App.Domain.Entities;

namespace AlternativeMedicine.App.Controllers.Dtos.Outgoing;

public class AttachmentDto
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string Path { get; set; }

}
