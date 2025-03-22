using Microsoft.Data.Sqlite;

string connectionString = @"Data Source =habit-Tracker.db";

using (var connection = new SqliteConnection(connectionString))
{
  connection.Open();
  var tableCmd = connection.CreateCommand();

  tableCmd.CommandText = "";

  tableCmd.ExecuteNonQuery();

  connection.Close()
}

