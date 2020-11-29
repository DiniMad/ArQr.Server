using System.Threading.Tasks;

namespace ArQr.Interface
{
    public interface IFileStorage
    {
        public string BasePath { get; }

        public Task WriteFileAsync(string    directory, string fileName, byte[] content);
        public bool DirectoryExist(string    directory);
        public void DeleteDirectory(string   directory);
        public void ReCreateDirectory(string directory);
    }
}