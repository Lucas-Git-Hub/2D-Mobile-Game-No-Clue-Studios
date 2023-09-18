using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{
    public float speed;
    private Touch touch;
    public GameObject characterPrefab;
    public TileBase WaterTile;
    public TileBase IceCrackAnimation;
    private CharacterInfo character;
    private SideCharacterInfo sideCharacter;
    private Pathfinder pathFinder;
    private RangeFinder rangeFinder;
    private List<OverlayTile> path;
    private List<OverlayTile> inRangeTiles = new List<OverlayTile>();
    public Tilemap tileMap;
    private OverlayTile startingTile;
    private OverlayTile endTile;
    public OverlayTile spawnLocation;
    private AudioSource currentSoundSource;
    public AudioClip backgroundMusic;
    public bool playBackgroundMusic = false;
    public float musicVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new Pathfinder();
        rangeFinder = new RangeFinder();
        path = new List<OverlayTile>();

        //if character isnt spawned in spawn him in
        if (character == null)
        {
            character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
            PositionCharacterOnLine(spawnLocation);
            character.standingOnTile = spawnLocation;
            // GetInRangeTiles();
        }

        currentSoundSource = GetComponentInChildren<AudioSource>();

        if(playBackgroundMusic == true)
        {
            currentSoundSource.clip = backgroundMusic;
            currentSoundSource.volume = musicVolume;
            currentSoundSource.loop = true;
            currentSoundSource.Play();
        }
    }

    void LateUpdate()
    {
        RaycastHit2D? focusedTileHit = GetFocusedOnTile(); 
        
        if (focusedTileHit.HasValue)
        {
            if(focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>())
            {
                OverlayTile overlayTile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
                transform.position = overlayTile.transform.position;
                gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

                overlayTile.ShowTile();

                endTile = overlayTile;
                startingTile = character.standingOnTile;
                path = pathFinder.FindPath(character.standingOnTile, overlayTile);

                overlayTile.HideTile();
            }
        }

        if(path.Count > 0 && endTile != null)
        {   
            MoveAlongPath(startingTile, endTile);
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

    private void MoveAlongPath(OverlayTile startingTile, OverlayTile end)
    {
        var step = speed * Time.deltaTime;
        var previousTile = path[0];
        bool begin = true;

        float zIndex = path[0].transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        // Take z index to make sure character has a "height" in the 2D space
        character.transform.position = new Vector3(character.transform.position.x , character.transform.position.y, zIndex);

        if(Vector2.Distance(character.transform.position, path[0].transform.position) < 0.0001f)
        {   
            PositionCharacterOnLine(path[0]);

            if(begin)
            {   
                if(startingTile.ice == true)
                {   
                    // Change Iceblock and refresh the tilemap
                    startingTile.isBlocked = true;
                    tileMap.SetTile(startingTile.gridLocation, IceCrackAnimation);
                    character.PlayIceCrackingSound();
                    tileMap.RefreshTile(startingTile.gridLocation);
                }
                begin = false;
            }

            if(previousTile.ice == true && path[0] != end)
            {   
                // Change Iceblock and refresh the tilemap
                previousTile.isBlocked = true;
                tileMap.SetTile(previousTile.gridLocation, IceCrackAnimation);
                character.PlayIceCrackingSound();
                tileMap.RefreshTile(previousTile.gridLocation);
            }
            
            // previousTile = path[0];
            path.RemoveAt(0);
        }

        //Show current available path for player character after moving
        if(path.Count == 0)
        {
            endTile = character.standingOnTile;
            // GetInRangeTiles();
        }
    }
    public void RefreshMap()
    {
        if (tileMap != null)
        {
            tileMap.RefreshAllTiles();
        }
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case (UnityEngine.TouchPhase)UnityEngine.InputSystem.TouchPhase.Began:
                    // Record initial touch position.
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 touchPos2d = new Vector2(touchPos.x, touchPos.y);

                    RaycastHit2D[] hits = Physics2D.RaycastAll(touchPos2d, Vector2.zero);

                    if (hits.Length > 0)
                    {
                        return hits.OrderByDescending(i => i.collider.transform.position.z).First();
                    }
                    break;
                case (UnityEngine.TouchPhase)UnityEngine.InputSystem.TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    break;
            }
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
