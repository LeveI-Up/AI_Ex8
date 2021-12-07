using UnityEngine;
using UnityEngine.AI;



/**
 * This component represents an NPC that patrols randomly between targets.
 * The targets are all the objects with a Target component inside a given folder.
 */
[RequireComponent(typeof(NavMeshAgent))]
public class Patroller: MonoBehaviour {
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

    [SerializeField] private Target braveTarget = null;
    [SerializeField] private Target cowardTarget = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject enemy2 = null;
    private bool isBrave = false;
    private float cowardPos = -1f;
    private float bravePos = 1000f;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        allTargets = targetFolder.GetComponentsInChildren<Target>(false); // false = get components in active children only
        Debug.Log("Found " + allTargets.Length + " active targets.");
        //SelectNewTarget();
        SelectCowardTarget();
    }

    private void SelectNewTarget() {
        currentTarget = allTargets[Random.Range(0, allTargets.Length - 1)];
        Debug.Log("New target: " + currentTarget.name);
        navMeshAgent.SetDestination(currentTarget.transform.position);
        //if (animator) animator.SetBool("Run", true);
        timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
    }

    private void SelectCowardTarget()
    {
        foreach(Target tempTarget in allTargets)
        {
            float tempPos = Vector3.Distance(player.transform.position , tempTarget.transform.position);
            if (tempPos > cowardPos)
            {
                cowardPos = tempPos;
                cowardTarget = tempTarget;
            }
        }
        navMeshAgent.SetDestination(cowardTarget.transform.position);
        //if (animator) animator.SetBool("Run", true);
       // timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
    }
    private void SelectBraveTarget()
    {
        foreach (Target tempTarget in allTargets)
        {
            float tempPos = Vector3.Distance(player.transform.position, tempTarget.transform.position);
            if (tempPos < bravePos)
            {
                bravePos = tempPos;
                braveTarget = tempTarget;
            }
        }
        navMeshAgent.SetDestination(braveTarget.transform.position);
        //if (animator) animator.SetBool("Run", true);
        // timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
    }

    private void Update() {
        if (enemy2 == null)
        {
            SelectBraveTarget();
        }       
        if (navMeshAgent.hasPath) {
            FaceDestination();
        } else {   // we are at the target
            //if (animator) animator.SetBool("Run", false);
            timeToWaitAtTarget -= Time.deltaTime;
            if (timeToWaitAtTarget <= 0 && isBrave==true)
                SelectNewTarget();
        }
    }

    private void FaceDestination() {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        //transform.rotation = lookRotation; // Immediate rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // Gradual rotation
    }


}
