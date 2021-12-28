using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Archivator;
using Backups.Entities;
using Backups.Repositories;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTests
    {
        private IBackupsService _backup;
        
        [SetUp]
        public void Setup()
        {
            _backup = new BackupsService();
        }

        [Test]
        public void TwoRestorePointsAndThreeStorages()
        {
            JobObject jobObject = new JobObject(new FileInfo($@"../../../../FileA"));
            JobObject jobObject2 = new JobObject(new FileInfo($@"../../../../FileB"));
            _backup.AddJobObjectBackupJob(jobObject);
            _backup.AddJobObjectBackupJob(jobObject2);
            IRepository repository = new VirtualRepo(new DirectoryInfo($@"../../../Directory"));
            List<Storage> storages = repository.SaveStoragesRepository(new SplitStorage(), _backup.GetJobObjects(), 1);
            _backup.StartBackupJob(storages);
            _backup.RemoveJobObjectBackupJob(jobObject);
            List<Storage> finalStorages = repository.SaveStoragesRepository(new SplitStorage(), _backup.GetJobObjects(), 1);
            _backup.StartBackupJob(finalStorages);
            int i = _backup.GetListRestorePoints().Sum(restorePoint => restorePoint.Storages.Count);
            Assert.AreEqual(4, _backup.GetListRestorePoints().Count);
            Assert.AreEqual(3, i);
        }

    }
}