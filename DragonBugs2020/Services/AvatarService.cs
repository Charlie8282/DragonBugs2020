using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DragonBugs2020.Services
{
    public class AvatarService : IImageService
    {
        public byte[] ConvertFileToByteArray(IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            var byteFile = memoryStream.ToArray();
            memoryStream.Close();
            memoryStream.Dispose();

            return byteFile;

        }

        public string ConvertByteArrayToFile(byte[] fileData, string extension)
        {
            string imageBase64Data = Convert.ToBase64String(fileData);
            //string extension = Path.GetExtension(FileName);
            return string.Format($"data:image/{extension};base64,{imageBase64Data}");

        }

        public string GetFileIcon(string file)
        {
            string ext = Path.GetExtension(file).Replace(".", "");
            return $"/img/png/{ext}.png";
        }

        public async Task<byte[]> AssignAvatarAsync(string avatar)
        {
            var defaultAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/img", avatar);
            return await File.ReadAllBytesAsync(defaultAvatarPath);
        }
    }
}
