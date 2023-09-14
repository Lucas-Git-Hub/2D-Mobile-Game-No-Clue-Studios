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

    public static UIHandler instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowLevelDialog(string status, string scores)

    {
        GetComponent<GearsHandler>().gearsAchieved();
        LevelDialog.SetActive(true);
        LevelStatus.text = status;
        scoreText.text = scores;
        PauseButton.SetActive(false);
    }
}
