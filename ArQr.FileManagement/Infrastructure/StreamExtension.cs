using System.IO;

namespace ArQr.FileManagement.Infrastructure
{
    public static class StreamExtension
    {
        public static byte[] ToByte(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}