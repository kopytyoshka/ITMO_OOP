#nullable enable
using System.IO;
using Backups.Entities;

namespace BackupsExtra.Recovery
{
    public interface IRecovery
    {
        void Recovery(BackupJob backupjob, DirectoryInfo? directory);
    }
}