using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GameCatalogue
{
    public class DatabaseManager
    {
        private readonly string _connectionString = "Data Source=UserSettings.db";

        public DatabaseManager()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            // Profiles table
            string profilesTable = @"
                CREATE TABLE IF NOT EXISTS Profiles (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProfileName TEXT UNIQUE
                );";

            // User settings tied to a profile
            string userSettingsTable = @"
                CREATE TABLE IF NOT EXISTS UserSettings (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProfileId INTEGER,
                    UserName TEXT,
                    SortColumn TEXT,
                    SortAscending INTEGER,
                    FOREIGN KEY(ProfileId) REFERENCES Profiles(Id)
                );";

            // Owned games tied to a profile
            string ownedGamesTable = @"
                CREATE TABLE IF NOT EXISTS OwnedGames (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProfileId INTEGER,
                    GameId INTEGER,
                    Title TEXT,
                    FOREIGN KEY(ProfileId) REFERENCES Profiles(Id)
                );";

            using var cmd0 = new SQLiteCommand(profilesTable, conn);
            cmd0.ExecuteNonQuery();

            using var cmd1 = new SQLiteCommand(userSettingsTable, conn);
            cmd1.ExecuteNonQuery();

            using var cmd2 = new SQLiteCommand(ownedGamesTable, conn);
            cmd2.ExecuteNonQuery();
        }

        // -----------------------------
        // PROFILE MANAGEMENT
        // -----------------------------

        public int CreateProfile(string profileName)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "INSERT INTO Profiles (ProfileName) VALUES (@name); SELECT last_insert_rowid();";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", profileName);

            long id = (long)cmd.ExecuteScalar();
            return (int)id;
        }

        public List<(int id, string name)> LoadProfiles()
        {
            var list = new List<(int, string)>();

            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT Id, ProfileName FROM Profiles";

            using var cmd = new SQLiteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add((
                    Convert.ToInt32(reader["Id"]),
                    reader["ProfileName"].ToString()!
                ));
            }

            return list;
        }

        // -----------------------------
        // USER SETTINGS (PER PROFILE)
        // -----------------------------

        public void SaveUserSettings(int profileId, string name, string sortColumn, bool ascending)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = @"
                INSERT OR REPLACE INTO UserSettings (ProfileId, UserName, SortColumn, SortAscending)
                VALUES (@pid, @name, @column, @asc);";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pid", profileId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@column", sortColumn);
            cmd.Parameters.AddWithValue("@asc", ascending ? 1 : 0);
            cmd.ExecuteNonQuery();
        }

        public (string name, string sortColumn, bool ascending) LoadUserSettings(int profileId)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT UserName, SortColumn, SortAscending FROM UserSettings WHERE ProfileId = @pid";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pid", profileId);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string name = reader["UserName"]?.ToString() ?? "";
                string column = reader["SortColumn"]?.ToString() ?? "Title";
                bool asc = Convert.ToInt32(reader["SortAscending"]) == 1;

                return (name, column, asc);
            }

            return ("", "Title", true);
        }

        // -----------------------------
        // OWNED GAMES (PER PROFILE)
        // -----------------------------

        public void AddOwnedGame(int profileId, int gameId, string title)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "INSERT INTO OwnedGames (ProfileId, GameId, Title) VALUES (@pid, @id, @title)";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pid", profileId);
            cmd.Parameters.AddWithValue("@id", gameId);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.ExecuteNonQuery();
        }

        public List<(int gameId, string title)> LoadOwnedGames(int profileId)
        {
            var list = new List<(int, string)>();

            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            string sql = "SELECT GameId, Title FROM OwnedGames WHERE ProfileId = @pid";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pid", profileId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add((
                    Convert.ToInt32(reader["GameId"]),
                    reader["Title"].ToString()!
                ));
            }

            return list;
        }
    }
}
