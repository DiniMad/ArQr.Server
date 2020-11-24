namespace ArQr.Models
{
    public sealed record CacheOptions
    {
        public char   KyeSeparatorCharacter          { get; init; }
        public string GhostPrefix                    { get; init; }
        public string QrCodePrefix                   { get; init; }
        public string PersistedViewersCountPrefix    { get; init; }
        public string ViewersListPrefix              { get; init; }
        public int    ViewersCountExpireTimeInMinute { get; init; }

        public string SequenceKeyBuilder(params object[] keySections)
        {
            return string.Join(KyeSeparatorCharacter, keySections);
        }
    }
}