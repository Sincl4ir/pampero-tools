using UnityEngine;
using System;

namespace Pampero.Tools.Utils
{
    /// <summary>
    /// Extension methods for Transform class.
    /// </summary>
    public static class TransformExtensions
	{
		[Serializable]
		/// <summary>
		/// Represents the directions along the axes.
		/// </summary>
		public enum Axis
		{
			/// <summary>
			/// Upward direction.
			/// </summary>
			Up,

			/// <summary>
			/// Downward direction.
			/// </summary>
			Down,

			/// <summary>
			/// Left direction.
			/// </summary>
			Left,

			/// <summary>
			/// Right direction.
			/// </summary>
			Right,

			/// <summary>
			/// Forward direction.
			/// </summary>
			Forward,

			/// <summary>
			/// Backward direction.
			/// </summary>
			Backward
		}

		public static T GetOrAddComponent<T>(this Transform a_Transform) where T : Component
		{
			T component = a_Transform.GetComponent<T>();

			if (component == null)
				component = a_Transform.gameObject.AddComponent<T>();

			return component;
		}

		public static Transform FindChildRecursivelyByNameProximity(this Transform trans, string name)
		{
			if (trans.gameObject.name.Contains(name))
				return trans;

			for (int i = 0; i < trans.childCount; i++)
			{
				Transform child = trans.GetChild(i);
				Transform t = child.FindChildRecursivelyByNameProximity(name);

				if (t != null)
				{
					return t;
				}
			}

			return null;
		}

		public static Transform FindChildRecursively(this Transform trans, string name)
		{
			if (trans.gameObject.name == name)
				return trans;

			foreach (Transform child in trans)
			{
				Transform t = child.FindChildRecursively(name);
				if (t != null)
					return t;
			}
			return null;
		}

		public static void SetLayerRecursively(this Transform trans, int layer)
		{
			trans.gameObject.layer = layer;
			foreach (Transform child in trans)
			{
				child.SetLayerRecursively(layer);
			}
		}

		public static void SetActiveRecursively(this Transform trans, bool newState)
		{
			trans.gameObject.SetActive(newState);

			int _numChilds = trans.childCount;
			for (int i = 0; i < _numChilds; i++)
			{
				trans.GetChild(i).SetActiveRecursively(newState);
			}
		}

		public static void ResetLocalPositionScaleRotation(this Transform trans)
		{
			trans.localScale = Vector3.one;
			trans.localPosition = Vector3.zero;
			trans.localRotation = Quaternion.identity;
		}

		public static T FindComponentInParents<T>(this Transform transform) where T : Component
		{
			if (transform == null)
			{
				return null;
			}

			T component = transform.GetComponent<T>();
			if (component != null)
			{
				// Component found in the current ancestor
				return component;
			}
			else
			{
				// Recursive call to check parent's ancestor
				return transform.parent.FindComponentInParents<T>();
			}
		}

		/// <summary>
		/// Sets the local axis value of the transform.
		/// </summary>
		/// <param name="trans">The transform to set the axis value for.</param>
		/// <param name="axis">The axis to set.</param>
		/// <param name="value">The value to set for the axis.</param>
		public static void SetAxisValue(this Transform trans, Axis axis, Vector3 value)
		{
			switch (axis)
			{
				case Axis.Up:
					trans.up = value;
					break;
				case Axis.Down:
					trans.up = -value;
					break;
				case Axis.Left:
					trans.right = -value;
					break;
				case Axis.Right:
					trans.right = value;
					break;
				case Axis.Forward:
					trans.forward = value;
					break;
				case Axis.Backward:
					trans.forward = -value;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
			}
		}
	}
}