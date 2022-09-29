using UnityEngine;

namespace Viewport
{

    public class ViewportBoundaries
    {
        public static float frustumHeight;
        public static float frustumWidth;
        
        private static float cameraDistance;

        public static float cameraPositionX;
        public static float cameraPositionZ;

        public static void CalculatingViewportBounds(Transform player)
        {
            cameraDistance = Vector3.Distance(player.position, Camera.main.transform.position);
            frustumHeight = (2.0f * cameraDistance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad)) / 2;
            frustumWidth = (frustumHeight * Camera.main.aspect);

            cameraPositionX = Camera.main.transform.position.x;
            cameraPositionZ = Camera.main.transform.position.z;
        }
    }

    
}
