using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaserState : State
{
    public override State RunCurrentState()
    {

        if (playerDied == true)
        {
            Debug.Log("HitEngineState");
            return this;
        }
        else
        {
            Debug.Log("ChaserState");
            return this;
        }
        
    }

    [Tooltip("The object that this enemy chases after")]
    [SerializeField]
    GameObject player = null;

    [Header("These fields are for display only")]
    [SerializeField] private Vector3 playerPosition;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private HitEngineState hitEngine;
    public bool playerDied;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerPosition = player.transform.position;
        float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceToPlayer <= 5f && distanceToPlayer > 1.5f && playerDied==false)
        {
            FacePlayer();
            navMeshAgent.SetDestination(playerPosition);

            
        }
        if (distanceToPlayer <= 1)
        {
            playerDied = true;
            Vector3 target = new Vector3(-17.48f, 12.2f, 10.57f);
            navMeshAgent.SetDestination(target);
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    internal Vector3 TargetObjectPosition()
    {
        return player.transform.position;
    }

    private void FaceDirection()
    {
        Vector3 direction = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }


}
