using UnityEngine;

namespace Pampero.Tools.Utils
{
#if !UNITY_METRO
    public static class StringExtensions
	{
		public static string ToTitleCase(this string str)
		{
			return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
		}

		public static string ToCamelCase(this string str)
		{
			return System.Text.RegularExpressions.Regex.Replace(
				System.Text.RegularExpressions.Regex.Replace(
					str,
					@"(\P{Ll})(\P{Ll}\p{Ll})",
					"$1 $2"
				),
				@"(\p{Ll})(\P{Ll})",
				"$1 $2"
			);
		}

		public static int CountString(this string s, string str)
		{
			return (s.Length - s.Replace(str, "").Length) / str.Length;
		}

		public static int GetStableHashCode(this string str)
		{
			unchecked
			{
				int hash1 = (5381 << 16) + 5381;
				int hash2 = hash1;

				for (int i = 0; i < str.Length; i += 2)
				{
					hash1 = ((hash1 << 5) + hash1) ^ str[i];
					if (i == str.Length - 1)
						break;
					hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
				}

				return hash1 + (hash2 * 1566083941);
			}
		}

		public static bool IsValidPath(this string str)
		{
			if (str.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
			{
				return false;
			}

			return true;
		}

		public enum SizeUnits
		{
			Byte, KB, MB, GB, TB, PB, EB, ZB, YB
		}

		public static string ToSize(this long value, SizeUnits unit)
		{
			return (value / (double)Mathf.Pow(1024, (long)unit)).ToString("0.00");
		}


		// Adapted from:
		// https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
		public static string FormatVariableName(this string a_VariableName)
		{
			if (a_VariableName.StartsWith("m_"))
				a_VariableName = a_VariableName.Substring(2);

			if (string.IsNullOrWhiteSpace(a_VariableName))
				return string.Empty;

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append(a_VariableName[0]);
			for (int i = 1; i < a_VariableName.Length; i++)
			{
				if (char.IsUpper(a_VariableName[i]))
				{
					if ((a_VariableName[i - 1] != ' ' && !char.IsUpper(a_VariableName[i - 1])) || (char.IsUpper(a_VariableName[i - 1]) && (i + 1) < a_VariableName.Length && !char.IsUpper(a_VariableName[i + 1])))
					{
						sb.Append(' ');
					}
				}

				sb.Append(a_VariableName[i]);
			}

			return sb.ToString();
		}

		public static string RemoveFromEnd(this string a_String, int a_NumberOfCharactersToRemove)
		{
			return a_String.Remove(a_String.Length - a_NumberOfCharactersToRemove);
		}
	}
#endif
}
//EOF.