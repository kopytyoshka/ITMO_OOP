using System.IO;

namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(FileInfo file)
        {
            File = file;
        }

        public FileInfo File { get; }
    }
}