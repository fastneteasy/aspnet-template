using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace AspNetTemplate.WebAPI.Extensions
{
    public class BigIntegerToStringConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
          JsonSerializer serializer)
        {
            if ((reader.ValueType == typeof(string) || reader.ValueType == null) && string.IsNullOrEmpty((string)reader.Value))
            {
                return null;
            }
            JToken jt = JToken.ReadFrom(reader);

            return jt.Value<BigInteger>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(BigInteger).Equals(objectType) || typeof(BigInteger?).Equals(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }
    }
}