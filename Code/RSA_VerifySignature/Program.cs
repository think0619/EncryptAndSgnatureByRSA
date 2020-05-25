using System;
using System.Security.Cryptography;

class RSASample
{

    static void Main()
    {
        try
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            byte[] Hash = { 59, 4, 248, 102, 77, 97, 142, 201, 210, 12, 224, 93, 25, 41, 100, 197, 213, 134, 130, 135 };

            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
            RSAFormatter.SetHashAlgorithm("SHA1");
            byte[] SignedHash = RSAFormatter.CreateSignature(Hash);

            Console.WriteLine("签名后的Hash:" + Convert.ToBase64String(SignedHash));

            RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
            RSADeformatter.SetHashAlgorithm("SHA1");

            if (RSADeformatter.VerifySignature(Hash, SignedHash))
            {
                Console.WriteLine("The signature was verified.");
            }
            else
            {
                Console.WriteLine("The signature was not verified.");
            }

        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);
        }
    }

}
