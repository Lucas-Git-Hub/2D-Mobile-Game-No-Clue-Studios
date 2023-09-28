using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LevelData : MonoBehaviour
{
    private int lvlData;
    public int level;

    public Image B_Gear;
    public Image S_Gear;
    public Image G_Gear;
    public Sprite bronzeGear;
    public Sprite silverGear;
    public Sprite goldGear;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get level data
        lvlData = PlayerPrefs.GetInt("Lvl" + level);

        //Change Bolt Sprites according to collected points
        if (lvlData == 1)
        {
            B_Gear.sprite = bronzeGear;
        }
        else if (lvlData == 2)
        {
            B_Gear.sprite = bronzeGear;
            S_Gear.sprite = silverGear;
        }
        else if (lvlData == 3)
        {
            B_Gear.sprite = bronzeGear;
            S_Gear.sprite = silverGear;
            G_Gear.sprite = goldGear;
        }
    }
}
