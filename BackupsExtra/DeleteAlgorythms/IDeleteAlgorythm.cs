using System;
using Backups.Entities;

namespace BackupsExtra.DeleteAlgorythms
{
    public interface IDeleteAlgorythm
    {
        BackupJob DeleteAlgorythm(BackupJob backupJob, int? amountSavedRestorePoints, DateTime? dateTime);
    }
}