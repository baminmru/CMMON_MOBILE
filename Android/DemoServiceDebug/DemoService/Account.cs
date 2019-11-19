using System;

namespace DemoService
{
	public class Account : Java.Lang.Object
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public DateTime ModifiedTime { get; set; }
		public Account ()
		{
			Id = -1;
			Login = string.Empty;
			Password = string.Empty;
		}
		public Account (long id, string login, string password, DateTime modified)
		{
			Id = id;
			Login = login;
			Password = password;
			ModifiedTime = modified;
		}
		public override string ToString ()
		{
			return ModifiedTime.ToString ();
		}
	}
}

