using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{
    public float speed;
    public GameObject characterPrefab;
    private CharacterInfo character;
    private SideCharacterInfo sideCharacter;
    private Pathfinder pathFinder;
    private RangeFinder rangeFinder;
    private List<OverlayTile> path;
    private List<OverlayTile> inRangeTiles = new List<OverlayTile>();

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new Pathfinder();
        rangeFinder = new RangeFinder();
        path = new List<OverlayTile>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusedTileHit = GetFocusedOnTile(); 

        if (focusedTileHit.HasValue)
        {
            if(focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>())
            {
                 OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
                 transform.position = overlayTile.transform.position;
                gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

                if(Input.GetMouseButtonDown(0))
                {
                    overlayTile.ShowTile();

                    //if character isnt spawned in spawn him in on click, else move the character
                    if (character == null)
                    {
                        character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
                        PositionCharacterOnLine(overlayTile);
                        GetInRangeTiles();
                    } else {
                        path = pathFinder.FindPath(character.standingOnTile, overlayTile);

                        overlayTile.HideTile();
                    }
                }
            }
           
            Coin coinTile = focusedTileHit.Value.collider.gameObject.GetComponent<Coin>();
        }

        if(path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    private void GetInRangeTiles()
    {
        foreach(var item in inRangeTiles)
        {
            item.HideTile();
        }
        
        inRangeTiles = rangeFinder.GetTilesInRange(character.standingOnTile, 15);

        foreach(var item in inRangeTiles)
        {
            item.ShowTile();
        }
    }

    private void MoveAlongPath()
    {
        var step = speed * Time.deltaTime;

        float zIndex = path[0].transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        // Take z index to make sure character has a "height" in the 2D space
        character.transform.position = new Vector3(character.transform.position.x , path[0].transform.position.y, zIndex);

        if(Vector2.Distance(character.transform.position, path[0].transform.position) < 0.0001f)
        {
            PositionCharacterOnLine(path[0]);
            path.RemoveAt(0);
        }

        //Show current available path for player character after moving
        if(path.Count == 0)
        {
            GetInRangeTiles();
        }
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    private void PositionCharacterOnLine(OverlayTile tile)
    {
        // Place character on clicked tile
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        character.GetComponentInChildren<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        character.standingOnTile = tile;
    }
}
