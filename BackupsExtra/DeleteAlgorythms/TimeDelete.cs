using System;
using System.Data;
using Backups.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.DeleteAlgorythms
{
    public class TimeDelete : IDeleteAlgorythm
    {
        public BackupJob DeleteAlgorythm(BackupJob backupJob, int? amountSavedRestorePoints, DateTime? dateTime)
        {
            if (backupJob.RestorePoints[^1].DateTime <= dateTime)
                throw new BackupExtraException("Operation can't be done");
            for (int i = 0; i < backupJob.RestorePoints.Count; i++)
            {
                if (backupJob.RestorePoints[i].DateTime > dateTime)
                    backupJob.RestorePoints.Remove(backupJob.RestorePoints[i]);
            }

            return backupJob;
        }
    }
}