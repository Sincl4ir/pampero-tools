using UnityEngine;

namespace Pampero.Tools.Utils
{
    public static class RectTransformExtensions
	{
		public static void ResetLocalPositionScaleRotation(this RectTransform trans)
		{
			if (trans != null)
			{
				trans.localScale = Vector3.one;
				trans.anchoredPosition = Vector2.zero;
				trans.localRotation = Quaternion.identity;
			}
		}

		public static void SetDefaultScale(this RectTransform trans)
		{
			if (trans != null)
			{
				trans.localScale = new Vector3(1, 1, 1);
			}
		}

		public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
		{
			if (trans != null)
			{
				trans.pivot = aVec;
				trans.anchorMin = aVec;
				trans.anchorMax = aVec;
			}
		}

		public static Vector2 GetSize(this RectTransform trans)
		{
			if (trans != null)
			{
				return trans.rect.size;
			}

			return Vector2.zero;
		}

		public static float GetWidth(this RectTransform trans)
		{
			if (trans != null)
			{
				return trans.rect.width;
			}

			return 0f;
		}

		public static float GetHeight(this RectTransform trans)
		{
			if (trans != null)
			{
				return trans.rect.height;
			}

			return 0f;
		}

		public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
		{
			if (trans != null)
			{
				trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
			}
		}

		public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
		{
			if (trans != null)
			{
				trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
			}
		}

		public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
		{
			if (trans != null)
			{
				trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
			}
		}

		public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
		{
			if (trans != null)
			{
				trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
			}
		}

		public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
		{
			if (trans != null)
			{
				trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
			}
		}

		public static void SetSize(this RectTransform trans, Vector2 newSize)
		{
			if (trans != null)
			{
				Vector2 oldSize = trans.rect.size;
				Vector2 deltaSize = newSize - oldSize;
				trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
				trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
			}
		}

		public static void SetWidth(this RectTransform trans, float newSize)
		{
			if (trans != null)
			{
				SetSize(trans, new Vector2(newSize, trans.rect.size.y));
			}
		}

		public static void SetHeight(this RectTransform trans, float newSize)
		{
			if (trans != null)
			{
				SetSize(trans, new Vector2(trans.rect.size.x, newSize));
			}
		}

		public static Vector3 GetWorldCenterPoint(this RectTransform trans)
		{
			if (trans != null)
			{
				Vector3[] _Corners = new Vector3[4];

				trans.GetWorldCorners(_Corners);

				Vector3 _Center = (_Corners[0] + _Corners[1] + _Corners[2] + _Corners[3]) * 0.25f;

				return _Center;
			}

			return Vector3.zero;
		}
	}
}