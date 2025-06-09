using System.ComponentModel.DataAnnotations;

namespace AlternativeMedicine.App.Domain.Entities;

public class Product    
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public int Rating {  get; set; }
    public ICollection<Attachment> Attachments { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
