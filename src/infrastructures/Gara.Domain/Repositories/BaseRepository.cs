using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Gara.Domain.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly IConfiguration _configuration;

        // private readonly IHttpContextAccessor _httpContextAccessor;



        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<dynamic>> ExecuteSelectCommandAsync(string sql, object parameters, string connectionString)
        {
            await using var conn = new SqlConnection(connectionString);

            conn.Open();
            var result = await conn.QueryAsync(sql, parameters);
            conn.Close();

            return result.ToList();
        }

        public async Task<int> ExecuteUpdateCommandAsync(string sql, object parameters, string connectionString)
        {
            await using var conn = new SqlConnection(connectionString);

            conn.Open();
            var result = await conn.ExecuteAsync(sql, parameters);
            conn.Close();

            return result;
        }
    }
}
