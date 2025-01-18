using UnityEngine;

namespace Pampero.Tools.Utils
{
    public static class LayerMaskExtensions
	{
		public static void Add(this ref LayerMask mask, string layerName)
		{
			mask.Add(LayerMask.NameToLayer(layerName));
		}

		public static void Add(this ref LayerMask mask, int layerId)
		{
			mask |= (1 << layerId);
		}

		// Extension method to remove layers from a mask
		public static void Remove(this ref LayerMask mask, string layerName)
		{
			mask.Remove(LayerMask.NameToLayer(layerName));
		}

		public static void Remove(this ref LayerMask mask, int layerId)
		{
			mask &= ~(1 << layerId);
		}

		// Extension method to check if a mask contains a layer
		public static bool Contains(this LayerMask mask, string layerName)
		{
			return mask.Contains(LayerMask.NameToLayer(layerName));
		}

		public static bool Contains(this LayerMask mask, int layerId)
		{
			return (mask.value & (1 << layerId)) != 0;
		}

		// Extension method to check if a mask contains any layer
		public static bool ContainsAny(this LayerMask mask)
		{
			return mask.value != 0;
		}

		// Extension method to concatenate masks
		public static void Concat(this ref LayerMask mask, LayerMask otherMask)
		{
			mask |= otherMask;
		}
	}
}