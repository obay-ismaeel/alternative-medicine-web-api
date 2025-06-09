namespace AlternativeMedicine.App.Controllers.Dtos.Incoming;

public class CreateUpdateCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile Attachment { get; set; }
}
