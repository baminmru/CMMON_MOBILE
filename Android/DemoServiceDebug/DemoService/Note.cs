using System;

namespace DemoService
{
	public class Note : Java.Lang.Object
	{
		public long Id { get; set; }
		public string Url { get; set; }
		public DateTime ModifiedTime { get; set; }
		public Note ()
		{
			Id = -1;
			Url = string.Empty;
		}
		public Note (long id, string url, DateTime modified)
		{
			Id = id;
			Url = url;
			ModifiedTime = modified;
		}
		public override string ToString ()
		{
			return ModifiedTime.ToString ();
		}
	}
}

