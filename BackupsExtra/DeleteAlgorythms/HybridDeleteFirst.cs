using System;
using Backups.Entities;

namespace BackupsExtra.DeleteAlgorythms
{
    public class HybridDeleteFirst : IDeleteAlgorythm
    {
        private AmountDelete _amountDelete = new AmountDelete();
        private TimeDelete _timeDelete = new TimeDelete();
        public BackupJob DeleteAlgorythm(BackupJob backupJob, int? amountSavedRestorePoints, DateTime? dateTime)
        {
            _amountDelete.DeleteAlgorythm(backupJob, amountSavedRestorePoints, null);
            _timeDelete.DeleteAlgorythm(backupJob, null, dateTime);
            return backupJob;
        }
    }
}