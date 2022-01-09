#nullable enable
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace BackupsExtra.Recovery
{
    public class OriginLoc : IRecovery
    {
        public void Recovery(BackupJob backupJob, DirectoryInfo? directory)
        {
            foreach (Storage storage in backupJob.RestorePoints[^1].Storages)
            {
                foreach (JobObject job in storage.JobObjects)
                {
                    ZipFile.ExtractToDirectory(job.File.DirectoryName, job.File.DirectoryName);
                }
            }
        }
    }
}