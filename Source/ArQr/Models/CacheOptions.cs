namespace ArQr.Models
{
    public sealed record CacheOptions
    {
        public char   KyeSeparatorCharacter           { get; init; }
        public string GhostPrefix                     { get; init; }
        public string QrCodePrefix                    { get; init; }
        public string ViewersListPrefix               { get; init; }
        public string PaymentPrefix                   { get; init; }
        public string UploadSessionPrefix             { get; init; }
        public string ChunkListPrefix                 { get; init; }
        public int    ViewersCountExpireTimeInMinute  { get; init; }
        public int    PaymentExpireTimeInMinute       { get; init; }
        public int    UploadSessionExpireTimeInMinute { get; init; }

        public string SequenceKeyBuilder(params object[] keySections)
        {
            return string.Join(KyeSeparatorCharacter, keySections);
        }

        public bool IsGhostKey(string key)
        {
            return KeyHasPrefix(key, GhostPrefix);
        }

        public string ExtractRawKey(string key)
        {
            return key.Replace($"{GhostPrefix}{KyeSeparatorCharacter}", string.Empty);
        }

        public bool KeyHasPrefix(string key, string prefix)
        {
            return key.StartsWith(prefix);
        }
    }
}