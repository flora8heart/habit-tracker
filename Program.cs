using System.Data;
using Microsoft.Data.Sqlite;

string connectionString = @"Data Source =habit-Tracker.db";

using (var connection = new SqliteConnection(connectionString))
{
  connection.Open();
  var sqlCmd = connection.CreateCommand();

  sqlCmd.CommandText =
    @"CREATE TABLE IF NOT EXISTS drinking_water (
      Id INTEGER PRIMARY KEY AUTOINCREMENT,
      Date TEXT,
      Quantity INTEGER
    )";

  sqlCmd.ExecuteNonQuery();

  connection.Close(); // not needed when using "using" to open connection https://www.sqlitetutorial.net/sqlite-csharp/connect/
}

GetUserInput();

void GetUserInput()
{
  Console.Clear();
  bool closeApp = false;
  while (closeApp == false)
  {
    Console.WriteLine("\n\nMAIN MENU");
    Console.WriteLine("\nWhat would you like to do?");
    Console.WriteLine("\nType q to Close Application.");
    Console.WriteLine("Type v to View All Records.");
    Console.WriteLine("Type i to Insert Record.");
    Console.WriteLine("Type d to Delete Record");
    Console.WriteLine("Type u to Update Record");
    Console.WriteLine("--------------------------------------\n");

    string userInput = Console.ReadLine();

    switch (userInput)
    {
      // case "q":
      //   Console.WriteLine("\nGoodbye!\n");
      //   closeApp = true;
      //   break;
      // case "v":
      //   // view all Records
      //   ViewAllRecords();
      //   break;
      case "i":
        // insert Record
        Insert();
        break;
        // case "u":
        //   // update Record
        //   Update();
        //   break;
        // default:
        //   Console.WriteLine("Invalid Command. Please type q, v, i or u\n");
        //   break;
    }
  }
}
void Insert()
{
  // get date from user
  string date = GetDateInput();

  // get number of water drank in terms of glasses
  int quantity = GetNumberInput("\n\nPlease insert number of glasses(no decimals allowed)\n\n");

  // insert into database
  using (var connection = new SqliteConnection(connectionString))
  {
    connection.Open();
    var sqlCmd = connection.CreateCommand();
    sqlCmd.CommandText =
      @"INSERT INTO drinking_water(date, quantity)
      VALUES(@date, @quantity
      )";

    // prevent sql injection
    sqlCmd.Parameters.AddWithValue("@date", date);
    sqlCmd.Parameters.AddWithValue("@quantity", quantity);

    sqlCmd.ExecuteNonQuery();
  }


}

string GetDateInput()
{
  Console.WriteLine("\n\nPlease insert the date: (Formate: dd-mm-yy). Type q to return to main menu");

  string dateInput = Console.ReadLine();

  // press q to return to main menu
  if (dateInput == "q")
  {
    GetUserInput();
  }

  return dateInput;
}

int GetNumberInput(string message)
{
  Console.WriteLine(message);

  string numberInput = Console.ReadLine();

  if (numberInput == "q") GetUserInput();

  int finalInput = Convert.ToInt32(numberInput);

  return finalInput;
}

// void ViewAllRecords()
// {
//   Console.Clear();
//   using (var connection = new SqliteConnection(connectionString))
//   {
//     connection.Open();
//     var sqlCmd = connection.CreateCommand();

//     sqlCmd.CommandText =
//       "SELECT * FROM drinking_water";

//     sqlCmd.ExecuteNonQuery();
//   }
// }