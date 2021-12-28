using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Backups.Archivator;
using Backups.Entities;
using Backups.Repositories;
using Backups.Services;
using BackupsExtra.DeleteAlgorythms;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BackupsExtra.Tests
{
    public class BackupExtraTest
    {

        private BackupsService _backupsService;
        
        [SetUp]
        public void Setup()
        {
            _backupsService = new BackupsService();
        }
        
        [Test]
        
        public void DeletedOneRestorePointByAmount()
        {
            BackupsService backupsService = new BackupsService();
            BackupJob backupJob = backupsService.BackupJob;
            JobObject jobObject = new JobObject(new FileInfo($@"../../../../FileA"));
            JobObject jobObject2 = new JobObject(new FileInfo($@"../../../../FileB"));
            backupsService.AddJobObjectBackupJob(jobObject);
            backupsService.AddJobObjectBackupJob(jobObject2);
            IRepository repository = new VirtualRepo(new DirectoryInfo($@"../../../Directory"));
            List<Storage> storages = repository.SaveStoragesRepository(new SplitStorage(), backupsService.GetJobObjects(), 1);
            backupsService.StartBackupJob(storages);
            new AmountDelete().DeleteAlgorythm(backupJob, 1, null);
            Assert.AreEqual(backupJob.RestorePoints.Count, 1);
        }
        
        [Test]
        
        public void MergeRestorePointsDeletedOld()
        {
            BackupsService backupsService1 = new BackupsService();
            BackupsService backupsService2 = new BackupsService();
            var merge = new Merge.Merge();
            BackupJob backupJob1 = backupsService1.BackupJob;
            JobObject jobObject1 = new JobObject(new FileInfo($@"../../../../FileA"));
            JobObject jobObject2 = new JobObject(new FileInfo($@"../../../../FileB"));
            backupsService1.AddJobObjectBackupJob(jobObject1);
            backupsService1.AddJobObjectBackupJob(jobObject2);
            IRepository repository1 = new VirtualRepo(new DirectoryInfo($@"../../../Directory"));
            IRepository repository2 = new VirtualRepo(new DirectoryInfo($@"../../../Directory"));
            List<Storage> storages1 = repository1.SaveStoragesRepository(new SplitStorage(), backupsService1.GetJobObjects(), 1);
            List<Storage> storages2 = repository1.SaveStoragesRepository(new SplitStorage(), backupsService1.GetJobObjects(), 2);
            backupsService1.StartBackupJob(storages1);
            backupsService1.StartBackupJob(storages2);
            merge.MergeRules(backupJob1);
            Assert.AreEqual(backupJob1.RestorePoints.Count, 4);

        }
        
    }
}