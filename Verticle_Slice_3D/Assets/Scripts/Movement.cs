using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float heading = 0;
    public Transform cam;
    CharacterController character;

    Vector3 camF;
    Vector3 camR;

    Vector2 input;

    Vector3 intent;
    Vector3 velocity;
    Vector3 velocityXZ;

    public float speed = 7;
    public float accel = 15;
    float turnSpeed;
    float turnSpeedLow = 5;
    float turnSpeedHigh = 20;

    public float jumpheight;
    public int maxjumpcount = 3;
    public int jumpcount;
    float inputTimer;

    public float grav = 10f;
    public bool grounded = false;
    public bool isjumping = false;
    private void Start()
    {
        inputTimer = 0;
        character = GetComponent<CharacterController>();
    }
    void Update()
    {
        inputTimer += Time.deltaTime;
        DoInput();
        CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        DoJump();

        character.Move(velocity * Time.deltaTime);

        if (inputTimer >= 2)
        {
            jumpcount = 0;
        }
    }

    void DoInput()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
    }
    void DoMove()
    {
        intent = camF * input.y + camR * input.x;
        float ts = velocity.magnitude / 5;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, ts);
        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);


    }
    void CalculateCamera()
    {
        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }
    void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 1.7f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    void DoGravity()
    {
        if (grounded)
        {
            if (!isjumping)
            {
                velocity.y = -0.5f;
            }
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
        }
        velocity.y = Mathf.Clamp(velocity.y, -10, 20);
    }
    void DoJump()
    {
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                inputTimer = 0;
                jumpcount++;
                StartCoroutine(jumpCooldown());
                Debug.Log(jumpcount);
            }

        }
        if (isjumping)
        {
            if (jumpcount == 1)
            {
                velocity.y = jumpheight;
            }
            else if (jumpcount == 2)
            {
                velocity.y = jumpheight + 2;
            }
            else if (jumpcount == maxjumpcount)
            {
                velocity.y = jumpheight + 5;
                jumpcount = 0;
            }
        }
    }
    IEnumerator jumpCooldown()
    {
        isjumping = true;
        if (jumpcount < maxjumpcount)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else if (jumpcount == maxjumpcount)
        {
            yield return new WaitForSeconds(0.1f);
        }
        isjumping = false;
    }
}