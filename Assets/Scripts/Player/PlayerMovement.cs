using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Canvas winScreen;
    private Animator animator;

    public GameObject modelAnimator;
    public Transform parent;
    public Transform currentCube;
    public Transform clickedCube;
    public Transform clickParticleParent;

    private PlayerPathFinder pathFinder;
    private InputHandler inputHandler;

    private void Start()
    {
        RayCastDown();
        animator = modelAnimator.GetComponent<Animator>();
        pathFinder = gameObject.GetComponent<PlayerPathFinder>();
        inputHandler = FindObjectOfType<GameManager>().inputHandler;
    }

    private void Update()
    {
        // Get cube under the player
        RayCastDown();

        CheckCurrentCube();

        if (inputHandler.HasReceivedClickInput())
        {
            Ray ray = inputHandler.GetRay(); RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Walkable>() != null) 
                {
                    clickedCube = hit.transform;
                    PlayParticles(hit.transform.GetComponent<Walkable>().GetWalkPoint());
                    AudioManager.Instance.PlayRandomClick();

                    DOTween.Kill(gameObject.transform);
                    pathFinder.finalPath.Clear();
                    pathFinder.FindPath(currentCube, clickedCube);
                    //Debug.Log($"PlayerMovement FollowPath {pathFinder.finalPath[0]} {pathFinder.finalPath[pathFinder.finalPath.Count]}");
                    FollowPath(pathFinder.finalPath);
                }
            }
            inputHandler.ResetTap();
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
            AudioManager.Instance.StopSteps();
            AudioManager.Instance.PlaySFX("levelComplete");
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

    void FollowPath(List<Transform> finalPath)
    {
        Sequence s = DOTween.Sequence();

        AudioManager.Instance.PlaySteps();
        animator.SetBool("Walking",true);
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
        foreach (Transform t in pathFinder.finalPath)
        {
            t.GetComponent<Walkable>().previousBlock = null;
        }
        pathFinder.finalPath.Clear();
        AudioManager.Instance.StopSteps();
        animator.SetBool("Walking", false);
        InputControl.EnableInput();
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
