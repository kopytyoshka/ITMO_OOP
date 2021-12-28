using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.DeleteAlgorythms
{
    public class AmountDelete : IAlgorythm
    {
        public BackupJob DeleteAlgorythm(BackupJob backupJob, int? amountSavedRestorePoints, DateTime? dateTime)
        {
            int? amountRestorePointsToDelete = backupJob.RestorePoints.Count - amountSavedRestorePoints;
            if (amountRestorePointsToDelete <= 0)
                throw new BackupExtraException("Operation can't be done");
            for (int i = 0; i < amountRestorePointsToDelete; i++)
            {
                backupJob.RestorePoints.Remove(backupJob.RestorePoints[i]);
            }

            return backupJob;
        }
    }
}