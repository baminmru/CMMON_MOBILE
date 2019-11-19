using System;

namespace Storyboard
{
	public class NetAdress
	{
		public long Id { get; set; }
		public string Url { get; set; }
		public DateTime ModifiedTime { get; set; }
		public NetAdress ()
		{
			Id = -1;
			Url = string.Empty;
		}
		public NetAdress (long id, string url, DateTime modified)
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

