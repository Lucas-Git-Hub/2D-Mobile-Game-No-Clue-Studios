using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    public bool cheatsEnabled = false;
    public LevelMenu levelMenu;
    public Collectables collectables;

    void Start()
    {
        if(PlayerPrefs.GetInt("Cheats") == 1)
        {
            cheatsEnabled = true;
            UnlockAllLvls();
            UnlockAllAchievements();
        } else 
        {
            cheatsEnabled = false;
            levelMenu.UnlockLvls(PlayerPrefs.GetInt("UnlockedLvl", 1));
            collectables.SetAvailableAchievements(PlayerPrefs.GetInt("UnlockedLvl", 1) - 1);
        }
    }
    public void ToggleCheats()
    {
        if(cheatsEnabled)
        {
            cheatsEnabled = false;
            PlayerPrefs.SetInt("Cheats", 0);
            PlayerPrefs.Save();
            levelMenu.UnlockLvls(PlayerPrefs.GetInt("UnlockedLvl", 1));
            collectables.SetAvailableAchievements(PlayerPrefs.GetInt("UnlockedLvl", 1) - 1);
        } else 
        {
            cheatsEnabled = true;
            PlayerPrefs.SetInt("Cheats", 1);
            PlayerPrefs.Save();
            UnlockAllLvls();
            UnlockAllAchievements();
        }
    }

    private void UnlockAllLvls()
    {
        for (int i = 0; i < levelMenu.buttons.Length; i++)
        {
            levelMenu.buttons[i].interactable = true;
        }
    }
    private void UnlockAllAchievements()
    {
        for (int i = 0; i < collectables.buttons.Length; i++)
        {
            collectables.buttons[i].interactable = false;
        }
        for (int i = 0; i < collectables.buttons.Length; i++)
        {
            collectables.buttons[i].interactable = true;
        }
    }
}
