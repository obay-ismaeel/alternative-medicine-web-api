﻿using System.ComponentModel.DataAnnotations;

namespace AlternativeMedicine.App.Domain.Entities;

public class Product    
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
