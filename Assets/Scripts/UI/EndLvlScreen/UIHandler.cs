using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public GameObject LevelDialog;
    public TMP_Text LevelStatus;
    public TMP_Text scoreText;
    public GameObject PauseButton;
    public int levelIndex;
    public GameObject cursor;

    public static UIHandler instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowLevelDialog(string status, string scores, int gears)

    {
        cursor.SetActive(false);
        GetComponent<GearsHandler>().gearsAchieved();
        LevelDialog.SetActive(true);
        LevelStatus.text = status;
        scoreText.text = scores;
        PauseButton.SetActive(false);

        //MARKER PlayerPrefs.GetInt("Lv" + levelIndex) his default value is 0
        if (gears > PlayerPrefs.GetInt("Lvl" + levelIndex))//KEY: Lv1; Value: Stars Number
        {
            PlayerPrefs.SetInt("Lvl" + levelIndex, gears);
        }

        Debug.Log("Saving Data is " + PlayerPrefs.GetInt("Lvl" + levelIndex));
    }
}
