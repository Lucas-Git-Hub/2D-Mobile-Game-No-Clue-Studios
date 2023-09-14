using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gears;
    private int coinsCount;

    void Start()
    {
        
        coinsCount = GameObject.FindGameObjectsWithTag("Coin").Length;

    }

    public void gearsAchieved()
    {
        int coinsLeft = GameObject.FindGameObjectsWithTag("Coin").Length;
        int coinsCollected = coinsCount - coinsLeft;

        float percentage = float.Parse(coinsCollected.ToString()) / float.Parse(coinsCount.ToString()) * 100f;

        if (percentage <= 0f && percentage < 33)
        {
            gears[0].SetActive(false);
            gears[1].SetActive(false);
            gears[2].SetActive(false);

        }  else if (percentage >= 33f && percentage< 66)
        {
         gears[0].SetActive(true);
        } else if (percentage >= 66 && percentage < 70)
        {
            gears[0].SetActive(true);
            gears[1].SetActive(true);
        } else
        {
            gears[0].SetActive(true);
            gears[1].SetActive(true);
            gears[2].SetActive(true);
        }
    }
}
