namespace Pampero.Tools.Utils
{
    public static class DateTimeExtensions
	{
		/// <summary>
		/// Retruns a datetime starting at 12:00am of the passed in datetime
		/// </summary>
		public static System.DateTime StartOfDay(this System.DateTime dt)
		{
			return new System.DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, dt.Kind);
		}
	}
}