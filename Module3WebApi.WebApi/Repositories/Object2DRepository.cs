using Dapper;
using Microsoft.Data.SqlClient;
using Module3WebApi.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module3WebApi.WebApi.Repositories
{
   public class Object2DRepository : IObject2DRepository
   {
       private readonly string sqlConnectionString;
   
       public Object2DRepository(string sqlConnectionString)
       {
           this.sqlConnectionString = sqlConnectionString;
       }
   
       public async Task<Object2D> InsertAsync(Object2D object2D)
       {
           using (var sqlConnection = new SqlConnection(sqlConnectionString))
           {
               await sqlConnection.ExecuteAsync("INSERT INTO [Object2D] (Id, EnvironmentId, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer) " +
                                                "VALUES (@Id, @EnvironmentId, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)", object2D);
               return object2D;
           }
       }
   
       public async Task<Object2D?> ReadAsync(Guid id)
       {
           using (var sqlConnection = new SqlConnection(sqlConnectionString))
           {
               return await sqlConnection.QuerySingleOrDefaultAsync<Object2D>("SELECT * FROM [Object2D] WHERE Id = @Id", new { id });
           }
       }
   
       public async Task<IEnumerable<Object2D>> ReadAllAsync(Guid environmentId)
       {
           using (var sqlConnection = new SqlConnection(sqlConnectionString))
           {
               return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D] WHERE EnvironmentId = @EnvironmentId", new { environmentId });
           }
       }
   
       public async Task UpdateAsync(Object2D object2D)
       {
           using (var sqlConnection = new SqlConnection(sqlConnectionString))
           {
               await sqlConnection.ExecuteAsync("UPDATE [Object2D] SET " +
                                                "EnvironmentId = @EnvironmentId, " +
                                                "PrefabId = @PrefabId, " +
                                                "PositionX = @PositionX, " +
                                                "PositionY = @PositionY, " +
                                                "ScaleX = @ScaleX, " +
                                                "ScaleY = @ScaleY, " +
                                                "RotationZ = @RotationZ, " +
                                                "SortingLayer = @SortingLayer " +
                                                "WHERE Id = @Id", object2D);
           }
       }
   
       public async Task DeleteAsync(Guid id)
       {
           using (var sqlConnection = new SqlConnection(sqlConnectionString))
           {
               await sqlConnection.ExecuteAsync("DELETE FROM [Object2D] WHERE Id = @Id", new { id });
           }
       }
   }
}
