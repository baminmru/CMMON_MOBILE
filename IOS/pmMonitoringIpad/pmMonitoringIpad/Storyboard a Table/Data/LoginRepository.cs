using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;
using System.Threading.Tasks;

namespace Storyboard
{
	public class LoginRepository
	{
		private static string db_file = "login.db3";

		private static SqliteConnection GetConnection ()
		{
			var dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), db_file);
			bool exists = File.Exists (dbPath);

			if (!exists)
				SqliteConnection.CreateFile (dbPath);

			var conn = new SqliteConnection ("Data Source=" + dbPath);

			if (!exists)
				CreateDatabase (conn);

			return conn;
		}

		private static void CreateDatabase (SqliteConnection connection)
		{
			var sql = "CREATE TABLE ACCOUNT (Id INTEGER PRIMARY KEY AUTOINCREMENT, login ntext, password ntext, Modified datetime);";

			connection.Open ();

			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.ExecuteNonQuery ();
			}

			// Create a sample note to get the user started
			sql = "INSERT INTO ACCOUNT (Login, Password, Modified) VALUES (@Login, @Password, @Modified);";

			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue ("@Login", "");
				cmd.Parameters.AddWithValue ("@Password", "");
				cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

				cmd.ExecuteNonQuery ();
			}

			connection.Close ();
		}



		public static Account GetAccount (long id)
		{

			var sql = string.Format ("SELECT * FROM ACCOUNT WHERE Id = {0};", id);

			using (var conn = GetConnection ()) {
				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = sql;

					using (var reader = cmd.ExecuteReader ()) {
						if (reader.Read ()) {

							return new Account (reader.GetInt32 (0), reader.GetString (1), reader.GetString(2),reader.GetDateTime (3));
						}
						else
							return null;
					}
				}
			}
		}

		public static async Task<Account> GetAccountAsync (long id)
		{
			var sql = string.Format ("SELECT * FROM ACCOUNT WHERE Id = {0};", id);
			using (var conn = GetConnection ()) {
				await conn.OpenAsync ();

				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = sql;

					using (var reader =await  cmd.ExecuteReaderAsync ()) {
						if (await reader.ReadAsync ()) {
							Console.WriteLine ("Id in getnote iz BD=" + reader.GetInt32 (0));
							return new Account (reader.GetInt32 (0), reader.GetString (1),reader.GetString(2), reader.GetDateTime (2)); 
						}
						else
							return null;
					}
				}
			}
		}



		public static void SaveAccount (Account account)
		{
			using (var conn = GetConnection ()) {
				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {

					if (account.Id < 0) {
						// Do an insert
						cmd.CommandText = "INSERT INTO ACCOUNT (Login, Password, Modified) VALUES (@Login, @Password @Modified); SELECT last_insert_rowid();";
						cmd.Parameters.AddWithValue ("@Login", account.Login);
						cmd.Parameters.AddWithValue ("@Password", account.Password);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

					} else {
						// Do an update
						cmd.CommandText = "UPDATE ACCOUNT SET Login = @Login, Password = @Password, Modified = @Modified WHERE Id = @Id";
						cmd.Parameters.AddWithValue ("@Id", account.Id);
						cmd.Parameters.AddWithValue ("@Login", account.Login);
						cmd.Parameters.AddWithValue ("@Password", account.Password);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

						cmd.ExecuteNonQuery ();
					}
				}
			}
		}

		public static async Task SaveAccountAsync (Account account)
		{
			using (var conn = GetConnection ()) {
				await conn.OpenAsync ();

				using (var cmd = conn.CreateCommand ()) {

					if (account.Id < 0) {
						// Do an insert
						cmd.CommandText = "INSERT INTO ACCOUNT (Login, Password, Modified) VALUES (@Login, @Password @Modified); SELECT last_insert_rowid();";
						cmd.Parameters.AddWithValue ("@Body", account.Login);
						cmd.Parameters.AddWithValue ("@Body", account.Password);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

						account.Id = (long)await cmd.ExecuteScalarAsync ();//cmd.ExecuteScalar ();
					} else {
						// Do an update
						cmd.CommandText = "UPDATE ACCOUNT SET Login = @Login, Password = @Password, Modified = @Modified WHERE Id = @Id";
						cmd.Parameters.AddWithValue ("@Id", account.Id);
						cmd.Parameters.AddWithValue ("@Body", account.Login);
						cmd.Parameters.AddWithValue ("@Body", account.Password);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

						await cmd.ExecuteNonQueryAsync ();
					}
				}
			}
		}
	}
}

