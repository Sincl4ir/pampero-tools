using UnityEngine;

namespace Pampero.Tools.Utils
{
    public static class GameObjectExtensions
	{
		public static T GetOrAddComponent<T>(this GameObject a_GameObject) where T : Component
		{
			T component = a_GameObject.GetComponent<T>();

			if (component == null)
				component = a_GameObject.AddComponent<T>();


			return component;
		}
	}
}