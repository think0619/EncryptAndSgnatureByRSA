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
                string str_DataToSign = @"Test data";
                Console.WriteLine("Data: " + str_DataToSign); 
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
                //string signedDate = HashAndSign(str_DataToSign, Alice_Private_Key);
                ////Step 2. Alice用Bob公钥加密
                //string EnryptedData = RSA_Encrypt(str_DataToSign, Bob_Public_Key); 
                string signedDate = HashAndSign(str_DataToSign, "BwIAAACkAABSU0EyAAQAAAEAAQDRyrB1TEb7jTTq3I0xsbA9sW+t/UfSGtSFX87ethdpOTy9wXqtt5L8UJTwvlQMVQppCjat31t7oFp3xYgqoYAl2dwqQE2kVDeeblfpBDS2Cqu5UabolMd6oOsTgIqoxDkL9HXQcSpHIoBNAIFGfJUv3r5lxsdzt+bmUBAHxhsho8cdHGF7xBwqS+a4G50U+ZDeDqCpzuk+EaoINvEXhkPii9KZ354XvmVzRmOWSVT1TsSbz4nWqTtjT7eb7HfW9NWnsjNX3rGUvNccSkWRv95zFF7HtVS5Af2viO9teRmuIUBjkac5651o2ikzkpXyLqaMw3tnUe4SKqNxKwIOZS/DTWvXFa9r02VO1xTtUVkBJuNAEtkvPqP5FBGrSNPf/3At7v40uEnRQH0Q/vCYnsttnmq/GMftfpaSrecHgDz5JlkzseNXSEteW3ykT5OSOef8cTEFqay3/93YSrbLghS/x7cXzC1v5OmK6vBwFtrHFRYbAzcYwJkzpbOt+mj/xwreuwoIZZM76RW51BvivzDsySMJbIyTI26Tu/ZOx2IVHdBRFBIh1XPnpjcmYv77x6pR12l7YPegU3cB7G78qhgTsYNHmdtJno4f/c9HLLYAgRziS4k9oW66JmL56TTy3CAX/L/pn8uhTClG6bR5ANym1OQWRSoR851LQJp2ZZ8vul5lDAVceQCLtu00llfDiD4VmY4k8L7v0F2bfTRsy7M8Uduf1QcftyucKljP7c5+SKDeCD4v20s0ynXuygklumA=");
                
                string EnryptedData = RSA_Encrypt(str_DataToSign, "BwIAAACkAABSU0EyAAQAAAEAAQAxQctLlvekj+KfMP0Qf2kLrber9cD8I5iE0UekBlzCeicIiLBBxuGImGM6pY2sJACR7U0/UJl/TrUM+rWzkci2HxlVW7TlndctQmXkKSFj7BJzUe1uX/SUlovzBWwWd0cXtTDeBrf6e69rnqxSC6OfTbomSybwRZJsE0Y9RqLKvq/gd0TYt0C5PKjA3UVmkGDXVYQp7uoghQAJtcHBthK+2FK4rcpmOqhbZkhxjq980V0PAAQlwvjEfd3C0imB8eAftHSY+GnVCJCH6oNWDDP3ousbcW+DfqAiFICf6cWBieowkSl2hY+XWFRTgLjJ7cV8l6MOygrra3n1CqNUDSLZ1SDsA3T0QTHoy0YE9XvfpM2/sE4KeaXh2fjRSt9E+ZCC7Zl6QILUZtzVSOKeZ7dUqpDaB0YEGXucEquuCWGZB181tqhUVbSUOKBvfhoqgfnr5rYvn1asvPuzTEzuEukAXqxY4QIBatZB1X4fL9LABTUx0nfrvB23rlK2kgPm+rRsFAfZMm37kckcPYf/B/3QdLbewzQ6jRh9JZOvUPdqjcDwwV7V5XqSbVGd83nGU66WoR2HZb04NRIBuWIa7c5KQSdsZ29WeklqiTaBpC0hCmb7+riupdgFEwPryAvEoUERhj/tfbzqMq0KyH36RahInj5Xd0rluvJqZ/GiThBKdP9efQWr5AhovEjJKta0IOVfCuap8VrQ1YAT8OaVdtE6rhcjKp1gjgW9remDKFf1uFGerV/Y5/oPg7atxwth93g=");



                //Bob 接收数据
                //Step 3: 用Bob私钥解密
                //string DecryptData = RSA_Decrypt(EnryptedData, Bob_Private_Key);
                string DecryptData = RSA_Decrypt(EnryptedData, Bob_Private_Key);
                //Step 4: 验证签名 
                var varifyresult =  VerifySignedHash(DecryptData, signedDate, Alice_Public_Key);
                
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
