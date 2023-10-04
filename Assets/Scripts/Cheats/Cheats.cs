using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    public bool cheatsEnabled = false;
    public Button[] buttons;
    public TextMeshProUGUI text;

    void Awake()
    {
        if(PlayerPrefs.GetInt("Cheats") == 1)
        {
            cheatsEnabled = true;
            text.text = "Disable Cheats";
            UnlockAllLvls();
        } else 
        {
            cheatsEnabled = false;
            text.text = "Enable Cheats";
            RegularLvlUnlocks();
        }
    }
    public void ToggleCheats()
    {
        if(cheatsEnabled)
        {
            cheatsEnabled = false;
            text.text = "Enable Cheats";
            PlayerPrefs.SetInt("Cheats", 0);
            PlayerPrefs.Save();
            RegularLvlUnlocks();
        } else 
        {
            cheatsEnabled = true;
            text.text = "Disable Cheats";
            PlayerPrefs.SetInt("Cheats", 1);
            PlayerPrefs.Save();
            UnlockAllLvls();
        }
    }

    private void UnlockAllLvls()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    private void RegularLvlUnlocks()
    {
        int unlockedLvl = PlayerPrefs.GetInt("UnlockedLvl", 1);
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLvl; i++)
        {
            if(i < buttons.Length)
            {
                buttons[i].interactable = true;
            }
        }
    }
}
