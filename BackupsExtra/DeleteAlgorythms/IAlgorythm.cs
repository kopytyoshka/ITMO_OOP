using System;
using Backups.Entities;

namespace BackupsExtra.DeleteAlgorythms
{
    public interface IAlgorythm
    {
        BackupJob DeleteAlgorythm(BackupJob backupJob, int? amountSavedRestorePoints, DateTime? dateTime);
    }
}