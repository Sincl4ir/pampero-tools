using UnityEngine;
using System.Collections;

namespace Pampero.Tools.Utils
{
    public static class MonobehaviourExtentions
	{
		public static void GetComponentIfNull<T>(this MonoBehaviour a_MonoBehaviour, ref T a_Component) where T : Component
		{
			if (a_Component == null)
			{
				a_Component = a_MonoBehaviour.GetComponent<T>();
			}
		}

		public static void GetComponentInChildrenIfNull<T>(this MonoBehaviour a_MonoBehaviour, ref T a_Component) where T : Component
		{
			GetComponentInChildrenIfNullRecursive<T>(a_MonoBehaviour.transform, ref a_Component);
		}

		public static void GetComponentInParentIfNull<T>(this MonoBehaviour a_MonoBehaviour, ref T a_Component) where T : Component
		{
			if (a_Component == null)
			{
				a_Component = a_MonoBehaviour.GetComponentInParent<T>();
			}
		}

		private static void GetComponentInChildrenIfNullRecursive<T>(Transform a_Parent, ref T a_Component) where T : Component
		{
			if (a_Component == null)
			{
				a_Component = a_Parent.GetComponent<T>();

				if (a_Component == null)
				{
					//Still null, try its children
					foreach (Transform child in a_Parent)
					{
						GetComponentInChildrenIfNullRecursive<T>(child, ref a_Component);
					}
				}
			}
		}

		public static T GetOrAddComponent<T>(this MonoBehaviour a_MonoBehaviour) where T : Component
		{
			T component = a_MonoBehaviour.GetComponent<T>();

			if (component == null)
				component = a_MonoBehaviour.gameObject.AddComponent<T>();

			return component;
		}

		public static void SafeInit<T>(this MonoBehaviour source, ref T component) where T : Component
		{
			if (component == null)
			{
				if (!source.TryGetComponent(out component))
				{
					Debug.LogError("Reference variable not assigned and no component found.");
				}
			}
		}

		public static void RunNextFrame(this MonoBehaviour a_MonoBehaviour, System.Action a_Callback)
		{
			a_MonoBehaviour.StartCoroutine(Coroutine(a_Callback));

			IEnumerator Coroutine(System.Action _Callback)
			{
				yield return null;
				_Callback?.Invoke();
			}
		}

		public static void RunAfterFrames(this MonoBehaviour a_MonoBehaviour, int a_DelayFrames, System.Action a_Callback)
		{
			a_MonoBehaviour.StartCoroutine(Coroutine(a_DelayFrames, a_Callback));

			IEnumerator Coroutine(int _DelayFrames, System.Action _Callback)
			{
				for (int i = 0; i < _DelayFrames; i++)
				{
					yield return null;
				}

				_Callback?.Invoke();
			}
		}

		public static void RunAfterSeconds(this MonoBehaviour a_MonoBehaviour, float a_Seconds, System.Action a_Callback)
		{
			a_MonoBehaviour.StartCoroutine(Coroutine(a_Seconds, a_Callback));

			IEnumerator Coroutine(float _Seconds, System.Action _Callback)
			{
				yield return new WaitForSeconds(_Seconds);
				_Callback?.Invoke();
			}
		}

		public static void RunAfterSecondsRealtime(this MonoBehaviour a_MonoBehaviour, float a_Seconds, System.Action a_Callback)
		{
			a_MonoBehaviour.StartCoroutine(Coroutine(a_Seconds, a_Callback));

			IEnumerator Coroutine(float _Seconds, System.Action _Callback)
			{
				yield return new WaitForSecondsRealtime(_Seconds);
				_Callback?.Invoke();
			}
		}

		public static void RunWhenReady(this MonoBehaviour a_MonoBehaviour, System.Func<bool> a_Predicate, System.Action a_Callback)
		{
			a_MonoBehaviour.StartCoroutine(Coroutine(a_Predicate, a_Callback));

			IEnumerator Coroutine(System.Func<bool> _Predicate, System.Action _Callback)
			{
				yield return new WaitUntil(_Predicate);
				_Callback?.Invoke();
			}
		}
	}
}