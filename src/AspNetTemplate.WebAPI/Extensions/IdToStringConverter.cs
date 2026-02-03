using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AspNetTemplate.WebAPI.Extensions
{
    public class IdToStringConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
          JsonSerializer serializer)
        {
            if ((reader.ValueType == typeof(string) || reader.ValueType == null) && string.IsNullOrEmpty((string)reader.Value))
            {
                return null;
            }
            JToken jt = JToken.ReadFrom(reader);

            return jt.Value<long>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(long).Equals(objectType) || typeof(long?).Equals(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }
    }
}