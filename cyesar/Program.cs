using System;

class CaesarCipher
{
    static void Main()
    {
        Console.WriteLine("Caesar Cipher");
        Console.WriteLine("Wil je encoden of decoden? (E/D)");

        string choice = Console.ReadLine().ToUpper();

        Console.WriteLine("Geef de shift (bijvoorbeeld 3):");
        int shift = int.Parse(Console.ReadLine());

        Console.WriteLine("Geef het bericht:");
        string message = Console.ReadLine();

        string result = "";

        if (choice == "E")
        {
            result = Encode(message, shift);
        }
        else if (choice == "D")
        {
            result = Decode(message, shift);
        }
        else
        {
            Console.WriteLine("Ongeldige keuze.");
            return;
        }

        Console.WriteLine("Resultaat:");
        Console.WriteLine(result);
    }

    static string Encode(string text, int shift)
    {
        string result = "";

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                char basis = char.IsUpper(c) ? 'A' : 'a';
                char encoded = (char)((c - basis + shift) % 26 + basis);
                result += encoded;
            }
            else
            {
                result += c; // Spaties en leestekens ongewijzigd
            }
        }

        return result;
    }

    static string Decode(string text, int shift)
    {
        string result = "";

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                char basis = char.IsUpper(c) ? 'A' : 'a';
                char decoded = (char)((c - basis - shift + 26) % 26 + basis);
                result += decoded;
            }
            else
            {
                result += c; // Spaties en leestekens ongewijzigd
            }
        }

        return result;
    }
}