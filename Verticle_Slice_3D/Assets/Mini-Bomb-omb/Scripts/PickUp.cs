using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private PlayerTest PT;

    public bool life, star, coin;

    public float rotateSpeed, despawnTime;

    private void Awake()
    {
        PT = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTest>();
    }

    private void Update()
    {
        if (coin)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            despawnTime -= Time.deltaTime;

            if (despawnTime <= 0)
            {
                Destroy(this.gameObject);
            }
            if (despawnTime <= -1)
            {
                despawnTime = 0;
            }
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
