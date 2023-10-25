using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{

    public List<WalkPath> possiblePaths = new List<WalkPath>();

    [Space]

    public Transform previousBlock;

    [Space]

    [Header("Booleans")]
    public bool isSlope = false;
    public bool movable = false;
    public bool isButton;
    public bool isGoal;

    [Space]

    [Header("Offsets")]
    public float walkPointOffset = .5f;
    public float slopeOffset = .4f;

    public Vector3 GetWalkPoint()
    {
        float slope = isSlope ? slopeOffset : 0;
        float slopeAdjust = isSlope ? .5f : 0;
        return transform.position + transform.up * walkPointOffset + transform.up * slopeAdjust - transform.up * slope + transform.right * -slopeAdjust + transform.forward * slopeAdjust;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        float stair = isSlope ? .4f : 0;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);

        if (possiblePaths == null)
            return;

        foreach (WalkPath p in possiblePaths)
        {
            if (p.target == null)
                return;
            Gizmos.color = p.active ? Color.red : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.target.GetComponent<Walkable>().GetWalkPoint());
        }
    }
}

[System.Serializable]
public class WalkPath
{
    public Transform target;
    public bool active = true;
}
