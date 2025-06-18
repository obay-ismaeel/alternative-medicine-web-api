using AlternativeMedicine.App.Domain.Entities;

namespace AlternativeMedicine.App.Controllers.Dtos.Outgoing;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public string SyrianPoundPrice { get; set; }
    public int CategoryId { get; set; }
    public ICollection<AttachmentDto> Attachments { get; set; }
}
