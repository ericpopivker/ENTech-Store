using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ENTech.Store.Infrastructure.Crypt
{
	/// <summary>
	/// Summary description for XCrypt.
	/// </summary>
	/// <summary>
	/// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
	/// decrypt data. As long as encryption and decryption routines use the same
	/// parameters to generate the keys, the keys are guaranteed to be the same.
	/// </summary>
	internal sealed class CryptEngine
	{
		private const string Salt = "15445FC6-0120-4C69-8890-C366008C5002";

		/// <summary>
		/// Hash algorithm used to generate password. Allowed values are: "MD5" and
		/// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
		/// </summary>
		private readonly string _hashAlgorithm;

		/// <summary>
		/// Number of iterations used to generate password. One or two iterations
		/// should be enough.
		/// </summary>
		private readonly int _passwordIterations;

		/// <summary>
		/// Initialization vector (or IV). This value is required to encrypt the
		/// first block of plaintext data. For RijndaelManaged class IV must be 
		/// exactly 16 ASCII characters long.
		/// </summary>
		private readonly string _initVector;

		/// <summary>
		/// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
		/// Longer keys are more secure than shorter keys.
		/// </summary>
		private readonly int _keySize;

		/// <summary>
		/// Salt value used along with passphrase to generate password. Salt can
		/// be any string. In this example we assume that salt is an ASCII string.
		///</summary>
		private readonly string _saltValue;

		//Key used for encryption and decryption
		private string _key;

		#region Class Properties

		/// <summary>
		/// Passphrase from which a pseudo-random password will be derived. The
		/// derived password will be used to generate the encryption key.
		/// Passphrase can be any string. In this example we assume that this
		/// passphrase is an ASCII string.
		/// </summary>
		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		#endregion

		/// <summary>Create new instance of XCrypto</summary>
		private CryptEngine()
		{
			//Initialize parameters used in crypto algorithm
			_hashAlgorithm = "SHA1"; // can be "MD5"
			_passwordIterations = 42; // can be any number
			_initVector = "1EA09552-1427-48"; // must be 16 bytes
			_keySize = 256; // can be 256 or 192 or 128
			_saltValue = Salt; // can be any string
			_key = "81C2B703FD25";
		}

		internal static CryptEngine Instance = new Lazy<CryptEngine>(() => new CryptEngine()).Value;

		/// <summary>Encrypts specified plaintext using Rijndael symmetric key algorithm and returns a base64-encoded result. </summary>
		/// <param name="text">Plaintext value to be encrypted.</param>
		/// <returns>Encrypted value formatted as a base64-encoded string.</returns>
		internal string Encrypt(string text)
		{
			// Convert strings into byte arrays.
			// Let us assume that strings only contain ASCII codes.
			// If strings include Unicode characters, use Unicode, UTF7, or UTF8 
			// encoding.
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(_initVector);

			// Convert our plaintext into a byte array.
			// Let us assume that plaintext contains UTF8-encoded characters.
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);

			var keyBytes = GetKeyBytes();

			// Create uninitialized Rijndael encryption object.
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey = InitializeRijndaelObject(symmetricKey);

			// Generate encryptor from the existing key bytes and initialization 
			// vector. Key size will be defined based on the number of the key 
			// bytes.
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
				keyBytes,
				initVectorBytes);

			// Define memory stream which will be used to hold encrypted data.
			MemoryStream memoryStream = new MemoryStream();

			// Define cryptographic stream (always use Write mode for encryption).
			CryptoStream cryptoStream = new CryptoStream(memoryStream,
														encryptor,
														CryptoStreamMode.Write);
			// Start encrypting.
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

			// Finish encrypting.
			cryptoStream.FlushFinalBlock();

			// Convert our encrypted data from a memory stream into a byte array.
			byte[] cipherTextBytes = memoryStream.ToArray();

			// Close both streams.
			memoryStream.Close();
			cryptoStream.Close();

			// Convert encrypted data into a base64-encoded string.
			string cipherText = Convert.ToBase64String(cipherTextBytes);

			// Return encrypted string.
			return cipherText;
		}

		/// <summary>Decrypts specified ciphertext using Rijndael symmetric key algorithm.</summary>
		/// <param name="text">Base64-formatted ciphertext value.</param>
		/// <returns>
		/// Decrypted string value.
		/// </returns>
		/// <remarks>
		/// Most of the logic in this function is similar to the Encrypt
		/// logic. In order for decryption to work, all parameters of this function
		/// - except cipherText value - must match the corresponding parameters of
		/// the Encrypt function which was called to generate the
		/// ciphertext.
		/// </remarks>
		/// 
		internal string Decrypt(string text)
		{
			// Convert strings defining encryption key characteristics into byte
			// arrays. Let us assume that strings only contain ASCII codes.
			// If strings include Unicode characters, use Unicode, UTF7, or UTF8
			// encoding.
			var initVectorBytes = Encoding.ASCII.GetBytes(_initVector);

			// Convert our ciphertext into a byte array.
			byte[] cipherTextBytes = Convert.FromBase64String(text);

			var keyBytes = GetKeyBytes();
			
			// Create uninitialized Rijndael encryption object.
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey = InitializeRijndaelObject(symmetricKey);

			// Generate decryptor from the existing key bytes and initialization 
			// vector. Key size will be defined based on the number of the key 
			// bytes.
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
				keyBytes,
				initVectorBytes);

			// Define memory stream which will be used to hold encrypted data.
			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

			// Define cryptographic stream (always use Read mode for encryption).
			CryptoStream cryptoStream = new CryptoStream(memoryStream,
														decryptor,
														CryptoStreamMode.Read);

			// Since at this point we don't know what the size of decrypted data
			// will be, allocate the buffer long enough to hold ciphertext;
			// plaintext is never longer than ciphertext.
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];

			// Start decrypting.
			int decryptedByteCount = cryptoStream.Read(plainTextBytes,
														0,
														plainTextBytes.Length);

			// Close both streams.
			memoryStream.Close();
			cryptoStream.Close();

			// Convert decrypted data into a string. 
			// Let us assume that the original plaintext string was UTF8-encoded.
			string plainText = Encoding.UTF8.GetString(plainTextBytes,
														0,
														decryptedByteCount);

			// Return decrypted string.   
			return plainText;
		}

		internal string HashText(string text)
		{
			var salt = GenerateRandom256BitSalt();
			var hash = GenerateSHA256BitHash(salt + text);

			return salt + hash;
		}

		internal bool ValidateInput(string input, string validHash)
		{
			if (validHash.Length != 128)
				throw new ArgumentException("Parameter validHash must be 128 hexadecimal characters long.");

			var saltPartOfValidHash = validHash.Substring(0, 64);
			var hashPartOfValidHash = validHash.Substring(64, 64);

			var inputHash = GenerateSHA256BitHash(saltPartOfValidHash + input);

			return String.CompareOrdinal(hashPartOfValidHash, inputHash) == 0;
		}

		private string GenerateRandom256BitSalt()
		{
			var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
			var random256BitSalt = new byte[32];

			rngCryptoServiceProvider.GetBytes(random256BitSalt);
			return ByteSequenceToHexString(random256BitSalt);
		}

		private string GenerateSHA256BitHash(string textToHash)
		{
			var sha256Managed = new SHA256Managed();
			var utf8ByteSequence = Encoding.UTF8.GetBytes(textToHash);

			return ByteSequenceToHexString(sha256Managed.ComputeHash(utf8ByteSequence));
		}

		private static string ByteSequenceToHexString(byte[] byteSequence)
		{
			var stringBuilder = new StringBuilder(byteSequence.Length * 2);

			for (int i = 0; i < byteSequence.Length; i++)
				stringBuilder.Append(byteSequence[i].ToString("X2"));

			return stringBuilder.ToString();
		}

		private RijndaelManaged InitializeRijndaelObject(RijndaelManaged algorithm)
		{
			// It is reasonable to set encryption mode to Cipher Block Chaining
			// (CBC). Use default options for other symmetric key parameters.
			algorithm.Mode = CipherMode.CBC;
			algorithm.Padding = PaddingMode.PKCS7;
			return algorithm;
		}

		private byte[] GetKeyBytes()
		{
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(_saltValue);

			// First, we must create a password, from which the key will be 
			// derived. This password will be generated from the specified 
			// passphrase and salt value. The password will be created using
			// the specified hash algorithm. Password creation can be done in
			// several iterations.
			PasswordDeriveBytes password = new PasswordDeriveBytes(
				_key,
				saltValueBytes,
				_hashAlgorithm,
				_passwordIterations);


			// Use the password to generate pseudo-random bytes for the encryption
			// key. Specify the size of the key in bytes (instead of bits).
			byte[] keyBytes = password.GetBytes(_keySize / 8);

			return keyBytes;
		}
	}
}
