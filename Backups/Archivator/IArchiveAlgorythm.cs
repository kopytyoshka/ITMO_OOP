using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Archivator
{
    public interface IArchiveAlgorythm
    {
        List<Storage> CreateArchive(List<JobObject> jobObjects);
    }
}