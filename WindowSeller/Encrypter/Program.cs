// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");
string data = Console.ReadLine();
Apply3DES(data);
Console.ReadLine();



static void Apply3DES(string raw)
{
    try
    {
        string _strTRIDESIV = "닼挛퍗";
        string _strTRIDESKey = "ந濶䃕桺샦ῴﶳ︍蔆❒㗰瀭";
        byte[] Key = GetBytes(_strTRIDESKey);
        byte[] IV = GetBytes(_strTRIDESIV);
        // Create 3DES that generates a new key and initialization vector (IV). 
        // Same key must be used in encryption and decryption 
        using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
        {
            // Encrypt string 
            byte[] encrypted = Encrypt(raw, Key, IV);
            // Print encrypted string 
            Console.WriteLine("Encrypted data:" + System.Text.Encoding.Unicode.GetString(encrypted));
            // Decrypt the bytes to a string. 
            string decrypted = Decrypt(encrypted, Key, IV);
            // Print decrypted string. It should be same as raw data 
            Console.WriteLine("Decrypted data: " + decrypted);
            decrypted = Decrypt(GetBytes(System.Text.Encoding.Unicode.GetString(encrypted).Trim()), Key, IV);
        }
    }
    catch (Exception exp)
    {
        Console.WriteLine(exp.Message);
    }
    Console.ReadKey();
}
static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
{
    byte[] encrypted;
    // Create a new TripleDESCryptoServiceProvider. 
    using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
    {
        // Create encryptor 
        ICryptoTransform encryptor = tdes.CreateEncryptor(Key, IV);
        // Create MemoryStream 
        using (MemoryStream ms = new MemoryStream())
        {
            // Create crypto stream using the CryptoStream class. This class is the key to encryption 
            // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream 
            // to encrypt 
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                // Create StreamWriter and write data to a stream 
                using (StreamWriter sw = new StreamWriter(cs))
                    sw.Write(plainText);
                encrypted = ms.ToArray();
            }
        }
    }
    // Return encrypted data 
    return encrypted;
}
static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
{
    string plaintext = null;
    // Create TripleDESCryptoServiceProvider 
    using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
    {
        // Create a decryptor 
        ICryptoTransform decryptor = tdes.CreateDecryptor(Key, IV);
        // Create the streams used for decryption. 
        using (MemoryStream ms = new MemoryStream(cipherText))
        {
            // Create crypto stream 
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                // Read crypto stream 
                using (StreamReader reader = new StreamReader(cs))
                    plaintext = reader.ReadToEnd();
            }
        }
    }
    return plaintext;
}

static byte[] GetBytes(string str)
{
    byte[] bytes = new byte[str.Length * sizeof(char)];
    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
    return bytes;
}