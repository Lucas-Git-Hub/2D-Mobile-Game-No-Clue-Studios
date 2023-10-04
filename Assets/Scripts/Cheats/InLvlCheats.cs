using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLvlCheats : MonoBehaviour
{
    public GameObject openPathButton;
    public GameObject disableIceBreakingButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Cheats") == 1)
        {
            if(openPathButton != null)
            {
                openPathButton?.SetActive(true);
            }
            disableIceBreakingButton?.SetActive(true);
        } else 
        {
            if(openPathButton != null)
            {
                openPathButton?.SetActive(false);
            }
            disableIceBreakingButton?.SetActive(false);
        }
    }
}
