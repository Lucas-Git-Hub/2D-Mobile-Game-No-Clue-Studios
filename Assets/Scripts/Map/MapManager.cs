using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }
    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;
    public Tile iceTile;
    public Tile packedIceTile;
    public Tile blackIceTile;
    public TileBase waterTile;
    public bool ignoreBottomTiles;
    public Dictionary<Vector2Int, OverlayTile> map;
    public Vector3Int spawnlocationxyz;
    public OverlayTile spawnLocation;
    public Vector3Int bridgeLocation;
    public OverlayTile bridgeTile;
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var tileMap = gameObject.GetComponentInChildren<Tilemap>();
        map = new Dictionary<Vector2Int, OverlayTile>();
        BoundsInt bounds = tileMap.cellBounds;

        //Looping through all our tiles
        for (int z = bounds.max.z; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    if (z == 0 && ignoreBottomTiles)
                    {
                        return;
                    }
                    
                    var tileLocation = new Vector3Int(x, y, z);
                    var tileKey = new Vector2Int(x, y);

                    if (tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey) && tileMap.GetTile(tileLocation) != waterTile)
                    {
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                        
                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                        overlayTile.gridLocation = tileLocation;
                        // Checks if its an ice block that's able to melt
                        if (tileMap.GetTile(tileLocation) == iceTile)
                        {
                            overlayTile.ice = true;
                            overlayTile.hp = 1;
                        } else if (tileMap.GetTile(tileLocation) == packedIceTile)
                        {
                            overlayTile.ice = true;
                            overlayTile.hp = 2;
                        } else if (tileMap.GetTile(tileLocation) == blackIceTile)
                        {
                            overlayTile.ice = true;
                            overlayTile.hp = 3;
                        }

                        if(overlayTile.gridLocation == bridgeLocation && bridgeLocation != null)
                        {
                            overlayTile.isBlocked = true;
                            bridgeTile = overlayTile;
                        }

                        map.Add(tileKey, overlayTile);

                        if(overlayTile.gridLocation == spawnlocationxyz)
                        {
                            spawnLocation = overlayTile;
                        }
                        
                    }
                }
            }
        }
    }

    public List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile, List<OverlayTile> searchableTiles)
    {
        var map = MapManager.Instance.map;

        Dictionary<Vector2Int, OverlayTile> tileSearch = new Dictionary<Vector2Int, OverlayTile>();

        if(searchableTiles.Count > 0)
        {
            foreach(var item in searchableTiles)
            {
                tileSearch.Add(item.grid2DLocation, item);
            }
        } else 
        {
            tileSearch = map;
        }


        List<OverlayTile> neighbours = new List<OverlayTile>();

        // Top Neighbour
        Vector2Int locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y + 1
            );

        if (tileSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - tileSearch[locationToCheck].gridLocation.z) <= 1)
                neighbours.Add(tileSearch[locationToCheck]);
        }

        // Bottom Neighbour
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x,
            currentOverlayTile.gridLocation.y - 1
            );

        if (tileSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - tileSearch[locationToCheck].gridLocation.z) <= 1)
                neighbours.Add(tileSearch[locationToCheck]);
        }

        // Right Neighbour
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x + 1,
            currentOverlayTile.gridLocation.y
            );

        if (tileSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - tileSearch[locationToCheck].gridLocation.z) <= 1)
                neighbours.Add(tileSearch[locationToCheck]);
        }

        //Left Neighbour
        locationToCheck = new Vector2Int(
            currentOverlayTile.gridLocation.x - 1,
            currentOverlayTile.gridLocation.y
            );

        if (tileSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.gridLocation.z - tileSearch[locationToCheck].gridLocation.z) <= 1)
                neighbours.Add(tileSearch[locationToCheck]);
        }

        return neighbours;
    }
}

