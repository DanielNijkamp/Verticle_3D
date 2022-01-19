using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float life, playerHP, star, coins;
    private int player = 6;
    private int lifes = 5;

    public void Start()
    {
        playerHP = player;
        life = lifes;
    }
}
