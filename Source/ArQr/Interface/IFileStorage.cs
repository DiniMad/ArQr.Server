using System.Threading.Tasks;

namespace ArQr.Interface
{
    public interface IFileStorage
    {
        public string BasePath { get; }

        public Task WriteFileAsync(string    fileName,  byte[]  content, string directory, string? subDirectory = null);
        public bool DirectoryExist(string    directory, string? subDirectory = null);
        public void DeleteDirectory(string   directory, string? subDirectory = null);
        public void ReCreateDirectory(string directory, string? subDirectory = null);
    }
}