/*
 * 基于RSA的加密/解密示例C#代码
 * (采用字符串作为参数)RSA_Demo2
 * 
 * 夏春涛 Email:xChuntao@163.com 
 * Blog:http://bluesky521.cnblogs.com
 * 运行环境：.net2.0 framework
 * 
 * 备注：
 * 不对称算法通常用于加密少量数据，如加密对称密钥和 IV。通常，
 * 执行不对称加密的个人使用由另一方生成的公钥。.NET Framework 
 * 为此目的而提供了 RSACryptoServiceProvider 类。
 */

using System;
using System.Security.Cryptography;
using System.Text;

class RSACSPSample
{

    static void Main()
    {
        try
        {
            string str_Plain_Text = "How are you?How are you?How are you?How are you?=-popopolA";
            Console.WriteLine("明文：" + str_Plain_Text);
            Console.WriteLine("长度：" + str_Plain_Text.Length.ToString());
            Console.WriteLine();

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            string str_Public_Key;
            string str_Private_Key;
            string str_Cypher_Text = RSA_Encrypt(str_Plain_Text, out str_Public_Key,out str_Private_Key);
            Console.WriteLine("密文：" + str_Cypher_Text);
            Console.WriteLine("公钥：" + str_Public_Key);
            Console.WriteLine("私钥：" + str_Private_Key);

            string str_Plain_Text2 = RSA_Decrypt(str_Cypher_Text, str_Private_Key);
            Console.WriteLine("解密：" + str_Plain_Text2);

            Console.WriteLine();
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("Encryption failed.");
        }
    }

    //RSA加密,随机生成公私钥对并作为出参返回
    static public string RSA_Encrypt(string str_Plain_Text, out string str_Public_Key, out string str_Private_Key)
    {
        str_Public_Key = "";
        str_Private_Key = "";
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        byte[] DataToEncrypt = ByteConverter.GetBytes(str_Plain_Text);
        try
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            str_Public_Key = Convert.ToBase64String(RSA.ExportCspBlob(false));
            str_Private_Key = Convert.ToBase64String(RSA.ExportCspBlob(true)); 

            byte[] bytes_Cypher_Text = RSA.Encrypt(DataToEncrypt, false);
            str_Public_Key = Convert.ToBase64String(RSA.ExportCspBlob(false));
            str_Private_Key = Convert.ToBase64String(RSA.ExportCspBlob(true));
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
}
