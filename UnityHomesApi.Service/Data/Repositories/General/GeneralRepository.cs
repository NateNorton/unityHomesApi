namespace HomesApi.Data.Repositories;

public class GeneralRepository : IGeneralRepository
{
    private readonly HomesDbContext _context;

    public GeneralRepository(HomesDbContext context)
    {
        _context = context;
    }

    public void AddEntity<T>(T entity)
        where T : class
    {
        if (entity != null)
            _context.Set<T>().Add(entity);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }
}
