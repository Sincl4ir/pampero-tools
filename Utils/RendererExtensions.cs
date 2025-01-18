using UnityEngine;

namespace Pampero.Tools.Utils
{
    public static class RendererExtensions
	{
		/// Tests if renderer is visible to a camera
		public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
			return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
		}
	}
}