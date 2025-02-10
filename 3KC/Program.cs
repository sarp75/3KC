using System.Text;
using _2ds;

namespace _3KC
{
#pragma warning disable CS8604
#pragma warning disable CS8602
	static class Program
	{
		static void Main()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Simple Cipher with Menu");
				Console.WriteLine("1. Encrypt Text");
				Console.WriteLine("2. Decrypt Text");
				Console.WriteLine("3. Exit");
				Console.Write("Select an option: ");

				string? choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						EncryptOption();
						break;
					case "2":
						DecryptOption();
						break;
					case "3":
						Console.WriteLine("Goodbye!");
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}

				Console.WriteLine();
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
			}
		}

		/// <summary>
		/// UI option to encrypt user-provided text.
		/// </summary>
		static void EncryptOption()
		{
			Console.Write("Enter text to encrypt (len:16): ");
			string? text = Console.ReadLine();
			char[] textchar = text.ToCharArray();
			Console.Write("Enter Key (len=16): ");
			string? key1 = Console.ReadLine();
			char[] keychar = key1.ToCharArray();
			Dismensional dismensional = new Dismensional();
			dismensional.ArrayCommon(textchar, keychar);
			/*
			Console.Clear();
			Console.WriteLine("=== Encrypt Text ===");
			Console.Write("Enter text to encrypt: ");
			string? text = Console.ReadLine();

			// Generate three keys (64-character strings = 512 bits)
			string key1 = GenerateRandomKey(64);
			string key2 = GenerateRandomKey(64);
			string key3 = GenerateRandomKey(64);

			// Perform encryption
			string encryptedText = Encrypt(text, key1, key2, key3);

			Console.WriteLine("\nGenerated Keys:");
			Console.WriteLine($"Key1 (Random Pattern): {key1}");
			Console.WriteLine($"Key2 (Weight Pattern): {key2}");
			Console.WriteLine($"Key3 (Shuffle Pattern): {key3}");

			Console.WriteLine($"\nEncrypted Text: {encryptedText}");*/
		}

		/// <summary>
		/// UI option to decrypt user-provided text.
		/// </summary>
		static void DecryptOption()
		{
			Console.Clear();
			Console.WriteLine("=== Decrypt Text ===");
			Console.Write("Enter text to decrypt: ");
			string? encrypted = Console.ReadLine();

			Console.Write("Enter Key1 (Random Pattern): ");
			string? key1 = Console.ReadLine();

			Console.Write("Enter Key2 (Weight Pattern): ");
			string? key2 = Console.ReadLine();

			Console.Write("Enter Key3 (Shuffle Pattern): ");
			string? key3 = Console.ReadLine();

			// Perform decryption
			string decryptedText = Decrypt(encrypted, key1, key2, key3);
			Console.WriteLine($"\nDecrypted Text: {decryptedText}");
		}

		/// <summary>
		/// Generates a random string of ASCII characters of the specified length.
		/// </summary>
		private static string GenerateRandomKey(int length)
		{
			var random = new Random();
			var sb = new StringBuilder();
			// Use a printable ASCII range to keep it UTF-8 friendly
			for (int i = 0; i < length; i++)
			{
				// 32 to 126 covers most standard printable ASCII characters
				sb.Append((char)random.Next(32, 127));
			}

			return sb.ToString();
		}

		/// <summary>
		/// Encrypts the given text using a simple shift (key1 + key2) and a pseudo-random permutation based on key3.
		/// </summary>
		static string Encrypt(string? text, string key1, string key2, string key3)
		{
			// Convert the plain text to UTF-8 bytes
			byte[] textBytes = Encoding.UTF8.GetBytes(text ?? "");

			// Step 1: Shift each byte by key1 and key2
			for (int i = 0; i < textBytes.Length; i++)
			{
				byte shift1 = (byte)key1[i % key1.Length];
				byte shift2 = (byte)key2[i % key2.Length];
				textBytes[i] = (byte)((textBytes[i] + shift1 + shift2) % 256);
			}

			// Step 2: Generate a pseudo-random permutation based on key3
			int seed = 0;
			foreach (var ch in key3)
			{
				seed += ch;
			}

			Random rand = new Random(seed);
			int length = textBytes.Length;
			int[] indices = new int[length];
			for (int i = 0; i < length; i++)
			{
				indices[i] = i;
			}

			// Fisher-Yates shuffle
			for (int i = length - 1; i > 0; i--)
			{
				int swapIndex = rand.Next(i + 1);
				(indices[i], indices[swapIndex]) = (indices[swapIndex], indices[i]);
			}

			// Rearrange textBytes according to permuted indices
			byte[] shuffledBytes = new byte[length];
			for (int i = 0; i < length; i++)
			{
				shuffledBytes[i] = textBytes[indices[i]];
			}

			// Return as Base64
			return Convert.ToBase64String(shuffledBytes);
		}

		/// <summary>
		/// Decrypts the text using the same keys:
		/// 1) Convert from Base64.
		/// 2) Generate the same permutation.
		/// 3) Invert the permutation.
		/// 4) Reverse the shifts from key1, key2.
		/// </summary>
		static string Decrypt(string? encrypted, string? key1, string? key2, string? key3)
		{
			if (string.IsNullOrEmpty(encrypted) ||
			    string.IsNullOrEmpty(key1) ||
			    string.IsNullOrEmpty(key2) ||
			    string.IsNullOrEmpty(key3))
			{
				return "One or more required inputs are null or empty.";
			}

			byte[] shuffledBytes;
			try
			{
				shuffledBytes = Convert.FromBase64String(encrypted);
			}
			catch
			{
				return "Invalid Base64 input.";
			}

			// Same pseudo-random permutation from key3
			int seed = 0;
			foreach (var ch in key3)
			{
				seed += ch;
			}

			Random rand = new Random(seed);
			int length = shuffledBytes.Length;
			int[] indices = new int[length];
			for (int i = 0; i < length; i++)
			{
				indices[i] = i;
			}

			// Fisher-Yates shuffle (to replicate encryption order)
			for (int i = length - 1; i > 0; i--)
			{
				int swapIndex = rand.Next(i + 1);
				(indices[i], indices[swapIndex]) = (indices[swapIndex], indices[i]);
			}

			// Invert the shuffle: place shuffledBytes[i] back at indices[i]
			byte[] unshuffled = new byte[length];
			for (int i = 0; i < length; i++)
			{
				unshuffled[indices[i]] = shuffledBytes[i];
			}

			// Reverse the shift by (key1 + key2)
			for (int i = 0; i < unshuffled.Length; i++)
			{
				byte shift1 = (byte)key1[i % key1.Length];
				byte shift2 = (byte)key2[i % key2.Length];
				unshuffled[i] = (byte)((unshuffled[i] - shift1 - shift2 + 512) % 256);
			}

			// Convert to a string
			return Encoding.UTF8.GetString(unshuffled);
		}
	}
#pragma warning restore CS8602
#pragma warning restore CS8604
}