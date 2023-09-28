using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterInfo : MonoBehaviour
{
    public OverlayTile standingOnTile;
    public int collectedCoins;
    public TMP_Text scoreText;
    public AudioClip coinPickup;
    public AudioClip boltPickup;
    public AudioClip iceCrackSound;
    private AudioSource currentSoundSource;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private float previousX;
    private float previousY;

    void Start()
    {
        currentSoundSource = GetComponentInChildren<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        // Set initial X and Y positions
        previousX = transform.position.x;
        previousY = transform.position.y;
    }
    
    void Update()
    {
        // NE = x++ & y++
        if(transform.position.x > previousX && transform.position.y > previousY)
        {
            animator.SetInteger("Direction", 1);
            spriteRenderer.flipX = true;
        }
        // NW = x-- & y++
        else if(transform.position.x < previousX && transform.position.y > previousY)
        {
            animator.SetInteger("Direction", 1);
            spriteRenderer.flipX = false;
        }
        // SE = x++ & y--
        else if(transform.position.x > previousX && transform.position.y < previousY)
        {
            animator.SetInteger("Direction", 2);
            spriteRenderer.flipX = false;
        }
        // SW = x-- & y--  
        else if(transform.position.x < previousX && transform.position.y < previousY)
        {
            animator.SetInteger("Direction", 2);
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;

            animator.SetInteger("Direction", 0);
        }

        previousX = transform.position.x;
        previousY = transform.position.y;
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
