using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Models;
using YotorContext.Models;

namespace YotorAspNetCoreApiResources.Contracts
{
    public interface IDatabaseRepository
    {
        Task CreateBackupAsync(string path);
        Task InsertBackupToDbAsync(string path);
        Task RestoreDatabaseBySomeBackupAsync(Backup backup);
        Task<Backup> GetLastBackupAsync();
        Task<Backup> GetBackupByIdAsync(int id);
    }
}
