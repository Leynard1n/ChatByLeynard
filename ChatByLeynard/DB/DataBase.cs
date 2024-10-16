using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ChatByLeynard
{
    public class Database
    {
        private MySqlConnection connection;

        public Database(string server, string database, string username, string password)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = server,
                Database = database,
                UserID = username,
                Password = password,
                Port = 3306, // Замените на нужный порт, если необходимо
                CharacterSet = "utf8mb4"
            };

            connection = new MySqlConnection(connectionStringBuilder.ConnectionString);
        }

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public DataTable GetMessages()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM messages ORDER BY id";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public void SendMessage(string text, string from)
        {
            string query = "INSERT INTO messages (text, from_author) VALUES (@text, @from)";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@from", from);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteMessage(int messageId)
        {
            string query = "DELETE FROM messages WHERE id = @id";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", messageId);
                command.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }
}
