using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private PlayerTest PT;
    private Rigidbody RB;

    public bool life, star, coin, isGravity;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        PT = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTest>();
        if (isGravity)
        {
            RB.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (coin)
            {
                PT.coins += 1;
            }
            if (star)
            {
                PT.star += 1;
            }
            if (life)
            {
                PT.life += 1;
            }
            Destroy(gameObject);
        }
    }

}
