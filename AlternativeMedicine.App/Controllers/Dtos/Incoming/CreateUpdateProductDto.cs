using AlternativeMedicine.App.Domain.Entities;

namespace AlternativeMedicine.App.Controllers.Dtos.Incoming;

public class CreateUpdateProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public int CategoryId { get; set; }
}
