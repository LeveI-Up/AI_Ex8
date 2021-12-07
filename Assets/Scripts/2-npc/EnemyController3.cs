
using UnityEngine;

/**
 * This component patrols between given points, chases a given target object when it sees it, and rotates from time to time.
 */
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(HitEngineState))]
public class EnemyController3: MonoBehaviour {
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] float probabilityToRotate = 0.2f;
    [SerializeField] float probabilityToStopRotating = 0.2f;
    [SerializeField] float radiusToDie = 1f;
    [SerializeField] bool playerDied = false;
    [SerializeField] bool braveEnemy = false;


    private Chaser chaser;
    private Patroller patroller;
    private Rotator rotator;
    private HitEngineState hitEngine;


    private void HitEngine()
    {
        hitEngine.enabled = true;
        patroller.enabled = rotator.enabled = chaser.enabled = false;
    }

    private void Chase() {
        chaser.enabled = true;
        patroller.enabled = rotator.enabled = hitEngine.enabled = false;
    }

    private void Patrol() {
        patroller.enabled = true;
        chaser.enabled = rotator.enabled = hitEngine.enabled = false;
    }

    private void Rotate() {
        rotator.enabled = true;
        chaser.enabled = patroller.enabled = hitEngine.enabled = false;
    }

    private void Start() {
        chaser = GetComponent<Chaser>();
        patroller = GetComponent<Patroller>();
        rotator = GetComponent<Rotator>();
        hitEngine = GetComponent<HitEngineState>();
        Patrol();
    }

    private void Update() {
        float distanceToTarget = Vector3.Distance(transform.position, chaser.TargetObjectPosition());
        //Debug.Log("distanceToTarget = " + distanceToTarget);

        if(distanceToTarget < radiusToDie)
        {
            playerDied = true;
            HitEngine();
        }
        else if (distanceToTarget <= radiusToWatch && playerDied == false) {
            Chase();
        }
        else if (rotator.enabled) {
            if (Random.Range(0f,1f) < probabilityToStopRotating*Time.deltaTime) {
                Patrol();
            }
        }
        else if (patroller.enabled) {
            if (Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime) {
                Rotate();
            }
        }
        /*else if (chaser.enabled) {
            if (distanceToTarget > radiusToWatch) {
                Patrol();
            } 
        }*/
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
 