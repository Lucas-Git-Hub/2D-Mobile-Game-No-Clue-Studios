using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callEnableCursor()
    {
        Invoke("enableCursor", 0.1f);
    }

    private void enableCursor()
    {
        cursor.SetActive(true);
    }
}