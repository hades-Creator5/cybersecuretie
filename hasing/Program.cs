using System;
using System.Security.Cryptography;
using System.Text;

namespace CyberSecurityHashing
{
    class Program
    {
        private const string GlobalSalt = "EenHeelGeheimStukjeTekst_123!";

        // Lokale "database"
        static string opgeslagenGebruikersnaam = null;
        static string opgeslagenWachtwoordHash = null;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Beveiligd Systeem ---");
                Console.WriteLine("1 - Aanmelden (Registreren)");
                Console.WriteLine("2 - Inloggen");
                Console.WriteLine("3 - Stoppen");
                Console.Write("Keuze: ");

                string keuze = Console.ReadLine();

                switch (keuze)
                {
                    case "1":
                        RegistreerGebruiker();
                        break;
                    case "2":
                        LoginGebruiker();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ongeldige keuze.");
                        break;
                }
            }
        }

        static void RegistreerGebruiker()
        {
            Console.Write("Kies een gebruikersnaam: ");
            string username = Console.ReadLine();

            Console.Write("Kies een wachtwoord: ");
            string password = Console.ReadLine();


            string hashedPw = HashWachtwoord(password);


            opgeslagenGebruikersnaam = username;
            opgeslagenWachtwoordHash = hashedPw;

            Console.WriteLine("\n[Systeem] Account succesvol aangemaakt.");
        }

        static void LoginGebruiker()
        {
            if (string.IsNullOrEmpty(opgeslagenGebruikersnaam))
            {
                Console.WriteLine("Er is nog geen account geregistreerd!");
                return;
            }

            Console.Write("Gebruikersnaam: ");
            string inputUser = Console.ReadLine();

            Console.Write("Wachtwoord: ");
            string inputPass = Console.ReadLine();


            string inputHash = HashWachtwoord(inputPass + GlobalSalt);


            if (inputUser == opgeslagenGebruikersnaam && inputHash == opgeslagenWachtwoordHash)
            {
                Console.WriteLine("\n[SUCCES] Je bent nu ingelogd!");
            }
            else
            {
                Console.WriteLine("\n[FOUT] Ongeldige gebruikersnaam of wachtwoord.");
            }
        }

        static string HashWachtwoord(string input)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));


                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}