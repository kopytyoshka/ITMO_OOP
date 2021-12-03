using System.Collections.Generic;

namespace Backups.Entities
{
    public class Storage
    {
        private static uint _id;
        public Storage()
        {
            Id = ++_id;
            JobObjects = new List<JobObject>();
        }

        public uint Id { get; }
        public List<JobObject> JobObjects { get; }
    }
}