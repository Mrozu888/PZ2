using System;
using System.Security.Cryptography;

class Program{
    public static void Main (String[] args){

        if(args.Length != 3){
            Console.WriteLine($"Error: Bad args");
            return;
        }
        string inputFile = args[0];
        string hashFile = args[1];
        string algorithmName = args[2].ToUpper();

        HashAlgorithm algorithm;
        if (algorithmName == "SHA256") algorithm = SHA256.Create();
        else if (algorithmName == "SHA512") algorithm = SHA512.Create();
        else if (algorithmName == "MD5") algorithm = MD5.Create();
        else {
            Console.WriteLine($"Error: Unsupported algorithm '{algorithmName}'. Supported: SHA256, SHA512, MD5");
            return;
        }

        if (!File.Exists(hashFile))
        {
            string hash = CalculateFileHash(inputFile, algorithm);
            File.WriteAllText(hashFile, hash);
            Console.WriteLine($"Hash calculated and saved from {inputFile} to {hashFile}");
            Console.WriteLine($"Hash ({algorithmName}): {hash}");
        }
        else
        {
            string expectedHash = File.ReadAllText(hashFile).Trim();
            string actualHash = CalculateFileHash(inputFile, algorithm);
            
            Console.WriteLine($"Expected hash: {expectedHash}");
            Console.WriteLine($"Actual hash:   {actualHash}");
            
            if (string.Equals(expectedHash, actualHash))
            {
                Console.WriteLine("HASHES MATCH");
            }
            else
            {
                Console.WriteLine("HASHES DO NOT MATCH");
            }
        }
    }

    static string CalculateFileHash(string filePath, HashAlgorithm algorithm)
    {
        using (var stream = File.OpenRead(filePath))
        {
            byte[] hashBytes = algorithm.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}