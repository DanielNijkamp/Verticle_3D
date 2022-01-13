using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Sprite[] text_sprites;
    public Sprite[] health_sprites;
    public Sprite transparent;
    public int lives;
    public int coins;
    public int stars;
    public int health;

    public GameObject lives_1;
    public GameObject lives_2;

    public GameObject coins_1;
    public GameObject coins_2;

    public GameObject stars_1;
    public GameObject stars_2;

    public GameObject health_wheel;

    private void Start()
    {
        health = 8;
    }
    void Update()
    {
        updateUI();
    }
    void updateUI()
    {
        if (lives <= 9)
        {
            lives_1.GetComponent<Image>().sprite = text_sprites[lives];
            lives_2.GetComponent<Image>().sprite = transparent;
        }
        else
        {

            lives_1.GetComponent<Image>().sprite = text_sprites[int.Parse(lives.ToString().Substring(0, 1))];
            lives_2.GetComponent<Image>().sprite = text_sprites[int.Parse(lives.ToString().Substring(1, 1))];

        }

        if (coins <= 9)
        {
            coins_1.GetComponent<Image>().sprite = text_sprites[coins];
            coins_2.GetComponent<Image>().sprite = transparent;
        }
        else
        {

            coins_1.GetComponent<Image>().sprite = text_sprites[int.Parse(coins.ToString().Substring(0, 1))];
            coins_2.GetComponent<Image>().sprite = text_sprites[int.Parse(coins.ToString().Substring(1, 1))];

        }

        if (stars <= 9)
        {
            stars_1.GetComponent<Image>().sprite = text_sprites[stars];
            stars_2.GetComponent<Image>().sprite = transparent;
        }
        else
        {

            stars_1.GetComponent<Image>().sprite = text_sprites[int.Parse(stars.ToString().Substring(0, 1))];
            stars_2.GetComponent<Image>().sprite = text_sprites[int.Parse(stars.ToString().Substring(1, 1))];

        }
        health_wheel.GetComponent<Image>().sprite = health_sprites[health];

    }
}
