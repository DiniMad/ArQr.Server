using System.IO;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using Microsoft.Extensions.Configuration;

namespace ArQr.Infrastructure
{
    public class FileStorage : IFileStorage
    {
        public           string        BasePath { get; }
        private readonly DirectoryInfo _directoryInfo;

        public FileStorage(IConfiguration configuration)
        {
            BasePath       = configuration.GetFileStoragePath();
            _directoryInfo = new DirectoryInfo(BasePath);
            if (_directoryInfo.Exists is false) _directoryInfo.Create();
        }

        public async Task WriteFileAsync(string path, string fileName, byte[] content)
        {
            var filePath  = Path.Join(BasePath, path, fileName);

            _directoryInfo.CreateSubdirectory(path);
            await using var file = File.OpenWrite(filePath);
            await file.WriteAsync(content);
        }
    }
}