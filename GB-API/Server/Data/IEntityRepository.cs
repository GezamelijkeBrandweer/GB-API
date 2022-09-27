namespace GB_API.Server.Data;

public interface IEntityRepository<T>
{
    public List<T> FindAll();
    public void Save(T t);
    public void DeleteById(long id);
    public T? GetById(long id);

}