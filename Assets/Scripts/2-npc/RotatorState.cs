using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorState : State
{
    private PatrollerState patrollerState;
    public bool canSeeThePlayer;

    private ChaserState chaserState;
    


    [Tooltip("The object that this enemy chases after")]//
    [SerializeField]//
    GameObject player = null;//
    [Header("These fields are for display only")]
    [SerializeField] private Vector3 playerPosition;

    [SerializeField] float minAngle = -90;
    [SerializeField] float maxAngle = 90;
    [SerializeField] float angularSpeed = 30;
    [SerializeField] private int direction = 1;


    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            /*this.enabled = chaserState.enabled = false;
            patrollerState.enabled = true;*/
            return patrollerState;
        }
        else
        {
            Debug.Log("RotatorState");
            return this;
        }
    }


    void Update()
    {
        transform.Rotate(new Vector3(0, direction * angularSpeed * Time.deltaTime, 0));
        float angle = transform.rotation.eulerAngles.y;
        if (angle > 180)
            angle -= 360;
        if (angle <= minAngle)
            direction = 1;
        if (angle >= maxAngle)
            direction = -1;

        playerPosition = player.transform.position;
        float distanceToPlayer = Vector3.Distance(playerPosition, transform.position);


        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToTarget <= 15f && distanceToTarget > 6f)//
        {
            //Debug.Log("PatrollerState");
            canSeeThePlayer = true;
        }
    }

}
