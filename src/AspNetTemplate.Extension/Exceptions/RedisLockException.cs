namespace AspNetTemplate.Extension
{
    public class RedisLockException : ApplicationException
    {

        public RedisLockException() { }

        public RedisLockException(string message) : base(message)
        {
        }
    }
}
