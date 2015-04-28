using ENTech.Store.Infrastructure.Crypt;

namespace ENTech.Store.Infrastructure.Utils
{

	public static class CryptUtils
	{
		/// <summary>Encrypt text</summary>
		/// <param name="text">Text to encrypt</param>
		/// <returns>Encrypted text</returns>
		public static string Encrypt(string text)
		{
			return CryptEngine.Instance.Encrypt(text);
		}

		/// <summary>Decrypt text</summary>
		/// <param name="text">Text to decrypt</param>
		/// <returns>Decrypted text</returns>
		public static string Decrypt(string text)
		{
			return CryptEngine.Instance.Decrypt(text);
		}

		public static string HashText(string text)
		{
			return CryptEngine.Instance.HashText(text);
		}

		public static bool ValidateInput(string text, string validHash)
		{
			return CryptEngine.Instance.ValidateInput(text, validHash);
		}
	}
}
