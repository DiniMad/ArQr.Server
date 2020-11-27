using System.Threading.Tasks;
using Domain;

namespace Data.Repository.Base
{
    public interface ISupportedMediaExtensionRepository : IRepository<SupportedMediaExtension, byte>
    {
        public Task<SupportedMediaExtension?> GetAsync(string extension);
    }
}