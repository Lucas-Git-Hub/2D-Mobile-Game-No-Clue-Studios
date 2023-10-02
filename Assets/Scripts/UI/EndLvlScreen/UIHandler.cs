using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject LevelDialog;
    public TMP_Text LevelStatus;
    public TMP_Text scoreText;
    public GameObject PauseButton;
    private int levelIndex;
    public GameObject cursor;
    public GameObject brokenBlocksCanvas;
    public static UIHandler instance;

    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void UnlockNewLvl()
    {
        if (SceneManager.GetActiveScene().buildIndex - 1 >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("UnlockedLvl", PlayerPrefs.GetInt("UnlockedLvl", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    public void ShowLevelDialog(string status, string scores, int gears)

    {
        cursor.SetActive(false);
        GetComponent<GearsHandler>().gearsAchieved();
        LevelDialog.SetActive(true);
        LevelStatus.text = status;
        scoreText.text = scores;
        PauseButton.SetActive(false);
        if(brokenBlocksCanvas != null)
        {
            brokenBlocksCanvas.SetActive(false);   
        }

        //MARKER PlayerPrefs.GetInt("Lv" + levelIndex) his default value is 0
        if (gears > PlayerPrefs.GetInt("Lvl" + levelIndex))//KEY: Lv1; Value: Stars Number
        {
            PlayerPrefs.SetInt("Lvl" + levelIndex, gears);
            PlayerPrefs.Save();
        }
        
        //If there is atleast 1 gear collected unlock the next lvl
        if(gears >= 1)
        {
            UnlockNewLvl();
        }
    }
}