using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCharacterInfo : MonoBehaviour
{
    public OverlayTile standingOnTile;
    public Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void update()
    {

    }
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Coin"))
    //     {
    //         CollectCoin(collision.gameObject); // we know collision.gameObject is a coin because it has the "Coin" tag, so pass it to the CollectCoin function
    //     }
    // }

    // private void CollectCoin(GameObject coinGameObject)
    // {
    //     if (coinGameObject != null)
    //     {
    //         Destroy(coinGameObject);
    //     }
    // }
}
