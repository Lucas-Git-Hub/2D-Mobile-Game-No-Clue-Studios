using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateBlocksBroken : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBlocksBrokenText(int brokenIceBlocks, int totalBlocksNeeded)
    {
        text.text = "Broken Ice Blocks " + brokenIceBlocks + "/" + totalBlocksNeeded;
    }
}
