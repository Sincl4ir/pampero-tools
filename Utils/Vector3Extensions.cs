using UnityEngine;

namespace Pampero.Tools.Utils
{
    public static class Vector3Extensions
	{
		public static void Invert(ref this Vector3 a_Vec)
		{
			a_Vec.x = 1f / a_Vec.x;
			a_Vec.y = 1f / a_Vec.y;
			a_Vec.z = 1f / a_Vec.z;
		}

		public static Vector3 Inverse(this Vector3 a_Vec)
		{
			a_Vec.Invert();
			return a_Vec;
		}

		public static Vector3 Divide(in this Vector3 a, in Vector3 b)
		{
			return Vector3.Scale(a, b.Inverse());
		}
	}
}