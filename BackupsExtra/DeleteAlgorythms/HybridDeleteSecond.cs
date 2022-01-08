using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.DeleteAlgorythms
{
    public class HybridDeleteSecond : IDeleteAlgorythm
    {
        private List<RestorePoint> _restorePointsToDeleteByTime = new List<RestorePoint>();
        private List<RestorePoint> _restorePointsToDeleteByAmount = new List<RestorePoint>();
        public BackupJob DeleteAlgorythm(BackupJob backupJob, int? amountSavedRestorePoints, DateTime? dateTime)
        {
            int? amountRestorePointsToDelete = backupJob.RestorePoints.Count - amountSavedRestorePoints;
            if (amountRestorePointsToDelete <= 0)
                throw new BackupExtraException("Operation can't be done");
            for (int i = 0; i < amountRestorePointsToDelete; i++)
            {
                _restorePointsToDeleteByAmount.Add(backupJob.RestorePoints[i]);
            }

            if (backupJob.RestorePoints[^1].DateTime <= dateTime)
                throw new BackupExtraException("Operation can't be done");
            foreach (RestorePoint restore in backupJob.RestorePoints)
            {
                if (restore.DateTime > dateTime)
                    _restorePointsToDeleteByTime.Add(restore);
            }

            foreach (RestorePoint restore in backupJob.RestorePoints)
            {
                if (_restorePointsToDeleteByAmount.Contains(restore) && _restorePointsToDeleteByTime.Contains(restore))
                    backupJob.RestorePoints.Remove(restore);
            }

            return backupJob;
        }
    }
}