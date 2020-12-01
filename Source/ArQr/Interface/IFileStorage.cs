using System.IO;
using System.Threading.Tasks;

namespace ArQr.Interface
{
    public interface IFileStorage
    {
        public string BasePath { get; }

        public Task WriteFileAsync(string        directory, string fileName, byte[] content);
        public Task WriteFromDiskToStream(string directory, string fileName, Stream stream, int chunkSize);
        public long GetFileSize(string           directory, string fileName);
        public bool DirectoryExist(string        directory);
        public void DeleteDirectory(string       directory);
        public void ReCreateDirectory(string     directory);
    }
}