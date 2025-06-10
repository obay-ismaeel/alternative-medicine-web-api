namespace AlternativeMedicine.App.Controllers.Dtos.Outgoing;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AttachmentDto Attachment{ get; set; }
}
