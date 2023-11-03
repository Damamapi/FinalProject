using UnityEngine;

public class QuadGizmoDrawer : MonoBehaviour
{
    void OnDrawGizmos()
    {
        foreach (Transform quadTransform in transform)
        {
            MeshFilter meshFilter = quadTransform.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                Gizmos.color = Color.gray; // Change the color to your preference
                Gizmos.matrix = quadTransform.localToWorldMatrix;
                Gizmos.DrawWireMesh(meshFilter.sharedMesh);
            }
        }
    }
}
