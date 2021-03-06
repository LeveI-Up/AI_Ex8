using System.Collections;
using UnityEngine;


/**
 * This component moves a player controlled with a CharacterController using the keyboard.
 */
[RequireComponent(typeof(CharacterController))]
public class CharacterKeyboardMover: MonoBehaviour {
    [Tooltip("Speed of player keyboard-movement, in meters/second")]
    [SerializeField] float speed = 3.5f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpForce = 5f;
    

    private float direction_y;
    

    private CharacterController cc;
    void Start() {
        cc = GetComponent<CharacterController>();
    }

    Vector3 velocity;

    void Update()  {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        velocity.x = x * speed;
        velocity.z = z * speed;
        if (cc.isGrounded) {
            /*float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            velocity.x = x * speed;
            velocity.z = z * speed;*/
            
            if (Input.GetButtonDown("Jump"))
            {
                direction_y = jumpForce ;
            }
           

        }/* else {
            direction_y -= gravity*Time.deltaTime;
        }*/
        direction_y -= gravity * Time.deltaTime;
        velocity.y = direction_y;

        // Move in the direction you look:
        velocity = transform.TransformDirection(velocity);

        cc.Move(velocity * Time.deltaTime);
    }
}
