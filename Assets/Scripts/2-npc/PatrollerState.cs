using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrollerState : State
{
    private ChaserState chaserState;//
    public bool canChaseAfterPlayer;//
    private RotatorState rotatorState;

    [Tooltip("The object that this enemy chases after")]
    [SerializeField] GameObject player;

    [Tooltip("Minimum time to wait at target between running to the next target")]
    [SerializeField] private float minWaitAtTarget = 7f;

    [Tooltip("Maximum time to wait at target between running to the next target")]
    [SerializeField] private float maxWaitAtTarget = 15f;


    [Tooltip("A game object whose children have a Target component. Each child represents a target.")]
    [SerializeField] private Transform targetFolder = null;
    private Target[] allTargets = null;

    [Header("For debugging")]
    [SerializeField] private Target currentTarget = null;
    [SerializeField] private float timeToWaitAtTarget = 0;


    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float rotationSpeed = 5f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        allTargets = targetFolder.GetComponentsInChildren<Target>(false); // false = get components in active children only
        Debug.Log("Found " + allTargets.Length + " active targets.");
        SelectNewTarget();
    }

    private void SelectNewTarget()
    {
        currentTarget = allTargets[Random.Range(0, allTargets.Length - 1)];
        Debug.Log("New target: " + currentTarget.name);
        navMeshAgent.SetDestination(currentTarget.transform.position);
        //if (animator) animator.SetBool("Run", true);
        timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
    }


    private void Update()
    {
        
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, player.transform.position);
        //print("patrol "+distanceToTarget);
        
        if (navMeshAgent.hasPath)
        {
            FaceDestination();
        }
        else 
        {   // we are at the target
            //if (animator) animator.SetBool("Run", false);
            timeToWaitAtTarget -= Time.deltaTime;
            if (timeToWaitAtTarget <= 0)
                SelectNewTarget();
        }

        if (distanceToTarget <= 5f)//
        {
            //Debug.Log("ChaserState");
            canChaseAfterPlayer = true;
        }

    }

    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        //transform.rotation = lookRotation; // Immediate rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // Gradual rotation
    }

    internal Vector3 TargetObjectPosition()
    {
        return player.transform.position;
    }

    public override State RunCurrentState()
    {
        if (canChaseAfterPlayer)
        {
            /*this.enabled = rotatorState.enabled = false;
            chaserState.enabled = true;*/
            return chaserState;
        }
        else
        {
            Debug.Log("PatrollerState");
            return this;
        }
    }
}
