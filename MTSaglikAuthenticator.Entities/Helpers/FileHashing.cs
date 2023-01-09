using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
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

        //public static string FileCreatePath(IFormFile file1)
        //{
        //    var file = AppDomain.CurrentDomain.BaseDirectory + file1.FileName;
        //    //var filepath = AppDomain.CurrentDomain.BaseDirectory + file1.FileName;
        //    //MemoryStream destination = new MemoryStream();
        //    Stream stream = file1.OpenReadStream();

        //    //System.IO.FileStream newFile = new System.IO.FileStream(file, System.IO.FileMode.Append, FileAccess.Write,FileShare.Write);
        //    using (System.IO.FileStream newFile = new System.IO.FileStream(file, System.IO.FileMode.Append, FileAccess.Write, FileShare.Write))
        //    {
        //        using (var sw = new StreamWriter(newFile))
        //        {
        //            sw.Write(file1);
        //        }
                
        //    }
        //    //newFile.CopyTo();
        //    //newFile.Close();
        //    //FileStream output = new FileStream(filepath, FileMode.Create, FileAccess.Write);
        //    //Aes aes = Aes.Create();
        //    //ICryptoTransform cryptoTransform = aes.CreateEncryptor();
        //    //CryptoStream cryptoStream = new CryptoStream(output,cryptoTransform, CryptoStreamMode.Write);


        //    //StreamWriter writer = new StreamWriter(file);
        //    //writer.Write(newFile);
        //    //writer.Close();
        //    return file;
        //}

        public static string FileCreatePath(IFormFile file1)
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + file1.FileName;
            //var filepath = AppDomain.CurrentDomain.BaseDirectory + file1.FileName;
            //MemoryStream destination = new MemoryStream();
            Stream stream = file1.OpenReadStream();

            //System.IO.FileStream newFile = new System.IO.FileStream(file, System.IO.FileMode.Append, FileAccess.Write,FileShare.Write);
            using (System.IO.FileStream newFile = new System.IO.FileStream(file, System.IO.FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (var sw = new StreamWriter(newFile))
                {
                    sw.Write(file1);
                }

            }
            //newFile.CopyTo();
            //newFile.Close();
            //FileStream output = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            //Aes aes = Aes.Create();
            //ICryptoTransform cryptoTransform = aes.CreateEncryptor();
            //CryptoStream cryptoStream = new CryptoStream(output,cryptoTransform, CryptoStreamMode.Write);


            //StreamWriter writer = new StreamWriter(file);
            //writer.Write(newFile);
            //writer.Close();
            return file;
        }
    }
}
