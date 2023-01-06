using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Entities.Helpers
{
    public static class FileHashing
    {
        public static string SHA256CheckSum(string filePath)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = System.IO.File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }

        public static string FileCreatePath(string fileName)
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + fileName;
            System.IO.FileStream newFile = new System.IO.FileStream(file, System.IO.FileMode.Create);
            newFile.Close();
            return file;
        }
    }
}
