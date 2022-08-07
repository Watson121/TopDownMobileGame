using UnityEngine;

namespace Viewport
{

    public class ViewportBoundaries
    {
        public static float frustumHeight;
        public static float frustumWidth;
        public static float cameraPositionZ;
        private static float cameraDistance;

        public static void CalculatingViewportBounds(Transform player)
        {
            cameraDistance = Vector3.Distance(player.position, Camera.main.transform.position);
            frustumHeight = (2.0f * cameraDistance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad)) / 2;
            frustumWidth = (frustumHeight * Camera.main.aspect);
            cameraPositionZ = Camera.main.transform.position.z;
        }
    }

    
}
