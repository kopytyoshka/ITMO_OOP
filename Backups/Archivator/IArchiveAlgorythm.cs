using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Archivator
{
    public interface IArchiveAlgorythm
    {
        public List<Storage> CreateArchive(List<JobObject> jobObjects);
    }
}