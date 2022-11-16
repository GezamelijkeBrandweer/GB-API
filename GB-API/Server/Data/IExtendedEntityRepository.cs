namespace GB_API.Server.Data;

public interface IExtendedEntityRepository<T>: IEntityRepository<T>
{
    public T? GetByName(string name);
}