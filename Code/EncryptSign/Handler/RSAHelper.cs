using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptSign.Handler
{
    public class RSAHelper
    {
        /// <summary>
        /// 生成RSA公钥和私钥
        /// </summary>
        /// <param name="user_uid">用户身份信息，如用户名、邮箱等</param>
        /// <param name="str_Public_Key">公钥(转为Base64编码)</param>
        /// <param name="str_Private_Key">私钥(转为Base64编码)</param>
        public static void generateKey(string user_uid, out string str_Public_Key, out string str_Private_Key)
        {
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = user_uid;
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);
            str_Private_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(true));
            str_Public_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(false));
        }

        /// <summary>
        /// 公钥加密数据，
        /// </summary>
        /// <param name="str_Plain_Text">明文数据</param>
        /// <param name="str_Public_Key">公钥</param>
        /// <returns>返回密文（Base64编码格式）</returns>
        static public string RSA_Encrypt(string str_Plain_Text, string str_Public_Key)
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

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="str_Cypher_Text">密文（Base64编码格式）</param>
        /// <param name="str_Private_Key">私钥</param>
        /// <returns></returns> 
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


        /// <summary>
        /// 对数据签名
        /// </summary>
        /// <param name="str_DataToSign">待签名的数据</param>
        /// <param name="str_Private_Key">私钥</param>
        /// <returns>消息摘要签名（Base64编码格式）</returns>
        public static string HashAndSign(string str_DataToSign, string str_Private_Key)
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToSign = ByteConverter.GetBytes(str_DataToSign);
            try
            {
                //导入私钥
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportCspBlob(Convert.FromBase64String(str_Private_Key));

                //对消息摘要（SHA1）签名
                byte[] signedData = RSAalg.SignData(DataToSign, new SHA256CryptoServiceProvider());
                string str_SignedData = Convert.ToBase64String(signedData);
                return str_SignedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="str_DataToVerify">待验证的数据</param>
        /// <param name="str_SignedData">已签名的数据</param>
        /// <param name="str_Public_Key">公钥</param>
        /// <returns></returns>
        public static bool VerifySignedHash(string str_DataToVerify, string str_SignedData, string str_Public_Key)
        {
            byte[] SignedData = Convert.FromBase64String(str_SignedData);

            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToVerify = ByteConverter.GetBytes(str_DataToVerify);
            try
            {
                //导入公钥
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportCspBlob(Convert.FromBase64String(str_Public_Key));
                //验证签名
                return RSAalg.VerifyData(DataToVerify, new SHA256CryptoServiceProvider(), SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
