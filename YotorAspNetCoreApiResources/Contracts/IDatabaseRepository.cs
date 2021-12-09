using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IDatabaseRepository
    {
        public Task CreateBackupAsync(string path);
        public Task InsertBackupToDbAsync(string path);
        public Task RestoreDatabaseBySomeBackupAsync(Backup backup);
        public Task<Backup> GetLastBackupAsync();
        public Task<Backup> GetBackupByIdAsync(int id);


    }
}
