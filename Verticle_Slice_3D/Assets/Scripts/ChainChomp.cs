using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChomp : MonoBehaviour
{
    [SerializeField] private GameObject ChompHead;
    [SerializeField] private float Cooldown;
    private bool On;
    public bool InSight;
    private Collider Player;


    void Start()
    {
        On = false;
    }

    void Update()
    {
        switch (InSight)
        {
            case (true):
                if (!On)
                {
                    InvokeRepeating("leap", 0f, .5f);
                }
                break;
            case (false):
                Patroll();
                break;
        }
    }

    public void leap()
    {
        On = true;
        Player = ChompHead.GetComponent<PlayerCheck>().Other;
        Vector3 PlayerPOS = Player.transform.position;
        float Startime = Time.time;


        if(Time.time < Startime + Cooldown)
        {
            ChargeLeap();
        }
        if(Time.time > Startime + Cooldown)
        {
            Debug.Log("ATTACK");
            ChompHead.transform.position = Vector3.Lerp(ChompHead.transform.position, PlayerPOS, 5);
        }
    }

    public void ChargeLeap()
    {
        Debug.Log("Charging");

    }

    public void Patroll()
    {
    }
}
