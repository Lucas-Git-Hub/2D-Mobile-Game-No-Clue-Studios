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
    public AudioClip iceAlmostBreakSound;
    public AudioClip iceBreakSound;
    private AudioSource currentSoundSource;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private float previousX;
    private float previousY;

    public int brokenIceBlocks;

    void Start()
    {
        brokenIceBlocks = 0;
        currentSoundSource = GetComponentInChildren<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        // Set initial X and Y positions
        previousX = transform.position.x;
        previousY = transform.position.y;
    }
    
    void Update()
    {
        WalkDirection();

        previousX = transform.position.x;
        previousY = transform.position.y;
    }

    private void WalkDirection()
    {
        // NE = x++ & y++
        if(transform.position.x > previousX && transform.position.y > previousY)
        {
            spriteRenderer.flipX = true;
            animator.SetInteger("Direction", 1);
        }
        // NW = x-- & y++
        else if(transform.position.x < previousX && transform.position.y > previousY)
        {
            spriteRenderer.flipX = false;
            animator.SetInteger("Direction", 1);
        }
        // SE = x++ & y--
        else if(transform.position.x > previousX && transform.position.y < previousY)
        {
            spriteRenderer.flipX = false;
            animator.SetInteger("Direction", 2);
        }
        // SW = x-- & y--  
        else if(transform.position.x < previousX && transform.position.y < previousY)
        {
            spriteRenderer.flipX = true;
            animator.SetInteger("Direction", 2);
        }
        else if(transform.position.x == previousX && transform.position.y == previousY)
        {
            spriteRenderer.flipX = false;
            animator.SetInteger("Direction", 0);
        }
    }

    public void PlayIceCrackingSound()
    {
        if(iceCrackSound != null)
        {
            currentSoundSource.PlayOneShot(iceCrackSound, 1);
        }
    }
    public void PlayIceAlmostBreakingSound()
    {
        if(iceAlmostBreakSound != null)
        {
            currentSoundSource.PlayOneShot(iceAlmostBreakSound, 1);
        }
    }
    public void PlayIceBreakingSound()
    {
        if(iceBreakSound != null)
        {
            currentSoundSource.PlayOneShot(iceBreakSound, 1);
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
