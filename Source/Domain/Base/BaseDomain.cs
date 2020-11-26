namespace Domain.Base
{
    public abstract class BaseDomain<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}