using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterInfo : MonoBehaviour
{
    public OverlayTile standingOnTile;
    public int collectedCoins;
    public TMP_Text scoreText;
    public AudioClip coinPickup;
    public AudioClip boltPickup;
    public AudioClip iceCrackSound;
    private AudioSource currentSoundSource;

    void Start()
    {
        currentSoundSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayIceCrackingSound()
    {
        if(iceCrackSound != null)
        {
            currentSoundSource.PlayOneShot(iceCrackSound, 1);
        }
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
            if (coinPickup != null)
            {
                currentSoundSource.PlayOneShot(coinPickup, 1);
            }
            collectedCoins++;
        }
    }

    private void CollectBolt(GameObject boltGameObject)
    {
        if (boltGameObject != null)
        {
            Destroy(boltGameObject);
            
            if (boltPickup != null)
            {
                currentSoundSource.PlayOneShot(boltPickup, 1);
            }

            // call endscreen here 
            UIHandler.instance.ShowLevelDialog("LEVEL COMPLETE", collectedCoins.ToString());
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
