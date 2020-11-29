using System.IO;
using System.Threading.Tasks;

namespace ArQr.Interface
{
    public interface IFileStorage
    {
        public string BasePath { get; }

        public Task WriteFileAsync(string           directory, string fileName, byte[] content);
        public Task WriteChunksFromDiskToStream(string    directory, Stream stream);
        public long CalculateChunksTotalSize(string directory);
        public bool DirectoryExist(string           directory);
        public void DeleteDirectory(string          directory);
        public void ReCreateDirectory(string        directory);
    }
}