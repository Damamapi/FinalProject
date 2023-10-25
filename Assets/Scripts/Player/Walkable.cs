using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    [Header("Booleans")]
    public bool isSlope = false;

    [Space]

    [Header("Offsets")]
    public float walkPointOffset = .5f;
    public float slopeOffset = .45f;

    public Vector3 GetWalkPoint()
    {
        float slope = isSlope ? slopeOffset : 0;
        float slopeAdjust = isSlope ? .5f : 0;
        return transform.position + transform.up * walkPointOffset + transform.up * slopeAdjust - transform.up * slope + transform.right * -slopeAdjust + transform.forward * slopeAdjust;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        float slope = isSlope ? slopeOffset : 0;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);
    }
}

public class GamePath
{
    public Transform target;
    public bool active = true;
}
