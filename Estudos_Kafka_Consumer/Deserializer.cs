using Confluent.Kafka;
using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;

namespace Estudos_Kafka_Consumer
{
    public class Deserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using var memoryStream = new MemoryStream(data.ToArray());
            using var zip = new GZipStream(memoryStream, CompressionMode.Decompress, true);
            using var streamReader = new StreamReader(zip);
            var json = streamReader.ReadToEnd();

            // Testando GitHub Actions
           // return JsonSerializer.Deserialize<T>(json);
        }
    }
}
