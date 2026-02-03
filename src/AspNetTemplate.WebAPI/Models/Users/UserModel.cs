using AspNetTemplate.DAL.Entities;

namespace AspNetTemplate.WebAPI.Models
{
    public class UserModel
    {
    }

    public class ListUserModel
    {
        public ListUserModel(User user)
        {
            this.Id = user.id;
            this.Disabled = user.disabled;
            this.CreatedAt = user.created_at;
        }

        public long Id { get; set; }
        public bool Disabled { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
