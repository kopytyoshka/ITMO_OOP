using System.Collections.Generic;
using System.IO;
using Backups.Archivator;
using Backups.Entities;
using Ionic.Zip;

namespace Backups.Repositories
{
    public class LocalRepo : IRepository
    {
        public LocalRepo(DirectoryInfo path)
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
                var zip = new ZipFile();
                var newStorage = new Storage();
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    zip.AddFile($"{jobObject.File.DirectoryName}/{jobObject.File.Name}", "/");
                    var fileInfo = new FileInfo($@"{RepositoryPath.FullName}/storage_{id}.zip/{jobObject.File.Name}{"_"}{id}.zip");
                }

                newStorages.Add(newStorage);
                zip.Save($@"{RepositoryPath}/storage_{storage.Id}.zip");
            }

            return newStorages;
        }
    }
}