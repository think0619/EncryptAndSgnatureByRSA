using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using R3;

namespace TestSignAndEncrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                string str_DataToSign = @"Data to Sign!Data to Sign!Data to Sign!";
                Console.WriteLine("Data: " + str_DataToSign);
                Console.WriteLine("Data Length: " + str_DataToSign.Length.ToString());
                Console.WriteLine();

                RSACryptoServiceProvider Alice_RSAalg = new RSACryptoServiceProvider();

                string Alice_Private_Key = Convert.ToBase64String(Alice_RSAalg.ExportCspBlob(true));
                string Alice_Public_Key = Convert.ToBase64String(Alice_RSAalg.ExportCspBlob(false));
                Console.WriteLine("Alice's Public Key: " + Alice_Public_Key);
                Console.WriteLine();
                Console.WriteLine("Alice's Private Key: " + Alice_Private_Key);
                Console.WriteLine();

                RSACryptoServiceProvider Bob_RSAalg = new RSACryptoServiceProvider();

                string Bob_Private_Key = Convert.ToBase64String(Bob_RSAalg.ExportCspBlob(true));
                string Bob_Public_Key = Convert.ToBase64String(Bob_RSAalg.ExportCspBlob(false));
                Console.WriteLine("Bob's Public Key: " + Bob_Public_Key);
                Console.WriteLine();
                Console.WriteLine("Bob's Private Key: " + Bob_Private_Key);
                Console.WriteLine();


                //Step 1. Alice对Data签名
                string signedDate = R3.RSACSPSample.HashAndSign(str_DataToSign,Alice_Private_Key);
                var len1 = signedDate.Length;
                //Step 2. Alice用Bob公钥加密
                string EnryptedData = RSA_Encrypt(signedDate, Bob_Public_Key);
                var len2 = signedDate.Length;
                //Bob 接收数据
                //Step 3: 用Bob私钥解密
                string DecryptData=RSA_Decrypt(EnryptedData, Bob_Private_Key);
                //Step 4: 验证签名

                var x = R3.RSACSPSample.VerifySignedHash(str_DataToSign, DecryptData, Alice_Public_Key);



                ////Sign by Alice Private Key
                //string str_SignedData =R3.RSACSPSample.HashAndSign(str_DataToSign, Alice_Private_Key);// Hash and sign the data.
                //Console.WriteLine("Data Digital Signatures: " + str_SignedData);
                //Console.WriteLine();

                //if (R3.RSACSPSample.VerifySignedHash(str_DataToSign, str_SignedData, Alice_Public_Key))
                //{
                //    Console.WriteLine("Signature is OK.");
                //}
                //else
                //{
                //    Console.WriteLine("Wrong!");
                //}
                Console.Read();

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The data was not signed or verified");

            }
        }

        /// <summary>
        /// 公钥加密数据
        /// </summary>
        /// <param name="str_Plain_Text">明文数据</param>
        /// <param name="str_Public_Key">公钥</param>
        /// <returns></returns>
        static public string RSA_Encrypt(string str_Plain_Text,  string str_Public_Key)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] DataToEncrypt = ByteConverter.GetBytes(str_Plain_Text);
             DataToEncrypt =Zip(DataToEncrypt);
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                byte[] bytes_Public_Key = Convert.FromBase64String(str_Public_Key);
                RSA.ImportCspBlob(bytes_Public_Key);

                //OAEP padding is only available on Microsoft Windows XP or later.  
                byte[] bytes_Cypher_Text = RSA.Encrypt(DataToEncrypt, true);
                string str_Cypher_Text = Convert.ToBase64String(bytes_Cypher_Text);
                return str_Cypher_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //RSA解密
        static public string RSA_Decrypt(string str_Cypher_Text, string str_Private_Key)
        {
            byte[] DataToDecrypt = Convert.FromBase64String(str_Cypher_Text);
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                //RSA.ImportParameters(RSAKeyInfo);
                byte[] bytes_Private_Key = Convert.FromBase64String(str_Private_Key);
                RSA.ImportCspBlob(bytes_Private_Key);

                //OAEP padding is only available on Microsoft Windows XP or later.  
                byte[] bytes_Plain_Text = RSA.Decrypt(DataToDecrypt, false);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string str_Plain_Text = ByteConverter.GetString(bytes_Plain_Text);
                return str_Plain_Text;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static byte[] Zip(byte[] bytes)
        {
            //var bytes = System.Text.Encoding.UTF8.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }
                return mso.ToArray();
            }
        }
        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }
                return System.Text.Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
    }
}
