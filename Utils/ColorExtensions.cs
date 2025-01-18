using UnityEngine;

namespace Pampero.Tools.Utils
{
    public static class ColorExtensions
	{
		public static int ToHex(this Color32 color)
		{
			int hex = 0;
			hex |= color.r << 16 | color.g << 8 | color.b;
			return hex;
		}

		public static Color32 FromHex(int hex)
		{
			byte R = (byte)((hex >> 16) & 0xFF);
			byte G = (byte)((hex >> 8) & 0xFF);
			byte B = (byte)((hex) & 0xFF);
			return new Color32(R, G, B, 255);
		}
	}
}