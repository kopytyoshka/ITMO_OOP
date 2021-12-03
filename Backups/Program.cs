using System.Collections.Generic;
using System.IO;
using Backups.Archivator;
using Backups.Entities;
using Backups.Repositories;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            BackupsService backupsService = new BackupsService();
            JobObject jobObject = new JobObject(new FileInfo($@"/home/kopytyoshka/RiderProjects/kopytyoshka/Backups/Files/FileA"));
            JobObject jobObject2 = new JobObject(new FileInfo($@"/home/kopytyoshka/RiderProjects/kopytyoshka/Backups/Files/FileB"));
            backupsService.AddJobObjectBackupJob(jobObject);
            backupsService.AddJobObjectBackupJob(jobObject2);
            IRepository repository = new LocalRepo(new DirectoryInfo($@"/home/kopytyoshka/RiderProjects/kopytyoshka/Backups/Directory"));
            List<Storage> storages = repository.SaveStoragesRepository(new SingleStorage(), backupsService.GetJobObjects(), 1);
            backupsService.StartBackupJob(storages);
        }
    }
}
