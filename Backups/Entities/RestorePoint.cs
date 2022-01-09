using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private static uint _id;

        public RestorePoint()
        {
            DateTime = DateTime.Now;
            Id = ++_id;
            Storages = new List<Storage>();
        }

        public DateTime DateTime { get; }
        public List<Storage> Storages { get; }
        public string RestoreDirectory { get; set; }
        private uint Id { get; }
    }
}