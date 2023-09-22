using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public GameObject b_Gear;
    private GameObject s_Gear;
    private GameObject g_Gear;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Lvl" + 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
