using UnityEngine;
using System.Collections;

namespace Pampero.Tools.Utils
{
    public static class MonobehaviourExtentions
	{
		public static void GetComponentIfNull<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
		{
			if (component == null)
			{
				monoBehaviour.TryGetComponent<T>(out component);
			}
		}

		public static void GetComponentInChildrenIfNull<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
		{
			GetComponentInChildrenIfNullRecursive<T>(monoBehaviour.transform, ref component);
		}

		public static void GetComponentInParentIfNull<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
		{
			if (component == null)
			{
				component = monoBehaviour.GetComponentInParent<T>();
			}
		}

		private static void GetComponentInChildrenIfNullRecursive<T>(Transform parent, ref T component) where T : Component
		{
			if (component != null) { return; } // Exit early if the component is already found.

			if (parent.TryGetComponent(out T foundComponent))
			{
				component = foundComponent;
				return; // Exit early if the component is found on the current transform.
			}

			foreach (Transform child in parent)
			{
				GetComponentInChildrenIfNullRecursive(child, ref component);
				if (component != null) { return; }
			}
		}

		public static T GetOrAddComponent<T>(this MonoBehaviour monoBehaviour) where T : Component
		{
			if (!monoBehaviour.TryGetComponent<T>(out T component))
			{
				component = monoBehaviour.gameObject.AddComponent<T>();
			}

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

		public static void RunNextFrame(this MonoBehaviour monoBehaviour, System.Action callback)
		{
			monoBehaviour.StartCoroutine(Coroutine(callback));

			IEnumerator Coroutine(System.Action onComplete)
			{
				yield return null;
				onComplete?.Invoke();
			}
		}

		public static void RunAfterFrames(this MonoBehaviour monoBehaviour, int delayFrames, System.Action callback)
		{
			monoBehaviour.StartCoroutine(Coroutine(delayFrames, callback));

			IEnumerator Coroutine(int delay, System.Action onComplete)
			{
				for (int i = 0; i < delay; i++)
				{
					yield return null;
				}

				onComplete?.Invoke();
			}
		}

		public static void RunAfterSeconds(this MonoBehaviour monoBehaviour, float seconds, System.Action callback)
		{
			monoBehaviour.StartCoroutine(Coroutine(seconds, callback));

			IEnumerator Coroutine(float delay, System.Action onComplete)
			{
				yield return new WaitForSeconds(delay);
				onComplete?.Invoke();
			}
		}

		public static void RunAfterSecondsRealtime(this MonoBehaviour monoBehaviour, float seconds, System.Action callback)
		{
			monoBehaviour.StartCoroutine(Coroutine(seconds, callback));

			IEnumerator Coroutine(float delay, System.Action onComplete)
			{
				yield return new WaitForSecondsRealtime(delay);
				onComplete?.Invoke();
			}
		}

		public static void RunWhenReady(this MonoBehaviour monoBehaviour, System.Func<bool> predicate, System.Action callback)
		{
			monoBehaviour.StartCoroutine(Coroutine(predicate, callback));

			IEnumerator Coroutine(System.Func<bool> condition, System.Action onComplete)
			{
				yield return new WaitUntil(condition);
				onComplete?.Invoke();
			}
		}
	}
}
//EOF.