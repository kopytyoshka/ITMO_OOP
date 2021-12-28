using System;
using System.IO;
using Newtonsoft.Json;

namespace BackupsExtra.Tools
{
    public class Converter : JsonConverter
    {
        public override bool CanConvert(Type typeFile)
        {
            return typeFile == typeof(FileInfo);
        }

        public override object ReadJson(JsonReader jsonReader, Type objectType, object existingValue, JsonSerializer jsonSerializer)
        {
            if (jsonReader.Value is string div)
            {
                return new FileInfo(div);
            }

            throw new ArgumentOutOfRangeException(nameof(jsonReader));
        }

        public override void WriteJson(JsonWriter jsonWriter, object value, JsonSerializer jsonSerializer)
        {
            if (!(value is FileInfo fileInfo))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            jsonWriter.WriteValue(fileInfo.FullName);
        }
    }
}