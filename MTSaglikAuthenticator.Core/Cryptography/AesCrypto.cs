using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Core.Cryptography
{
    public static class AesCrypto
    {
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, encryptor);
                }
            }
        }

        public static void EncryptFile(string filePath, byte[] key, byte[] iv)
        {
            // Dosyanın okunması için FileStream nesnesi oluşturulur
            using (FileStream inputFile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Dosyanın şifrelenmiş hali için FileStream nesnesi oluşturulur

                using (FileStream outputFile = new FileStream(filePath + ".aes", FileMode.Create, FileAccess.Write))
                {
                    // AES sınıfından bir nesne oluşturulur
                    Aes aes = Aes.Create();
                    // Anahtar ve IV değerleri ayarlanır
                    aes.Key = key;
                    aes.IV = iv;
                    // Şifreleme işlemini gerçekleştirecek nesne oluşturulur
                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    // Verilerin şifrelenmesi için CryptoStream nesnesi oluşturulur
                    using (CryptoStream cryptoStream = new CryptoStream(outputFile, encryptor, CryptoStreamMode.Write))
                    {
                        // Dosyadaki veriler okunur ve şifrelenir
                        inputFile.CopyTo(cryptoStream);
                    }

                }


            }
            File.Delete(filePath);

        }

        public static void DecryptFile(string filePath, byte[] key, byte[] iv)
        {
            // Dosyanın okunması için FileStream nesnesi oluşturulur
            using (FileStream inputFile = new FileStream(filePath + ".aes", FileMode.Open, FileAccess.Read))
            {
                // Dosyanın çözülmüş hali için FileStream nesnesi oluşturulur
                using (FileStream outputFile = new FileStream(filePath.Replace(".aes", ""), FileMode.Create, FileAccess.Write))
                {
                    // AES sınıfından bir nesne oluşturulur
                    Aes aes = Aes.Create();
                    // Anahtar ve IV değerleri ayarlanır
                    aes.Key = key;
                    aes.IV = iv;
                    // Çözümleme işlemini gerçekleştirecek nesne oluşturulur
                    ICryptoTransform decryptor = aes.CreateDecryptor();
                    // Verilerin çözülmesi için CryptoStream nesnesi oluşturulur
                    using (CryptoStream cryptoStream = new CryptoStream(outputFile, decryptor, CryptoStreamMode.Write))
                    {
                        // Dosyadaki veriler okunur ve çözülür
                        inputFile.CopyTo(cryptoStream);
                    }
                }
            }
        }


        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, decryptor);
                }
            }
        }

        public static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }

        public static byte[] CreateUpdateKey()
        {
            byte[] UpdateKey = new byte[32];
            using (RNGCryptoServiceProvider rndDataGenerater = new RNGCryptoServiceProvider())
            {
                rndDataGenerater.GetBytes(UpdateKey);

                return UpdateKey;
            }
        }

        /// <summary>
        /// TS_13584 - 4.5.1>B-3 16 BYTE'LIK RANDOM İV
        /// </summary>
        /// <returns></returns>
        public static byte[] CreateIv()
        {
            byte[] iv = new byte[16];
            using (RNGCryptoServiceProvider rndDataGenerater = new RNGCryptoServiceProvider())
            {
                rndDataGenerater.GetBytes(iv);
                return iv;
            }
        }


        public static RSA GetRsaPublicKeyFromCertificate(byte[] crt)
        {
            try
            {
                using (X509Certificate2 cert = new X509Certificate2(crt))
                {
                    return cert.GetRSAPublicKey();
                }
            }
            catch (Exception)
            {
                //_logger.Error("Certificate GetPublicKeyError");
                return null;
            }
        }

    }
}
