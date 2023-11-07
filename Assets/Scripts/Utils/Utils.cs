using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        if (camera == null)
        {
            Debug.LogError("Camera reference is null.");
            return Vector3.zero;
        }
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}
