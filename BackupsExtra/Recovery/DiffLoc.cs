#nullable enable
using System.IO;
using Backups.Entities;
using Ionic.Zip;
using ZipFile = System.IO.Compression.ZipFile;

namespace BackupsExtra.Recovery
{
    public class DiffLoc
    {
        public void Recovery(BackupJob backupJob, DirectoryInfo? directory)
        {
            foreach (Storage storage in backupJob.RestorePoints[^1].Storages)
            {
                foreach (JobObject job in storage.JobObjects)
                {
                    ZipFile.ExtractToDirectory(job.File.DirectoryName, directory?.Name);
                }
            }
        }
    }
}