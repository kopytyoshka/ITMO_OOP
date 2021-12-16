using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Archivator
{
    public class SingleStorage : IArchiveAlgorythm
    {
        public List<Storage> CreateArchive(List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();
            var storage = new Storage();

            foreach (var objects in jobObjects)
            {
                storage.JobObjects.Add(objects);
            }

            storages.Add(storage);

            return storages;
        }
    }
}