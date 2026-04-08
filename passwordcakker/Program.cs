using System;
using System.Collections.Generic;
using System.Linq; // Nodig voor handige checks zoals Any()

namespace PasswordChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Wachtwoord Sterkte Controle ---");
            Console.WriteLine("Voer een wachtwoord in:");
            string password = Console.ReadLine();

            List<string> fouten = new List<string>();

            // ===== CONTROLES =====

            // TODO 1: Controleer lengte (minimaal 8 tekens)
            if (!IsLengteCorrect(password))
            {
                fouten.Add("Wachtwoord moet minimaal 8 tekens lang zijn.");
            }

            // TODO 2: Controleer op cijfers en kleine letters
            if (!BevatCijfer(password))
            {
                fouten.Add("Wachtwoord moet minimaal één cijfer bevatten.");
            }

            // TODO 3: Controleer op hoofdletters
            if (!BevatHoofdletter(password))
            {
                fouten.Add("Wachtwoord moet minimaal één hoofdletter bevatten.");
            }

            // TODO 4: Controleer op veelgebruikte (zwakke) wachtwoorden
            if (IsVeelGebruiktWachtwoord(password))
            {
                fouten.Add("Dit wachtwoord is te algemeen en makkelijk te raden.");
            }

            // ===== RESULTAAT =====

            if (fouten.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWachtwoord is ONVEILIG:");
                foreach (string fout in fouten)
                {
                    Console.WriteLine("- " + fout);
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nWachtwoord is GOED beveiligd.");
                Console.ResetColor();
            }

            Console.WriteLine("\nDruk op een toets om af te sluiten...");
            Console.ReadKey();
        }

        // ===== HULPFUNCTIES =====

        static bool IsLengteCorrect(string password)
        {
            return password.Length >= 8;
        }

        static bool BevatHoofdletter(string password)
        {
            // Checkt of er minstens één karakter een hoofdletter is
            return password.Any(char.IsUpper);
        }

        static bool BevatCijfer(string password)
        {
            // Checkt of er minstens één karakter een getal is
            return password.Any(char.IsDigit);
        }

        static bool IsVeelGebruiktWachtwoord(string password)
        {
            // Een "Blacklist" van verboden wachtwoorden
            List<string> zwakkeWachtwoorden = new List<string>
            {
                "123456", "wachtwoord", "password", "welkom01", "qwerty", "admin"
            };

            // We zetten alles naar kleine letters voor een eerlijke vergelijking
            return zwakkeWachtwoorden.Contains(password.ToLower());
        }
    }
}