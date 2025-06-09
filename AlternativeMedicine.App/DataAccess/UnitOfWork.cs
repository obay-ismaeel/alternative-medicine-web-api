using AlternativeMedicine.App.Domain.Entities;

namespace AlternativeMedicine.App.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IBaseRepository<Category> Categories { get; private set; }

    public IBaseRepository<Product> Products { get; private set; }

    public IBaseRepository<Attachment> Attachments { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Categories = new BaseRepository<Category>(context);
        Products = new BaseRepository<Product>(context);
        Attachments = new BaseRepository<Attachment>(context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}