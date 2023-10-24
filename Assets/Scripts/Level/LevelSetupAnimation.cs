using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSetupAnimation : MonoBehaviour
{
    public float duration = 1.5f;

    public Vector3 ranges;
    private Rigidbody[] rigidbodies;

    void Start()
    {
        rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.useGravity = false;
            rb.detectCollisions = false;
        }

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("LevelBlock");
        SetupLevel(cubes);
    }

    void SetupLevel(GameObject[] cubes)
    {
        foreach (GameObject cube in cubes)
        {
            Vector3 randomOffset = RandomOffset();
            float randomDelay = Random.Range(0f, 1f);
            Vector3 startPosition = cube.transform.position + randomOffset;
            cube.transform.position = startPosition;
            cube.transform.DOMove(cube.transform.position - randomOffset, duration)
                .SetEase(Ease.InCubic)
                .SetDelay(randomDelay);
        }

        Invoke("EnableInteractions", 3f);
    }

    void EnableInteractions()
    {
        foreach (var rb in rigidbodies)
        {
            rb.useGravity = true;
            rb.detectCollisions = true;
        }

        InputControl.AllowInput();
    }

    Vector3 RandomOffset()
    {
        // Randomly pick one of the three axes (X, Y, or Z)
        int axis = Random.Range(0, 3);

        float offsetX = 0f, offsetY = 0f, offsetZ = 0f;

        // outsiderangerandom:
        // Random.Range(0, 1) < 0.5f ? Random.Range(-ranges.z - ranges.z / 10, -ranges.z) : Random.Range(ranges.z, ranges.z + ranges.z / 10);

        if (axis == 0)  // X-axis
        {
            offsetX = ranges.x;
        }
        else if (axis == 1)  // Y-axis
        {
            offsetY = ranges.y;
        }
        else  // Z-axis
        {
            offsetZ = ranges.z;
        }

        return new Vector3(offsetX, offsetY, offsetZ);
    }
}
