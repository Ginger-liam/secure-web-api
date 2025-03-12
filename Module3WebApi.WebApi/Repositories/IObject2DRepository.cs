using Module3WebApi.WebApi.Models;

namespace Module3WebApi.WebApi.Repositories
{
    public interface IObject2DRepository
    {
        Task<Object2D> InsertAsync(Object2D object2D);
        Task<Object2D?> ReadAsync(Guid id);
        Task<IEnumerable<Object2D>> ReadAllAsync(Guid environmentId);
        Task UpdateAsync(Object2D object2D);
        Task DeleteAsync(Guid id);
    }
}