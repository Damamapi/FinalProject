using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Walkable))]
public class MovableCube : MonoBehaviour
{
    public LayerMask walkableLayer;
    public bool[] raycastDirections = { true, true, true, true };

    private Walkable walkable;
    private List<Walkable> previousAdjacentWalkables = new List<Walkable>();

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
        List<Walkable> currentAdjacentWalkables = new List<Walkable>();

        for (int i = 0; i < directions.Length; i++)
        {
            if (raycastDirections[i])
            {
                Ray ray = new Ray(transform.position, directions[i]);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1.1f, walkableLayer))
                {
                    Walkable adjacentWalkable = hit.collider.GetComponent<Walkable>();
                    if (adjacentWalkable != null)
                    {
                        currentAdjacentWalkables.Add(adjacentWalkable);
                        if (!previousAdjacentWalkables.Contains(adjacentWalkable))
                        {
                            UpdatePath(adjacentWalkable, true);
                        }
                    }
                }
            }
        }

        foreach (Walkable previousAdjacent in previousAdjacentWalkables)
        {
            if (!currentAdjacentWalkables.Contains(previousAdjacent))
            {
                UpdatePath(previousAdjacent, false);
            }
        }

        previousAdjacentWalkables = new List<Walkable>(currentAdjacentWalkables);
    }

    void UpdatePath(Walkable targetWalkable, bool isActive)
    {
        UpdatePathForWalkable(walkable, targetWalkable, isActive);
        UpdatePathForWalkable(targetWalkable, walkable, isActive);
    }

    void UpdatePathForWalkable(Walkable sourceWalkable, Walkable targetWalkable, bool isActive)
    {
        WalkPath path = sourceWalkable.possiblePaths.Find(p => p.target == targetWalkable.transform);
        if (path == null && isActive)
        {
            path = new WalkPath { target = targetWalkable.transform, active = true };
            sourceWalkable.possiblePaths.Add(path);
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
