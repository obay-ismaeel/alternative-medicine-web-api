namespace AlternativeMedicine.App.Domain.Entities;

public class Attachment
{
    public int Id { get; set; }

    public string Path { get; set; }
    
    public int? ProductId { get; set; }
    public virtual Product Product { get; set; }
}
