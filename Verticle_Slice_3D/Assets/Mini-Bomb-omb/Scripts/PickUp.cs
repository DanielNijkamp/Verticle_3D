using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private PlayerTest PT;

    private void Awake()
    {

        PT = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTest>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PT.coins += 1;

            Destroy(gameObject);
        }
    }


}
