using System.Collections.Generic;
using System.IO;
using Backups.Archivator;
using Backups.Entities;

namespace Backups.Repositories
{
    public class VirtualRepo : IRepository
    {
        public VirtualRepo(DirectoryInfo path)
        {
            RepositoryPath = path;
        }

        private DirectoryInfo RepositoryPath { get; }

        public List<Storage> SaveStoragesRepository(IArchiveAlgorythm algo, List<JobObject> job, uint id)
        {
            List<Storage> storages = algo.CreateArchive(job);
            var newStorages = new List<Storage>();
            foreach (Storage storage in storages)
            {
                var newStorage = new Storage();
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    var fileInfo = new FileInfo($@"{RepositoryPath.FullName}/storage_{id}.zip/{jobObject.File.Name}_{id}");
                    var newJobObject = new JobObject(fileInfo);
                    newStorage.JobObjects.Add(newJobObject);
                }

                newStorages.Add(newStorage);
            }

            return newStorages;
        }
    }
}