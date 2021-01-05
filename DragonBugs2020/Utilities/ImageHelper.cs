using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Utilities
{
    public class ImageHelper
    {
        public byte[] EncodeImage(IFormFile image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);
            var output = ms.ToArray();

            ms.Close();
            ms.Dispose();

            return output;
        }

        public static string DecodeImage(byte[] imageData, string fileName)
        {
            var binary = Convert.ToBase64String(imageData);
            var ext = Path.GetExtension(fileName);
            string imageDataURL = $"data:image/{ext};base64,{binary}";
            return imageDataURL;
        }
    }
}
