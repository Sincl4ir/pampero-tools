using UnityEngine;

namespace Pampero.Tools.Utils
{
    public static class TimeSpanExtensions
	{
		public static string ToNiceLongString(this System.TimeSpan timespan)
		{
			string s = "";

			if (Mathf.Abs((int)timespan.TotalDays) > 0)
			{
				// Will display as  "x Days, x Hours"
				s = ((int)timespan.TotalDays).ToString() + " day" + (Mathf.Abs((int)timespan.TotalDays) != 1 ? "s" : "");

				if (timespan.Hours != 0)
					s += ", " + timespan.Hours.ToString() + " hours";
			}
			else if (timespan.Hours > 0)
			{
				// Will display as  "x Hours, x Minutes"
				if (timespan.Hours != 1)
					s = timespan.Hours.ToString() + " hours";
				else
					s = "1 hour";

				if (timespan.Minutes > 0)
				{
					if (timespan.Minutes != 1)
						s += ", " + timespan.Minutes.ToString() + " minutes";
					else
						s += ", 1 minute";
				}
			}
			else if (timespan.Minutes > 0)
			{
				// Will display as  "x minutes, x seconds"
				if (timespan.Minutes != 1)
					s = timespan.Minutes.ToString() + " minutes";
				else
					s = "1 minute";

				if (timespan.Seconds > 0)
				{
					if (timespan.Seconds != 1)
						s += ", " + timespan.Seconds.ToString() + " seconds";
					else
						s += ", 1 second";
				}
			}
			else if (timespan.Seconds > 0)
			{
				// Will display as  "x seconds"

				if (timespan.Seconds != 1)
					s = timespan.Seconds.ToString() + " seconds";
				else
					s = "1 second";
			}
			else
			{
				return "0 seconds";
			}

			return s;
		}
	}
}