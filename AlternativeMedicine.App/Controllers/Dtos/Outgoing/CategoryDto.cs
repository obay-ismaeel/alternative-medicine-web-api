namespace AlternativeMedicine.App.Controllers.Dtos.Outgoing;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath{ get; set; }
    public int? ParentId { get; set; }
    public string NameArabic { get; set; }
    public string Color { get; set; }
}
