using Backups.Entities;

namespace BackupsExtra.Merge
{
    public class Merge
    {
        public BackupJob MergeRules(BackupJob backupJob)
        {
            foreach (RestorePoint restore in backupJob.RestorePoints)
            {
                if (restore.Storages.Count == 1)
                    restore.Storages.Clear();
            }

            if (backupJob.RestorePoints[^1].Storages.Count != 0)
            {
                foreach (RestorePoint restore in backupJob.RestorePoints)
                {
                    if (restore == backupJob.RestorePoints[^1])
                        break;
                    restore.Storages.Clear();
                }
            }

            if (backupJob.RestorePoints[^1].Storages.Count == 0)
            {
                foreach (RestorePoint restore in backupJob.RestorePoints)
                {
                    if (restore == backupJob.RestorePoints[^1])
                        break;
                    foreach (Storage storage in restore.Storages) backupJob.RestorePoints[^1].Storages.Add(storage);
                    restore.Storages.Clear();
                }
            }

            return backupJob;
        }
    }
}