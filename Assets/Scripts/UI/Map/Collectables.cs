using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Collectables : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unlockedLvl = PlayerPrefs.GetInt("UnlockedLvl", 1) - 1;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLvl; i++)
        {
            if (i < buttons.Length)
            {
                buttons[i].interactable = true;
            }
        }
    }
   

}

