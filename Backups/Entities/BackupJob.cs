using System.Collections.Generic;

namespace Backups.Entities
{
    public class BackupJob
    {
        public BackupJob()
        {
            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
        }

        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }
    }
}