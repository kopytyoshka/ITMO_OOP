using System.IO;
using Backups.Entities;
using Newtonsoft.Json;

namespace BackupsExtra.Tools
{
    public class SaveUpload
    {
        private void Serialization(RestorePoint restorePoint)
        {
            string jsonSerialize = JsonConvert.SerializeObject(restorePoint, Formatting.Indented, new Converter());
            File.AppendAllText("serialize.json", jsonSerialize);
        }

        private RestorePoint Deserialization(string path)
        {
            string jsonDeserialize = File.ReadAllText(path);
            RestorePoint restorePoint = JsonConvert.DeserializeObject<RestorePoint>(jsonDeserialize, new Converter());
            return restorePoint;
        }
    }
}