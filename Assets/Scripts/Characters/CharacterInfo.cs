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

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject); 
        }

        if (collision.gameObject.CompareTag("Bolt"))
        {
            CollectBolt(collision.gameObject); 
        }

        if(collision.gameObject.CompareTag("Snowpile"))
        {
            ClearSnowPile(collision.gameObject);
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
            UIHandler.instance.ShowLevelDialog("LEVEL COMPLETE", collectedCoins.ToString(), collectedCoins);
        }
    }  


           
    private void ClearSnowPile(GameObject snowPileGameObject)
    {
        if (snowPileGameObject != null)
        {
            Destroy(snowPileGameObject);
        }
    }
}
