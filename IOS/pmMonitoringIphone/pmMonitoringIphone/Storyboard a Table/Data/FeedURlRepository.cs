using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;
using System.Threading.Tasks;

namespace Storyboard
{
	public class FeedURlRepository
	{
		private static string db_file = "FeedUrl.db3";

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
			var sql = "CREATE TABLE ITEMS (Id INTEGER PRIMARY KEY AUTOINCREMENT, Url ntext, Modified datetime);";

			connection.Open ();

			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.ExecuteNonQuery ();
			}


			sql = "INSERT INTO ITEMS (Url, Modified) VALUES (@Url, @Modified);";

			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue ("@Url", "");
				cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

				cmd.ExecuteNonQuery ();
			}

			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue ("@Url", "");
				cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

				cmd.ExecuteNonQuery ();
			}
			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue ("@Url", "");
				cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

				cmd.ExecuteNonQuery ();
			}
			connection.Close ();
		}

		public static IEnumerable<NetAdress> GetAllNetAdress ()
		{
			var sql = "SELECT * FROM ITEMS;";

			using (var conn = GetConnection ()) {
				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = sql;

					using (var reader = cmd.ExecuteReader ()) {
						while (reader.Read ())
							yield return new NetAdress (reader.GetInt32 (0), reader.GetString (1), reader.GetDateTime (2)); 
					}
				}
			}
		}

		public static async Task<NetAdress[]> GetAllNetAdressAsync()
		{
			var sql = "SELECT * FROM ITEMS;";
			List<NetAdress> netadreses = new List<NetAdress> ();
			using (var conn = GetConnection ()) {
				await conn.OpenAsync ();

				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = sql;

					using (var reader = await cmd.ExecuteReaderAsync ()) {
						while (await reader.ReadAsync ())
							netadreses.Add (new NetAdress (reader.GetInt32 (0), reader.GetString (1), reader.GetDateTime (2))); 
					}
				}
			}
			return netadreses.ToArray ();
		}

		public static NetAdress GetNetAdress (long id)
		{

			var sql = string.Format ("SELECT * FROM ITEMS WHERE Id = {0};", id);

			using (var conn = GetConnection ()) {
				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = sql;

					using (var reader = cmd.ExecuteReader ()) {
						if (reader.Read ()) {

							return new NetAdress (reader.GetInt32 (0), reader.GetString (1), reader.GetDateTime (2));
						}
						else
							return null;
					}
				}
			}
		}

		public static async Task<NetAdress> GetNetAdressAsync (long id)
		{
			var sql = string.Format ("SELECT * FROM ITEMS WHERE Id = {0};", id);
			using (var conn = GetConnection ()) {
				await conn.OpenAsync ();

				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = sql;

					using (var reader =await  cmd.ExecuteReaderAsync ()) {
						if (await reader.ReadAsync ()) {

							return new NetAdress (reader.GetInt32 (0), reader.GetString (1), reader.GetDateTime (2)); 
						}
						else
							return null;
					}
				}
			}
		}


		public static void DeleteNetAdress(NetAdress netadress)
		{
			var sql = string.Format ("DELETE FROM ITEMS WHERE Id = {0};", netadress.Id);

			using (var conn = GetConnection ()) {
				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {
					cmd.CommandText = sql;
					cmd.ExecuteNonQuery ();
				}
			}
		}


		public static void SaveNetAdress (NetAdress netadress)
		{
			using (var conn = GetConnection ()) {
				conn.Open ();

				using (var cmd = conn.CreateCommand ()) {
					Console.WriteLine ("Id=" + netadress.Id);
					if (netadress.Id < 0) {
						// Do an insert
						cmd.CommandText = "INSERT INTO ITEMS (Url, Modified) VALUES (@Url, @Modified); SELECT last_insert_rowid();";
						cmd.Parameters.AddWithValue ("@Url", netadress.Url);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

						netadress.Id = (long)cmd.ExecuteScalar ();
						Console.WriteLine (netadress.Id+" save");
					} else {
						// Do an update
						cmd.CommandText = "UPDATE ITEMS SET Url = @Url, Modified = @Modified WHERE Id = @Id";
						cmd.Parameters.AddWithValue ("@Id", netadress.Id);
						cmd.Parameters.AddWithValue ("@Url", netadress.Url);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

						cmd.ExecuteNonQuery ();
					}
				}
			}
		}

		public static async Task SaveNetAdressAsync (NetAdress netadress)
		{
			using (var conn = GetConnection ()) {
				await conn.OpenAsync ();

				using (var cmd = conn.CreateCommand ()) {

					if (netadress.Id < 0) {
						// Do an insert
						cmd.CommandText = "INSERT INTO ITEMS (Body, Modified) VALUES (@Body, @Modified); SELECT last_insert_rowid();";
						cmd.Parameters.AddWithValue ("@Body", netadress.Url);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

						netadress.Id = (long)await cmd.ExecuteScalarAsync ();//cmd.ExecuteScalar ();
					} else {
						// Do an update
						cmd.CommandText = "UPDATE ITEMS SET Body = @Body, Modified = @Modified WHERE Id = @Id";
						cmd.Parameters.AddWithValue ("@Id", netadress.Id);
						cmd.Parameters.AddWithValue ("@Body", netadress.Url);
						cmd.Parameters.AddWithValue ("@Modified", DateTime.Now);

						await cmd.ExecuteNonQueryAsync ();
					}
				}
			}
		}
	}
}

