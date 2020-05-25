
/* 基于RSA的数字签名和验证C#源码
 * (采用字符串作为参数)RSA_Demo3
 * 
 * 夏春涛 Email:xChuntao@163.com 
 * Blog:http://bluesky521.cnblogs.com
 * 运行环境：.net2.0 framework
 */

/* 备注：
 * 若要使用公钥系统对消息进行数字签名，发送方先向该消息应用哈希函数以创建消息摘要。
 * 然后，发送方用自己的私钥对消息摘要进行加密，以创建发送方的个人签名。在收到消息和
 * 签名后，接收方使用发送方的公钥解密该签名，以恢复消息摘要，并使用发送方所用的同一
 * 哈希算法对该消息进行哈希运算。如果接收方计算的消息摘要与从发送方收到的消息摘要完
 * 全匹配，则接收方可以假定消息在传输中未被更改。请注意，因为发送方的公钥是公共知识，
 * 所以任何人都可以验证签名。
 */

using System;
using System.Security.Cryptography;
using System.Text;
namespace R3
{
   public class RSACSPSample
    {
        static void Main()
        {
            try
            {

                string str_DataToSign = @"Data to Sign!Data to Sign!Data to Sign!";
                Console.WriteLine("原文：" + str_DataToSign);
                Console.WriteLine("长度：" + str_DataToSign.Length.ToString());
                Console.WriteLine();

                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                string str_Private_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(true));
                string str_Public_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(false));
                Console.WriteLine("公钥：" + str_Public_Key);
                Console.WriteLine();
                Console.WriteLine("私钥：" + str_Private_Key);
                Console.WriteLine();
                string str_SignedData = HashAndSign("x", "BgIAAACkAABSU0ExAAQAAAEAAQAhmButqdiSZAuH9GvDglIQU4slmYtNzMBIdrZ1GJsCyY8LFL3NHd5AnZ848Z2CudAOMeoHEAOCpYqtLf2ufrkTtTQAYRFchHWQKWPPO9Lwec6cS2iKxH5bK2oNbHe6S7OKtIdEeu3T6KaX7aCbRusfSaYubXIrLyqrgXcRsNnlwQ==");
             //   string str_SignedData = HashAndSign(str_DataToSign, str_Private_Key);// Hash and sign the data.
                Console.WriteLine("签名数据：" + str_SignedData);
                Console.WriteLine();

                if (VerifySignedHash(str_DataToSign, str_SignedData, str_Public_Key))
                {
                    Console.WriteLine("验证签名OK.");
                }
                else
                {
                    Console.WriteLine("签名不匹配!");
                }
                Console.Read();

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The data was not signed or verified");

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