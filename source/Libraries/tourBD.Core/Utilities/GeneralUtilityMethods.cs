using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace tourBD.Core.Utilities
{
    public static class GeneralUtilityMethods
    {
        public static async Task<string> GetSavedImageUrlAsync(IFormFile file, string physicalUploadPath, string demoImage)
        {
            if (file != null && file.Length > 0)
            {
                // Create directory
                if (!Directory.Exists(physicalUploadPath))
                    Directory.CreateDirectory(physicalUploadPath);

                // Create unique file name
                var guid = "pic" + Guid.NewGuid().ToString();
                var uniqueFileName = Path.Combine(guid + "." + file.FileName.Split(".")[1].ToLower());
                var fullPath = physicalUploadPath + uniqueFileName;
                //var imageVirtualPath = logicalImagePath + uniqueFileName;

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return uniqueFileName;
            }
            else
                return demoImage;
        }

        public static string GeneratePackageCode()
        {
            Random random = new Random();
            string fourDigitNumber = (random.Next(1000, 9999)).ToString();
            string twoLetter = "" + (char)('A' + random.Next(0, 25)) + (char)('A' + random.Next(0, 25));
            string code = twoLetter + fourDigitNumber;
            return code;
        }
    }
}
