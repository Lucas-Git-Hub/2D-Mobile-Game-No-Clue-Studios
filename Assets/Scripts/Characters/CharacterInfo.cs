using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterInfo : MonoBehaviour
{
    public OverlayTile standingOnTile;
    public int collectedCoins;
    public TMP_Text scoreText;

    void Start()
    {
        
    }

    void update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject); // we know collision.gameObject is a coin because it has the "Coin" tag, so pass it to the CollectCoin function
        }

        if (collision.gameObject.CompareTag("Bolt"))
        {
            CollectBolt(collision.gameObject); // we know collision.gameObject is a coin because it has the "Coin" tag, so pass it to the CollectCoin function
        }
    }

    private void CollectCoin(GameObject coinGameObject)
    {
        if (coinGameObject != null)
        {
            Destroy(coinGameObject);
            collectedCoins++;
        }
    }

    private void CollectBolt(GameObject boltGameObject)
    {
        if (boltGameObject != null)
        {
            Destroy(boltGameObject);

            // call endscreen here 
            
                UIHandler.instance.ShowLevelDialog("LEVEL COMPLETE", collectedCoins.ToString());
            
     


           
        }
    }
}
