using Dapper;
using Microsoft.Data.SqlClient;
using Module3WebApi.WebApi.Models;

namespace Module3WebApi.WebApi.Repositories
{
    public class Environment2DRepository : IEnvironment2DRepository
    {
        private readonly string sqlConnectionString;
        //private readonly IAuthenticationService authenticationService;

        public Environment2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
            //this.authenticationService = authenticationService;
        }

        public async Task<Environment2D> InsertAsync(Environment2D environment2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [Environment2D] (Id, Name, OwnerUserId, MaxLength, MaxHeight) VALUES (@Id, @Name, @OwnerUserId, @MaxLength, @MaxHeight)", environment2D);
                return environment2D;
            }
        }

        public async Task<Environment2D?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Environment2D>("SELECT * FROM [Environment2D] WHERE Id = @Id", new { id });
            }
        }
        public async Task<IEnumerable<Environment2D>> ReadAsync(string userId)
        {
            // Only return environments that belong to the logged in user
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Environment2D>("SELECT * FROM [Environment2D] WHERE OwnerUserId = @userId", new { userId });
            }
        }
        public async Task UpdateAsync(Environment2D environment2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Environment2D] SET " +
                                                 "Name = @Name, " +
                                                 "OwnerUserId = @OwnerUserId, " +
                                                 "MaxLength = @MaxLength, " +
                                                 "MaxHeight = @MaxHeight " +
                                                 "WHERE Id = @Id", environment2D);
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Environment2D] WHERE Id = @Id", new { id });
            }
        }
    }
}