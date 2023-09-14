using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gears;
    private int coinsCount;

    void Start()
    {
        
        coinsCount = GameObject.FindGameObjectsWithTag("Gear").Length;

    }

    public void gearsAcheived()
    {
        int coinsLeft = GameObject.FindGameObjectsWithTag("coin").Length;
        int coinsCollected = coinsCount - coinsLeft;

        float percentage = float.Parse(coinsCollected.ToString()) / float.Parse(coinsCount.ToString()) * 100f;

        if (percentage >= 33f && percentage< 66)
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
