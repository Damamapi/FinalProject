using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public bool walking = false;

    [Space]

    public Transform parent;
    public Transform currentCube;
    public Transform clickedCube;
    public Transform clickParticleParent;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    void Start()
    {
        RayCastDown();
    }

    private void Update()
    {
        // Get cube under the player
        RayCastDown();

        if (currentCube.GetComponent<Walkable>().movable)
        {
            transform.parent = currentCube;
        }
        else
        {
            transform.parent = parent;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if(Physics.Raycast(mouseRay, out mouseHit))
            {
                if(mouseHit.transform.GetComponent<Walkable>() != null) 
                {
                    clickedCube = mouseHit.transform;
                    PlayParticles(mouseHit.transform.GetComponent<Walkable>().GetWalkPoint() + Vector3.up * 10 + Vector3.right * 10 + Vector3.forward * 10);

                    DOTween.Kill(gameObject.transform);
                    finalPath.Clear();
                    FindPath();

                }
            }
        }
    }

    void PlayParticles(Vector3 particlePosition)
    {
        clickParticleParent.position = particlePosition;
        ParticleSystem[] particles = clickParticleParent.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            Debug.Log("Playing particle");
            particle.Play();
        }
    }

    void FindPath()
    {
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in currentCube.GetComponent<Walkable>().possiblePaths) 
        {
            if (path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = currentCube;
            }
        }

        pastCubes.Add(currentCube);

        ExploreCube(nextCubes, pastCubes);
        BuildPath();
    }

    void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == clickedCube) return;
        
        foreach(WalkPath path in current.GetComponent<Walkable>().possiblePaths)
        {
            if (!visitedCubes.Contains(path.target) && path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = current;
            }
        }

        visitedCubes.Add(current);
        
        if (nextCubes.Any())
        {
            ExploreCube(nextCubes, visitedCubes);
        }
    }

    void BuildPath()
    {
        Transform cube = clickedCube;
        while (cube != currentCube)
        {
            finalPath.Add(cube);
            if (cube.GetComponent<Walkable>().previousBlock != null)
                cube = cube.GetComponent<Walkable>().previousBlock;
            else
                return;
        }

        finalPath.Insert(0, clickedCube);

        FollowPath();
    }

    void FollowPath()
    {
        Sequence s = DOTween.Sequence();

        walking = true;
        InputControl.DisableInput();

        for (int i = finalPath.Count - 1; i > 0; i--)
        {
            float time = finalPath[i].GetComponent<Walkable>().isSlope ? 1.5f : 1;

            s.Append(transform.DOMove(finalPath[i].GetComponent<Walkable>().GetWalkPoint(), .3f * time).SetEase(Ease.Linear));
        }

        if (clickedCube.GetComponent<Walkable>().isButton)
        {
            // pending button implementations
        }

        s.AppendCallback(() => Clear());
    }

    void Clear()
    {
        foreach (Transform t in finalPath)
        {
            t.GetComponent<Walkable>().previousBlock = null;
        }
        finalPath.Clear();
        walking = false;
        InputControl.AllowInput();
    }

    public void RayCastDown()
    {

        Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCube = playerHit.transform;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Ray ray = new Ray(transform.GetChild(0).position, -transform.up);
        Gizmos.DrawRay(ray);
    }
}
