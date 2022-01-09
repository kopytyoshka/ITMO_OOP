using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public class BackupsService : IBackupsService
    {
        public BackupJob BackupJob { get; } = new ();

        public void AddJobObjectBackupJob(JobObject jobObject)
        {
            BackupJob.JobObjects.Add(jobObject);
        }

        public void RemoveJobObjectBackupJob(JobObject jobObject)
        {
            BackupJob.JobObjects.Remove(jobObject);
        }

        public List<JobObject> GetJobObjects()
        {
            return BackupJob.JobObjects;
        }

        public void AddRestorePointBackupJob(RestorePoint restorePoint)
        {
            BackupJob.RestorePoints.Add(restorePoint);
        }

        public void RemoveRestorePointBackupJob(RestorePoint restorePoint)
        {
            BackupJob.RestorePoints.Remove(restorePoint);
        }

        public List<RestorePoint> GetListRestorePoints()
        {
            return BackupJob.RestorePoints;
        }

        public void StartBackupJob(List<Storage> storages)
        {
            var restorePoint = new RestorePoint();
            AddRestorePointBackupJob(restorePoint);
            var restorePoint2 = new RestorePoint();
            AddRestorePointBackupJob(restorePoint2);
            restorePoint.Storages.AddRange(storages);
        }
    }
}