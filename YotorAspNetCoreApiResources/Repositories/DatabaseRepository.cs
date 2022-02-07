using Dapper;
using System.Data;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;
using YotorContext.Context;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly DapperContext _dapperContext;
        public DatabaseRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task CreateBackupAsync(string path)
        {
            var query = $"BACKUP DATABASE YotorDb TO DISK = @path";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { path});
            }
        }
        public async Task InsertBackupToDbAsync(string path)
        {
            var query = "INSERT INTO [Backup] (path) values (@path);";
            
            var parameters = new DynamicParameters();
            parameters.Add("path", path, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task RestoreDatabaseBySomeBackupAsync(Backup backup)
        {
            var query = $"use YotorDb alter database YotorDb set single_user with rollback immediate use master RESTORE DATABASE YotorDb FROM DISK = @path WITH REPLACE,RECOVERY ";
            var parameters = new DynamicParameters();
            parameters.Add("path", backup.Path, DbType.String);
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<Backup> GetLastBackupAsync()
        {
            var query = "SELECT * FROM [Backup] WHERE backup_id=(SELECT max(backup_id) FROM [Backup])";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Backup>(query);
            }
        }
        public async Task<Backup> GetBackupByIdAsync(int id)
        {
            var query = "Select * from [Backup] where backup_id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Backup>(query, new { id });
            }
        }
    }
}
