using System.Drawing;

namespace AlternativeMedicine.App.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameArabic { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public string ImagePath { get; set; }
    public int? ParentId { get; set; }
    public ICollection<Product> Products { get; set; }
}
