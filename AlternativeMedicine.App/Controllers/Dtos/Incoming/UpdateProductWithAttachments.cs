using System.ComponentModel.DataAnnotations;

namespace AlternativeMedicine.App.Controllers.Dtos.Incoming;

public class UpdateProductWithAttachments
{
    [Required]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Price { get; set; }
    public int? CategoryId { get; set; }
    public List<IFormFile>? Images { get; set; }
}
