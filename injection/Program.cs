using Microsoft.Data.Sqlite;

while (true)
{
    Console.WriteLine("=== Game Library ===");
    Console.Write("Zoek een game op titel (of typ 'stop' om af te sluiten): ");
    string userInput = Console.ReadLine() ?? string.Empty;

    if (userInput.ToLower() == "stop")
        break;

    string connectionString = "Data Source=games.db;";
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();

        string query = "SELECT title, genre, year FROM games WHERE title = @title";

        using (var command = new SqliteCommand(query, connection))
        {
            command.Parameters.AddWithValue("@title", userInput);

            using (var reader = command.ExecuteReader())
            {
                bool gevonden = false;
                while (reader.Read())
                {
                    gevonden = true;
                    Console.WriteLine("\nGame gevonden:");
                    Console.WriteLine("Titel: " + reader["title"]);
                    Console.WriteLine("Genre: " + reader["genre"]);
                    Console.WriteLine("Jaar: " + reader["year"]);
                }
                if (!gevonden)
                    Console.WriteLine("\nGeen games gevonden.");
            }
        }
    }

    Console.WriteLine(); // lege regel tussen zoekopdrachten
}

Console.WriteLine("\nDruk op een toets om af te sluiten...");
Console.ReadKey();