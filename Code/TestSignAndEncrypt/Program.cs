using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text; 

namespace TestSignAndEncrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                string str_DataToSign = @"壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾壹贰叁肆伍陆柒捌玖拾";
                int len = str_DataToSign.Length;
                Console.WriteLine("Length: " + len.ToString());
                Console.WriteLine("Data: " + str_DataToSign); 
                Console.WriteLine();

                CspParameters aliceCspParams = new CspParameters();
                aliceCspParams.KeyContainerName = "alice@qq.com";
                RSACryptoServiceProvider Alice_RSAalg = new RSACryptoServiceProvider(2048, aliceCspParams); 

                string Alice_Private_Key = Convert.ToBase64String(Alice_RSAalg.ExportCspBlob(true));
                string Alice_Public_Key = Convert.ToBase64String(Alice_RSAalg.ExportCspBlob(false));
                Console.WriteLine("Alice's Public Key: " + Alice_Public_Key);
                Console.WriteLine();
                Console.WriteLine("Alice's Private Key: " + Alice_Private_Key);
                Console.WriteLine();

                RSACryptoServiceProvider Bob_RSAalg = new RSACryptoServiceProvider(2048); 
                string Bob_Private_Key = Convert.ToBase64String(Bob_RSAalg.ExportCspBlob(true));
                string Bob_Public_Key = Convert.ToBase64String(Bob_RSAalg.ExportCspBlob(false));
                Console.WriteLine("Bob's Public Key: " + Bob_Public_Key);
                Console.WriteLine();
                Console.WriteLine("Bob's Private Key: " + Bob_Private_Key);
                Console.WriteLine();

                //Step 1.Alice对Data签名
                string signedDate = HashAndSign(str_DataToSign, Alice_Private_Key);
                //Step 2. Alice用Bob公钥加密
                string EnryptedData = RSA_Encrypt(str_DataToSign, Bob_Public_Key);



                //Bob 接收数据
                //Step 3: 用Bob私钥解密
                //string DecryptData = RSA_Decrypt(EnryptedData, Bob_Private_Key);
                string DecryptData = RSA_Decrypt(EnryptedData, Bob_Private_Key);
                //Step 4: 验证签名 
                var varifyresult =  VerifySignedHash(DecryptData, signedDate, Alice_Public_Key);
                if (varifyresult)
                {
                    Console.WriteLine(String.Format($"明文：{DecryptData}"));
                }
              
                Console.WriteLine(String.Format("验证{0}", varifyresult ? "成功" : "失败"));

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
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                byte[] bytes_Public_Key = Convert.FromBase64String(str_Public_Key);
                RSA.ImportCspBlob(bytes_Public_Key);

             
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
                byte[] bytes_Private_Key = Convert.FromBase64String(str_Private_Key);
                RSA.ImportCspBlob(bytes_Private_Key);
                 
                byte[] bytes_Plain_Text = RSA.Decrypt(DataToDecrypt, true);
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

        //对数据签名
        public static string HashAndSign(string str_DataToSign, string str_Private_Key)
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToSign = ByteConverter.GetBytes(str_DataToSign);
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportCspBlob(Convert.FromBase64String(str_Private_Key));
                byte[] signedData = RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
                string str_SignedData = Convert.ToBase64String(signedData);
                var lenth = str_SignedData.Length;
                return str_SignedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //验证签名
        public static bool VerifySignedHash(string str_DataToVerify, string str_SignedData, string str_Public_Key)
        {
            byte[] SignedData = Convert.FromBase64String(str_SignedData);

            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToVerify = ByteConverter.GetBytes(str_DataToVerify);
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportCspBlob(Convert.FromBase64String(str_Public_Key));

                return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
