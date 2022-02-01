using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChomp : MonoBehaviour
{
    [Header("Floats (can touch)")]
    [SerializeField] private float Range;
    [SerializeField] private float ReturnRadius;
    [SerializeField] private float speed;
    [SerializeField] private float ReturnSpeed;

    [Header("Gameobjects (maybe also dont touch)")]
    [SerializeField] private GameObject ChompHead;
    [SerializeField] private GameObject ChompHeadMesh;
    private GameObject[] Players;

    [Header("Bools (also dont touch)")]
    [SerializeField] public bool hit;
    private bool Attacking;
    private bool Returning;
    private bool donedamage;
    private bool TimeSet;
    private bool Grabbed;

    [Header("timing (dont adjust here)")]
    public float timing;
    public float TimeTillAttack;
    public float CantAttackTime;

    [Header("vector3 stuff")]
    private Vector3 dir;

    void Start()
    {
    }

    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        if(CantAttackTime > 0)
        {
            CantAttackTime -= Time.deltaTime;
            ChompHeadMesh.transform.LookAt(player);
        }
        else if(CantAttackTime <= 0)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                float dis = Vector3.Distance(transform.position, Players[i].transform.position);

                if (dis <= Range && Players[i].CompareTag("Player") && !Returning)
                {
                    Charge(Players[i]);
                }

            }
        }

        if (Returning)
        {
            ReturnToMiddle();
        }
        if (hit)
        {
            if (!donedamage)
            {
                donedamage = true;
                Debug.Log("once");
                //DO DAMAGE HERE
            }
            ReturnToMiddle();
        }
    }


    public void Charge(GameObject Player)
    {
        if (!TimeSet)
        {
            //ADJUST SOME TIMING HERE
            TimeTillAttack = 2f;
            timing = 0.5f;
            TimeSet = true;
        }

        if(TimeTillAttack > 0)
        {
            dir = Player.transform.position - ChompHead.transform.position;
            dir = dir.normalized;
            dir = dir * Range;
            dir = dir + new Vector3(0, 0, 0);

            ChompHeadMesh.transform.LookAt(dir);

            Vector3 moveto = Vector3.MoveTowards(ChompHead.transform.position, -dir, 2 * Time.deltaTime);
            ChompHead.transform.position = moveto;

            TimeTillAttack -= Time.deltaTime;
        }
        else if(TimeTillAttack <= 0)
        {

            if (!Grabbed)
            {
                dir = Player.transform.position - ChompHead.transform.position;
                dir = dir.normalized;
                dir = dir * Range;
                dir = dir + new Vector3(0, 3, 0);

                Grabbed = true;
            }

            Debug.DrawRay(ChompHead.transform.position, dir);
            attack();
        }
    }
    public void attack()
    {
        Vector3 moveto = Vector3.MoveTowards(ChompHead.transform.position, dir, speed * Time.deltaTime);
        ChompHead.transform.position = moveto;

        float distanceToChain = Vector3.Distance(ChompHead.transform.position, transform.position);

        if(distanceToChain >= Range - .2f)
        {
            Returning = true;
        }


    }
    public void ReturnToMiddle()
    {
        //return to mid
        if(timing > 0)
        {
            timing -= Time.deltaTime;
        }
        else if(timing <= 0)
        {
            Vector3 lerp = Vector3.Lerp(ChompHead.transform.position, transform.position, ReturnSpeed * Time.deltaTime);
            ChompHead.transform.position = lerp;

            float distanceToCenter = Vector3.Distance(ChompHead.transform.position, transform.position);

            if(distanceToCenter <= ReturnRadius)
            {
                //RESETING EVERYTING HERE
                Returning = false;
                TimeSet = false;
                Grabbed = false;
                hit = false;
                donedamage = false;
                //ADJUST CANT ATTACK TIME HERE
                CantAttackTime = 2f;
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ReturnRadius);
    }
}
