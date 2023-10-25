using UnityEngine;

[RequireComponent(typeof(Walkable))]
public class MovableCube : MonoBehaviour
{
    public LayerMask walkableLayer;  
    public bool[] raycastDirections = { true, true, true, true };  // Enables/Disables raycasts in each direction

    private float rayCastLength = .6f;

    private Walkable walkable;

    void Start()
    {
        walkable = GetComponent<Walkable>();
    }

    void Update()
    {
        CheckAdjacency();
    }

    void CheckAdjacency()
    {
        Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        for (int i = 0; i < directions.Length; i++)
        {
            if (raycastDirections[i])
            {
                Ray ray = new Ray(transform.position, directions[i]);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayCastLength, walkableLayer))
                {
                    Walkable adjacentWalkable = hit.collider.GetComponent<Walkable>();
                    if (adjacentWalkable != null)
                    {
                        UpdatePath(adjacentWalkable, true);
                    }
                }
                else
                {
                    // Optional: Remove path if no longer adjacent
                    Ray inverseRay = new Ray(transform.position + directions[i], -directions[i]);
                    if (Physics.Raycast(inverseRay, out hit, rayCastLength + .2f, walkableLayer))
                    {
                        Walkable nonAdjacentWalkable = hit.collider.GetComponent<Walkable>();
                        if (nonAdjacentWalkable != null)
                        {
                            UpdatePath(nonAdjacentWalkable, false);
                        }
                    }
                }
            }
        }
    }

    void UpdatePath(Walkable targetWalkable, bool isActive)
    {
        WalkPath path = walkable.possiblePaths.Find(p => p.target == targetWalkable.transform);
        if (path == null && isActive)
        {
            path = new WalkPath { target = targetWalkable.transform, active = true };
            walkable.possiblePaths.Add(path);
        }
        else if (path != null)
        {
            path.active = isActive;
        }
    }
    void OnDrawGizmos()
    {
        Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        for (int i = 0; i < directions.Length; i++)
        {
            if (raycastDirections[i])
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + directions[i]);
            }
        }
    }
}

