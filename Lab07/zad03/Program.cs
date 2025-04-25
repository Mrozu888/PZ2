using System;
using System.Security.Cryptography;

class Program{
    static void Main(String[] args){

        if(args.Length != 2){
            Console.WriteLine($"Error: Bad args");
            return;
        }

        string inputFile = args[0];
        string signatureFile = args[1];

        if (!File.Exists(inputFile)) {
            Console.WriteLine($"Error: Input file '{inputFile}' does not exist.");
            return;
        }

        RSAParameters privateKey = LoadPrivateKey("privateKey.xml");
        RSAParameters publicKey = LoadPublicKey("publicKey.xml");

        if (File.Exists(signatureFile)) {
            bool isValid = VerifyFileSignature(inputFile, signatureFile, publicKey);
            Console.WriteLine($"Signature verification result: {(isValid ? "VALID" : "INVALID")}");
        }
        else {
            CreateFileSignature(inputFile, signatureFile, privateKey);
            Console.WriteLine($"Signature created and saved to {signatureFile}");
        }

    }

    static RSAParameters LoadPublicKey(string keyFile) {
        string keyXml = File.ReadAllText(keyFile);
        
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        
        rsa.FromXmlString(keyXml);
        return rsa.ExportParameters(false);
    }

    static RSAParameters LoadPrivateKey(string keyFile) {
        string keyXml = File.ReadAllText(keyFile);
        
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        
        rsa.FromXmlString(keyXml);
        return rsa.ExportParameters(true);
    }

    static void CreateFileSignature(string inputFile, string signatureFile, RSAParameters privateKey)
    {
        byte[] fileData = File.ReadAllBytes(inputFile);

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        SHA256 sha256 = SHA256.Create();

        rsa.ImportParameters(privateKey);

        byte[] hash = sha256.ComputeHash(fileData);
        byte[] signature = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
            
        File.WriteAllBytes(signatureFile, signature);
    }

    static bool VerifyFileSignature(string inputFile, string signatureFile, RSAParameters publicKey)
    {
        byte[] fileData = File.ReadAllBytes(inputFile);
        byte[] signature = File.ReadAllBytes(signatureFile);

        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        SHA256 sha256 = SHA256.Create();
        
        rsa.ImportParameters(publicKey);
        
        byte[] hash = sha256.ComputeHash(fileData);
        return rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signature);
    }
}