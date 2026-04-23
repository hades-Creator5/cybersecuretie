using System;
using System.Threading;
using System.Text;

namespace JWTSimulatie
{
    class Program
    {
        // ===== "DATABASE" =====
        static string correcteGebruikersnaam = "bodhi";
        static string correcteWachtwoord = "bodhi";

        // ===== JWT OPSLAG (lokaal) =====
        static string jwtToken = null;
        static DateTime jwtVerloopTijd;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Login ===");

            // 1. Inloggen
            Console.Write("Gebruikersnaam: ");
            string username = Console.ReadLine();

            Console.Write("Wachtwoord: ");
            string password = Console.ReadLine();

            // 2. Controle login
            if (username != correcteGebruikersnaam || password != correcteWachtwoord)
            {
                Console.WriteLine("Fout: ongeldige gebruikersnaam of wachtwoord.");
                return;
            }

            // 3. JWT aanmaken
            jwtToken = GenereerJWT(username);
            jwtVerloopTijd = DateTime.Now.AddMinutes(1);

            Console.WriteLine("\nLogin succesvol.");
            Console.WriteLine($"JWT ontvangen: {jwtToken}");

            // 4. Actie-loop
            bool actief = true;
            while (actief)
            {
                Console.WriteLine("\nKies een actie:");
                Console.WriteLine("1 - Doe actie");
                Console.WriteLine("2 - Stop");
                Console.Write("Keuze: ");

                string keuze = Console.ReadLine();

                if (keuze == "1")
                {
                    Console.WriteLine("Actie wordt uitgevoerd...");

                    // 4a. JWT controle bij iedere actie
                    if (!IsJWTGeldig())
                    {
                        Console.WriteLine("Fout: JWT is verlopen. Sessie beëindigd.");
                        return;
                    }

                    // Simuleer server verwerking
                    Thread.Sleep(2000);

                    Console.WriteLine("Actie voltooid.");
                }
                else if (keuze == "2")
                {
                    actief = false;
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze.");
                }
            }

            Console.WriteLine("\nApplicatie gestopt.");
        }

        // ===== HULPFUNCTIES =====

        // Maakt een gesimuleerde JWT aan (header.payload.signature)
        static string GenereerJWT(string gebruikersnaam)
        {
            // Header: algoritme en type
            string header = Convert.ToBase64String(
                Encoding.UTF8.GetBytes("{\"alg\":\"HS256\",\"typ\":\"JWT\"}")
            );

            // Payload: gebruikersnaam + vervaltijd (Unix timestamp)
            long exp = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds();
            string payloadJson = $"{{\"sub\":\"{gebruikersnaam}\",\"exp\":{exp}}}";
            string payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(payloadJson));

            // Signature: gesimuleerd (in productie gebruik je HMAC-SHA256 met een geheime sleutel)
            string signature = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{header}.{payload}.geheimesleutel")
            );

            return $"{header}.{payload}.{signature}";
        }

        // Controleert of de lokaal opgeslagen JWT nog geldig is
        static bool IsJWTGeldig()
        {
            if (jwtToken == null)
                return false;

            return DateTime.Now < jwtVerloopTijd;
        }
    }
}