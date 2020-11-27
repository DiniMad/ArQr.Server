using System.Threading.Tasks;

namespace ArQr.Interface
{
    public interface IFileStorage
    {
        public string BasePath { get; }

        public Task WriteFileAsync(string path, string fileName, byte[] content);
    }
}