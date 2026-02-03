using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetTemplate.DAL.Entities
{
    [Table("user")]
    public class User : Entity
    {
        public string? name { get; set; }
        public bool disabled { get; set; }
    }
}
