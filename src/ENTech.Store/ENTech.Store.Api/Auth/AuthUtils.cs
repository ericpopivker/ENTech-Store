using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace ENTech.Store.Api.Auth
{
    public class AuthTokenInfo
    {
        public string UserId { get; set; }
        public string Login { get; set; }
        public long Created { get; set; }

        public string Secret { get; set; }

        public AuthTokenInfo() { }
        public AuthTokenInfo(string userId, string login, string secret)
        {
            this.UserId = userId;
            this.Login = login;
            this.Created = DateTime.UtcNow.Ticks;
            this.Secret = secret;
        }

    }


    public class AuthToken
    {

        private const int ExpirationInMinutes = 30 * 24 * 60;


        public string Token { get; private set; }
        
        public string Tag { get; set; }

        public AuthTokenInfo Info { get; private set; }

        public AuthToken() { }

        public AuthToken(AuthTokenInfo info)
        {
            this.Info = info;
            var serialized = JsonConvert.SerializeObject(info);

            var encrypted = UserAuthCrypto.EncryptToken(serialized);
            this.Token = encrypted;
        }

        public AuthToken(string stoken)
        {
            this.Token = stoken;

            var serialized = UserAuthCrypto.DecryptToken(stoken);
            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthTokenInfo>(serialized);
            this.Info = info;
        }

        public bool IsValid(string secret)
        {
            var ts = DateTime.UtcNow - new DateTime(Info.Created, DateTimeKind.Utc);
            var minutes = ts.TotalMinutes;
            return (minutes >= 0 && minutes <= ExpirationInMinutes) && Info.Secret == secret;
        }
    }

    public class UserAuthCrypto
    {
        private static readonly Dictionary<string, string> Keys = new Dictionary<string, string>();
        private const string CurrentkeyCode = "efG5u9"; //6 chars

        static UserAuthCrypto()
        {
            //the values are not going to be used in prod or dev
            Keys.Add("efG5u9", "#key!589?VSNI*io$su");
        }

        public static string EncryptToken(string rawToken)
        {
            var algoIndex = "uVcOD5";//6 chars
            var key = Keys[CurrentkeyCode];
            var token = StringCipher.Encrypt(rawToken, key);
            return algoIndex + CurrentkeyCode + token;
        }

        public static string DecryptToken(string encryptedToken)
        {
            var algoIndex = encryptedToken.Substring(0,6);//6 chars
            var keyCode= encryptedToken.Substring(6, 6);//6 chars
            var key = Keys[keyCode];

            var token = StringCipher.Decrypt(encryptedToken.Substring(12), key);
            return token;
        }


 
    }


    public static class StringCipher
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.

        //the values are not going to be used in prod or dev
        private const string initVector = "111q8qwerwerwe0t82qu4";//todo: read from the config

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string Encrypt(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();

                return Convert.ToBase64String(cipherTextBytes);
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
        }
    }

}