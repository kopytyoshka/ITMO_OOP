using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Services
{
    public interface IBackupsService
    {
        void AddJobObjectBackupJob(JobObject jobObject);
        void RemoveJobObjectBackupJob(JobObject jobObject);
        void RemoveRestorePointBackupJob(RestorePoint restorePoint);
        List<JobObject> GetJobObjects();
        List<RestorePoint> GetListRestorePoints();
        public void AddRestorePointBackupJob(RestorePoint restorePoint);
        void StartBackupJob(List<Storage> storages);
    }
}