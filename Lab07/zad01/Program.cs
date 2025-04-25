using System;
using System.IO;
using System.Security.Cryptography;

class Program{
    public static void Main(string[] args){
        int command = int.Parse(args[0]);
        
        if (command == 0){
            GenerateAndSaveKeys();
        }
        else if(command == 1){
            EncryptFile(args[1],args[2]);
        }
        else if(command == 2){
            DecryptFile(args[1],args[2]);
        }

    }

    static void GenerateAndSaveKeys()
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
        string privateFile = "privateKey.xml";
        string publicFile = "publicKey.xml";
        File.WriteAllText(privateFile, rsa.ToXmlString(true));
        File.WriteAllText(publicFile, rsa.ToXmlString(false));
                
        Console.WriteLine($"Keys generated and saved to {privateFile} and {publicFile}");

    }

    static void EncryptFile(string inputFile, string outputFile)
    {
        string publicKeyXml = File.ReadAllText("publicKey.xml");
        
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        rsa.FromXmlString(publicKeyXml);
            
        byte[] dataToEncrypt = File.ReadAllBytes(inputFile);
        byte[] encryptedData = rsa.Encrypt(dataToEncrypt, true);
        
        File.WriteAllBytes(outputFile, encryptedData);
            
        Console.WriteLine($"File encrypted and saved to {outputFile}");
        
    }

    static void DecryptFile(string inputFile, string outputFile)
    {
        string privateKeyXml = File.ReadAllText("privateKey.xml");
        
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        rsa.FromXmlString(privateKeyXml);
            
        byte[] dataToDecrypt = File.ReadAllBytes(inputFile);
        byte[] decryptedData = rsa.Decrypt(dataToDecrypt, true);
        
        File.WriteAllBytes(outputFile, decryptedData);
            
        Console.WriteLine($"File decrypted and saved to {outputFile}");
        
    }
}