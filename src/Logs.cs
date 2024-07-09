using Godot;
using System;

public class Logs
{
	public static void Print(string msg)
	{
		GD.Print(TimeStamp() + "\t\t" + msg);
	}

	public static string TimeStamp()
	{
		ulong ms = Time.GetTicksMsec();
		TimeSpan ts = TimeSpan.FromMilliseconds(ms);
		return ts.ToString("g");
	}

	public static string GetDateTime()
	{
		return "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff");
	}
}
