using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HitEngineState : MonoBehaviour
{
    /*public override State RunCurrentState()
    {
        return this;
    }*/

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [Tooltip("The object that this enemy wants to destroy")]
    [SerializeField] GameObject engine = null;
    private Vector3 enginePos;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceEngine();
        navMeshAgent.SetDestination(enginePos);
    }

    private void FaceEngine()
    {
        enginePos = engine.transform.position;
        Vector3 direction = (enginePos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

        if(enginePos == transform.position)
        {
            print("YOU LOST");
        }
    }


}
