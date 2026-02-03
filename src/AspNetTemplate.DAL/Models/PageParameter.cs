using Newtonsoft.Json;

namespace AspNetTemplate.DAL.Models
{
    public class PageParameter
    {
        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public bool SortASC { get; set; }

        public bool Pagination { get; set; } = true;

        [JsonIgnore]
        public long Offset
        {
            get { return this.PageSize * (this.PageNumber - 1); }
        }
    }


    public class PageParameter<T> : PageParameter where T : new()
    {
        public T Parameter { get; set; } = new();
    }
}