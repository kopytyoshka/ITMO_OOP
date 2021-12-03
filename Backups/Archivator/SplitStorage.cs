using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Archivator
{
    public class SplitStorage : IArchiveAlgorythm
    {
        public List<Storage> CreateArchive(List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();

            foreach (var objects in jobObjects)
            {
                var storage = new Storage();
                storage.JobObjects.Add(objects);
                storages.Add(storage);
            }

            return storages;
        }
    }
}