using System;

namespace ArQr.Models
{
    public sealed record CacheOptions
    {
        public char   KyeSeparatorCharacter          { get; init; }
        public string GhostPrefix                    { get; init; }
        public string QrCodePrefix                   { get; init; }
        public string ViewersListPrefix              { get; init; }
        public int    ViewersCountExpireTimeInMinute { get; init; }

        public void Deconstruct(out string ghostPrefix,
                                out string qrCodePrefix,
                                out string viewersListPrefix,
                                out int    viewersCountExpireTimeInMinute)
        {
            ghostPrefix                    = GhostPrefix;
            qrCodePrefix                   = QrCodePrefix;
            viewersListPrefix              = ViewersListPrefix;
            viewersCountExpireTimeInMinute = ViewersCountExpireTimeInMinute;
        }

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