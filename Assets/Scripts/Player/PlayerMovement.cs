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
    private Animator animator;
    public GameObject modelAnimator;

    [SerializeField] Canvas winScreen;

    [Space]

    public Transform parent;
    public Transform currentCube;
    public Transform clickedCube;
    public Transform clickParticleParent;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    private void Start()
    {
        RayCastDown();
        animator = modelAnimator.GetComponent<Animator>();
    }

    private void Update()
    {
        // Get cube under the player
        RayCastDown();

        CheckCurrentCube();

        if (Input.GetMouseButtonDown(0) && InputControl.IsInputAllowed)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if(Physics.Raycast(mouseRay, out mouseHit))
            {
                if(mouseHit.transform.GetComponent<Walkable>() != null) 
                {
                    clickedCube = mouseHit.transform;
                    PlayParticles(mouseHit.transform.GetComponent<Walkable>().GetWalkPoint());
                    AudioManager.instance.PlayRandomClick();

                    DOTween.Kill(gameObject.transform);
                    finalPath.Clear();
                    FindPath();
                }
            }
        }
    }

    void CheckCurrentCube()
    {
        Walkable cubeBelow = currentCube.GetComponent<Walkable>();
        // Check for movable
        if (cubeBelow.movable) transform.parent = currentCube;
        else transform.parent = parent;

        // Check for win
        if (InputControl.IsInputAllowed && cubeBelow.isGoal)
        {
            InputControl.DisableInput();
            winScreen.gameObject.SetActive(true);
        }
    }

    void PlayParticles(Vector3 particlePosition)
    {
        clickParticleParent.position = particlePosition;
        ParticleSystem[] particles = clickParticleParent.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
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
        AudioManager.instance.ToggleSteps();
        animator.SetBool("Walking",walking);
        InputControl.DisableInput();

        for (int i = finalPath.Count - 1; i > 0; i--)
        {
            float time = finalPath[i].GetComponent<Walkable>().isSlope ? 1.5f : 1;

            Vector3 targetPosition = finalPath[i].GetComponent<Walkable>().GetWalkPoint();
            Vector3 previousPosition = i + 1 < finalPath.Count ? finalPath[i + 1].GetComponent<Walkable>().GetWalkPoint() : transform.position;
            Vector3 direction = (targetPosition - previousPosition).normalized;

            float targetYRotation = GetTargetYRotation(direction);
            Quaternion targetRotation = Quaternion.Euler(0f, targetYRotation, transform.eulerAngles.z);

            s.Append(transform.DORotateQuaternion(targetRotation, .3f * time).SetEase(Ease.Linear));
            s.Join(transform.DOMove(targetPosition, .3f * time).SetEase(Ease.Linear));
        }

        if (clickedCube.GetComponent<Walkable>().isButton)
        {
            // pending button implementations
        }

        s.AppendCallback(() => Clear());
    }



    float GetTargetYRotation(Vector3 direction)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
        float targetYRotation = Mathf.Round(angle / 90f) * 90f;
        return targetYRotation;
    }


    void Clear()
    {
        foreach (Transform t in finalPath)
        {
            t.GetComponent<Walkable>().previousBlock = null;
        }
        finalPath.Clear();
        walking = false;
        AudioManager.instance.ToggleSteps();
        animator.SetBool("Walking", walking);
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
