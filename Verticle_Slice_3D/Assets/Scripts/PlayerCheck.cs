using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField] private ChainChomp Chom;
    [HideInInspector] public Collider Other;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Other = other;
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }
}
