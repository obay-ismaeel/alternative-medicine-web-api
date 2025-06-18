using AlternativeMedicine.App.Domain.Entities;

namespace AlternativeMedicine.App.DataAccess;

public interface IUnitOfWork
{
    IBaseRepository<Category> Categories { get; }
    IBaseRepository<Product> Products { get; }
    IBaseRepository<Attachment> Attachments{ get; }
    IBaseRepository<Currency> Currencies{ get; }
    Task<int> CompleteAsync();
    void Dispose();
}