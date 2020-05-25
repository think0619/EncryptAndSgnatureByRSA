using System;
using System.Security.Cryptography;

class RSASample
{

    static void Main()
    {
        try
        {
            //Create a new instance of RSACryptoServiceProvider.
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            //The hash to sign.
            byte[] Hash = { 59, 4, 248, 102, 77, 97, 142, 201, 210, 12, 224, 93, 25, 41, 100, 197, 213, 134, 130, 135 };

            //Create an RSAOPKCS1SignatureFormatter object and pass it the 
            //RSACryptoServiceProvider to transfer the key information.
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);

            //Set the hash algorithm to SHA1.
            RSAFormatter.SetHashAlgorithm("SHA1");

            //Create a signature for HashValue and return it.
            byte[] SignedHash = RSAFormatter.CreateSignature(Hash);

            Console.WriteLine(Convert.ToBase64String(SignedHash));
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);
        }
    }

}