using Module3WebApi.WebApi.Models;

namespace Module3WebApi.WebApi.Repositories
{
    public interface IEnvironment2DRepository
    {
        Task<Environment2D> InsertAsync(Environment2D environment2D);
        Task<Environment2D?> ReadAsync(Guid id);
        Task<IEnumerable<Environment2D>> ReadAsync(string userId);
        Task UpdateAsync(Environment2D environment2D);
        Task DeleteAsync(Guid id);
    }
}