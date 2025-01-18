namespace Pampero.Tools.Utils
{

#if !UNITY_METRO
    public static class EnumExtensions
	{
		public static bool TryParse<T>(this System.Enum theEnum, string valueToParse, out T returnValue)
		{
			returnValue = default(T);
			if (System.Enum.IsDefined(typeof(T), valueToParse))
			{
				System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
				returnValue = (T)converter.ConvertFromString(valueToParse);
				return true;
			}
			return false;
		}
	}
#endif
}