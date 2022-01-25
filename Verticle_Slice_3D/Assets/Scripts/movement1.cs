using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement1 : MonoBehaviour
{
    public CharacterController cc;
    private float speed;
    public float walkingspeed = 15;
    public float sprintspeed = 20;
    public float grav = -9.81f;
    public float jumpheight = 2f;
    public KeyCode jump;


    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Vector3 velocity;
    bool grounded;

    private void Start()
    {
        speed = walkingspeed;
    }
    void Update()
    {
        grounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x * Time.deltaTime + transform.forward * y * Time.deltaTime;

        cc.Move(move * speed);

        if (Input.GetKeyDown(jump) && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2 * grav);
        }

        velocity.y += grav * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintspeed;
        }
        else
        {
            speed = walkingspeed;
        }

    }
}
