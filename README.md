# Simple Cipher with Menu

This is a console-based C# application demonstrating a simple cipher that:
• Uses three 64-character (512-bit) keys.  
• Shifts bytes based on two keys (Key1 and Key2).  
• Pseudo-randomly permutes the byte order based on a seed derived from a third key (Key3).

Please note that this cipher is purely for demonstration and learning. It is not cryptographically secure.

---

## Table of Contents

1. [Features](#features)
2. [How It Works](#how-it-works)
3. [Requirements](#requirements)
4. [Usage](#usage)
5. [Code Overview](#code-overview)
6. [Disclaimer](#disclaimer)

---

## Features

• Console-based UI with a simple menu.  
• Option to "Encrypt" text, automatically generating three keys.  
• Option to "Decrypt" using user-provided keys and ciphertext.

---

## How It Works

1. The program prompts you to choose between:
   • Encrypt Text  
   • Decrypt Text  
   • Exit

2. When encrypting, the program:
   • Reads the input text from the user.  
   • Generates three random 64-character (512-bit) keys (Key1, Key2, Key3).  
   • Shifts each byte of the text by the numeric values of the corresponding characters in Key1 and Key2 (wrapping around with modulo 256).  
   • Uses a pseudo-random permutation (Fisher-Yates shuffle) based on the sum of the characters in Key3 as the seed.  
   • Converts the shuffled bytes to a Base64-encoded string for output.

3. When decrypting, the program:
   • Expects the user to provide the previously generated ciphertext, as well as Key1, Key2, and Key3.  
   • Decodes the ciphertext from Base64.  
   • Re-creates the same Fisher-Yates permutation using the sum of Key3’s characters as the seed.  
   • Inverts the permutation to restore the order of bytes to their pre-shuffled state.  
   • Subtracts the Key1 and Key2 shifts from each byte (again using modulo 256).  
   • Converts the final byte array back to a UTF-8 string.

---

## Requirements

• .NET 6.0 SDK or higher (or a compatible .NET runtime).  
• A console terminal capable of UTF-8 output.

---

## Usage

1. Clone or download the repository.
2. Navigate to the project folder.
3. Build and run the project with the .NET CLI (example with .NET 6.0):

```bash
dotnet build
dotnet run
```

4. Follow the on-screen instructions:
   • Choose (1) to Encrypt Text -> enter your text -> get the generated keys and encrypted result.  
   • Choose (2) to Decrypt Text -> enter the Base64 string and the three keys generated previously -> get the decrypted original text.  
   • Choose (3) to exit.

---

## Code Overview

Below is a high-level view of the main sections in Program.cs:

1. Menu Options  
   The while loop in Main() handles user selection:
   • (1) EncryptOption()  
   • (2) DecryptOption()  
   • (3) Exit

2. EncryptOption()  
   • Asks user for plaintext.  
   • Generates Key1, Key2, Key3 each with 64 randomly chosen printable ASCII characters.  
   • Calls Encrypt(...) and displays the encrypted text plus the keys.

3. DecryptOption()  
   • Asks user for the Base64-encoded ciphertext.  
   • Asks the user for the three keys.  
   • Calls Decrypt(...) and displays the resulting plaintext.

4. GenerateRandomKey(int length)  
   • Creates a string of <length> printable ASCII characters (from 32 to 126).

5. Encrypt(...)  
   • Converts the plaintext to bytes.  
   • Shifts bytes by Key1 and Key2.  
   • Creates a seed from Key3.  
   • Uses a pseudo-random shuffle (Fisher-Yates) to reorder bytes.  
   • Encodes bytes as Base64.

6. Decrypt(...)  
   • Converts the ciphertext from Base64 to bytes.  
   • Generates the same pseudo-random shuffle as in Encrypt(...).  
   • Inverts the shuffle to restore the original byte order.  
   • Subtracts the byte shifts from Key1 and Key2.  
   • Converts the result back to a string.

---

## Disclaimer

• This application is a purely educational example.  
• It is not cryptographically secure.  
• For real-world encryption, always use industry-standard libraries and algorithms (e.g., AES, RSA, ECC).

Please feel free to experiment with this code for learning and demonstration purposes, but do not rely on it for production or sensitive data.

Enjoy exploring this simple cipher! If you have any questions or need additional clarification, feel free to ask.
