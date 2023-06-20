using System.Security.Cryptography;
using System.Security;

namespace StockModule.Utility
{
    public class TrippleDES
    {
        private const string _strTRIDESIV = "닼挛퍗";
        private const string _strTRIDESKey = "ந濶䃕桺샦ῴﶳ︍蔆❒㗰瀭";

        public static byte[] Encrypt(string plainText)
        {
            byte[] encrypted;
            // Create a new TripleDESCryptoServiceProvider. 
            using (System.Security.Cryptography.TripleDES tdes = System.Security.Cryptography.TripleDES.Create())
            {
                byte[] Key = GetBytes(_strTRIDESKey);
                byte[] IV = GetBytes(_strTRIDESIV);
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
        public static string Decrypt(byte[] cipherText)
        {
            string plaintext = null;
            // Create TripleDESCryptoServiceProvider 
            using (System.Security.Cryptography.TripleDES tdes = System.Security.Cryptography.TripleDES.Create())
            {
                byte[] Key = GetBytes(_strTRIDESKey);
                byte[] IV = GetBytes(_strTRIDESIV);
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

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static SecureString GetSecureString(string str)
        {
            SecureString sec_str = new SecureString();
            Array.ForEach(str.ToArray(), sec_str.AppendChar);
            sec_str.MakeReadOnly();
            return sec_str;
        }
    }
}