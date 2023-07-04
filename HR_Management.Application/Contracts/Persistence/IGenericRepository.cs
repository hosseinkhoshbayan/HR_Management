using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<T> Get(long id);

    Task<IReadOnlyList<T>> GetAll();

    Task<bool> Exist(long id);

    Task<T> Add(T entity);

    Task Update(T entity);

    Task Delete(T entity);
}