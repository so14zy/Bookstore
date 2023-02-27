namespace Bookstore.DAL.Repositories;
interface IRepository<T>
{
    void add(T model);
    Task<T> getById(int id);
    Task<List<T>> getAll();
    void update(T model);
    void delete(int id);
}