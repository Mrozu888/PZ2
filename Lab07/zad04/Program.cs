using System;
using System.Security.Cryptography;

class Program{

    private static readonly byte[] Salt = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
    private const int Iterations = 10000;
    private const int KeySize = 256;

    static void Main(String[] args){

        if(args.Length != 4){
            Console.WriteLine($"Error: Bad args");
            return;
        }

        string inputFile = args[0];
        string outputFile = args[1];
        string password = args[2];
        string option = args[3];

        if (!File.Exists(inputFile)) {
            Console.WriteLine($"Error: Input file '{inputFile}' does not exist.");
            return;
        }

        byte[] key = GenerateKeyFromPassword(password);

        if (option == "0")
        {
            EncryptFile(inputFile, outputFile, key);
            Console.WriteLine($"File encrypted and saved to {outputFile}");
        }
        else if(option == "1")
        {
            DecryptFile(inputFile, outputFile, key);
            Console.WriteLine($"File decrypted and saved to {outputFile}");
        }

    }

    static byte[] GenerateKeyFromPassword(string password)
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(password, Salt, Iterations))
        {
            return deriveBytes.GetBytes(KeySize / 8);
        }
    }

    static void EncryptFile(string inputFile, string outputFile, byte[] key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.GenerateIV();

            using (FileStream output = new FileStream(outputFile, FileMode.Create))
            {
                output.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cryptoStream = new CryptoStream(
                    output,
                    aes.CreateEncryptor(),
                    CryptoStreamMode.Write))
                {
                    using (FileStream input = new FileStream(inputFile, FileMode.Open))
                    {
                        input.CopyTo(cryptoStream);
                    }
                }
            }
        }
    }

    static void DecryptFile(string inputFile, string outputFile, byte[] key)
    {
        using (Aes aes = Aes.Create())
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open))
            {
                byte[] iv = new byte[aes.IV.Length];
                input.Read(iv, 0, iv.Length);

                aes.Key = key;
                aes.IV = iv;

                using (CryptoStream cryptoStream = new CryptoStream(
                    input,
                    aes.CreateDecryptor(),
                    CryptoStreamMode.Read))
                {
                    using (FileStream output = new FileStream(outputFile, FileMode.Create))
                    {
                        cryptoStream.CopyTo(output);
                    }
                }
            }
        }
    }
}