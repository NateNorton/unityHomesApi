namespace HomesApi.Data.Repositories;

public interface IGeneralRepository
{
    bool SaveChanges();
    void AddEntity<T>(T entity)
        where T : class;
}
