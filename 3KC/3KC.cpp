#include <iostream>
#include <string>
#include <vector>

std::string encrypt(std::string message, std::string key1, std::string key2, std::string key3) {
	std::vector<char> tbytes(message.size());
	std::string shift;
	//step 1
	std::copy(message.begin(), message.end(), tbytes.begin());
	for (int i = 0; i < tbytes.size(); i++)
	{
		char shift1 = (char)key1[i % key1.size()];
		char shift2 = (char)key2[i % key2.size()];
		tbytes[i] = (char)((tbytes[i] + shift1 + shift2) % 256);
	}
	std::cout << "step1 complete\n";

	//step 2
	int seed = 0;
	for(auto ch:key3)
	{
		seed += ch;
	}
	std::cout << "seed complete\n";
	srand(seed);//i would never trust the standard c random but this will do the job
	int length = tbytes.size();
	int indices[length];
	for (int i = 0; i < length; i++)
	{
		indices[i] = i;
	}
	std::cout << "substep complete\n";
	for (int i = length - 1; i > 0; i--) {
        int swapIndex = rand() % (i + 1);
        std::swap(indices[i], indices[swapIndex]);
    }//burda c# ile aynı şekilde kopyalarsam hata veriyordu std swap çözdü.
	char shuffledBytes[length];
	std::cout << "substep complete\n";
	for (int i = 0; i < length; i++)
	{
		shuffledBytes[i] = tbytes[indices[i]];
	}

	std::cout << "step2 complete\n";
	// Return as Base64
	return (shuffledBytes);

}

int main() {
	std::string key1, key2, key3;
	std::string text;
	std::cin >> key1;
	std::cin >> key2;
	std::cin >> key3;
	std::cin >> text;
	std::cout << encrypt(text, key1, key2, key3) << "\n";

}




/* 22mins wasted here on translating and trying to read c# syntax...
static string Encrypt(string ? text, string key1, string key2, string key3)
{
	// Convert the plain text to UTF-8 bytes
	byte[] textBytes = Encoding.UTF8.GetBytes(text ? ? "");

	// Step 1: Shift each byte by key1 and key2
	for (int i = 0; i < textBytes.Length; i++)
	{
		byte shift1 = (byte)key1[i % key1.Length];
		byte shift2 = (byte)key2[i % key2.Length];
		textBytes[i] = (byte)((textBytes[i] + shift1 + shift2) % 256);
	}

	// Step 2: Generate a pseudo-random permutation based on key3
	int seed = 0;
	foreach(var ch in key3)
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
}*/