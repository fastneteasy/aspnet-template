using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AspNetTemplate.DAL.Entities
{
    public abstract class Entity
    {
        [Key]
        public long id { get; set; }

        [JsonIgnore]
        public long created_user_id { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;

        [JsonIgnore]
        public long updated_user_id { get; set; }

        [JsonIgnore]
        public DateTime updated_at { get; set; } = DateTime.Now;

        [JsonIgnore]
        public long? deleted_user_id { get; set; }

        [JsonIgnore]
        public DateTime? deleted_at { get; set; }
    }
}
