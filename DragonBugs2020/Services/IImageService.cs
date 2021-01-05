using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DragonBugs2020.Services
{
    public interface IImageService
    {
        public byte[] ConvertFileToByteArray(IFormFile file);

        public string ConvertByteArrayToFile(byte[] fileData, string extension);

        public Task<byte[]> AssignAvatarAsync(string avatar);
    }
}
