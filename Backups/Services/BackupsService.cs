using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public class BackupsService : IBackupsService
    {
        private BackupJob _backupJob = new ();

        public void AddJobObjectBackupJob(JobObject jobObject)
        {
            _backupJob.JobObjects.Add(jobObject);
        }

        public void RemoveJobObjectBackupJob(JobObject jobObject)
        {
            _backupJob.JobObjects.Remove(jobObject);
        }

        public List<JobObject> GetJobObjects()
        {
            return _backupJob.JobObjects;
        }

        public void AddRestorePointBackupJob(RestorePoint restorePoint)
        {
            _backupJob.RestorePoints.Add(restorePoint);
        }

        public void RemoveRestorePointBackupJob(RestorePoint restorePoint)
        {
            _backupJob.RestorePoints.Remove(restorePoint);
        }

        public List<RestorePoint> GetListRestorePoints()
        {
            return _backupJob.RestorePoints;
        }

        public void StartBackupJob(List<Storage> storages)
        {
            var restorePoint = new RestorePoint();
            AddRestorePointBackupJob(restorePoint);
            restorePoint.Storages.AddRange(storages);
        }
    }
}