namespace AspNetTemplate.DAL.Models
{
    /// <summary>
    /// 分页用的工具
    /// </summary>
    public class PageCollection<T>
    {
        public PageCollection(long total, int size, IList<T> collection)
        {
            TotalCount = total;
            PageSize = size;
            Collection = collection;
        }

        public long TotalCount { get; set; }

        public int PageSize { get; set; }

        public int PageCount
        {
            get
            {
                if (this.PageSize == 0 || this.TotalCount == 0) return 0;
                if (this.TotalCount % this.PageSize == 0)
                {
                    return (int)(this.TotalCount / this.PageSize);
                }
                else
                {
                    return ((int)(this.TotalCount / this.PageSize)) + 1;
                }
            }
        }

        public IList<T> Collection { get; set; }
    }
}
