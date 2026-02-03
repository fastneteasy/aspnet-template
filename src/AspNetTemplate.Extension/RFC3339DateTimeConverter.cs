using Newtonsoft.Json.Converters;

namespace AspNetTemplate.Extension
{
    public class RFC3339DateTimeConverter : IsoDateTimeConverter
    {
        public RFC3339DateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
        }
    }
}
