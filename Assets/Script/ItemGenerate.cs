using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class ItemGenerate : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tileset;
    private Dictionary<TileBase, int> diamondValues; // New dictionary to store diamond values

    void Start()
    {

        tilemap = GetComponent<Tilemap>();
        tileset = new TileBase[3];
        tileset[0] = Resources.Load<TileBase>("Diamond1");
        tileset[1] = Resources.Load<TileBase>("Diamond2");
        tileset[2] = Resources.Load<TileBase>("Diamond3");

        diamondValues = new Dictionary<TileBase, int>(); // Initialize the dictionary
        diamondValues.Add(tileset[0], 10); // Assign values to each diamond type
        diamondValues.Add(tileset[1], 20);
        diamondValues.Add(tileset[2], 30);

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                var randomIndex = Random.Range(0, tileset.Length);
                tilemap.SetTile(pos, tileset[randomIndex]);
            }
        }
    }

    public int CatEatsItem(Vector3 position)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(position);
        var tile = tilemap.GetTile(cellPosition);
        if (tile == null)
        {
            return 0;
        }
        tilemap.SetTile(cellPosition, null);
        tilemap.SetTile(cellPosition + Vector3Int.down, null);
        //Debug.Log("Collision detected");
        int diamondValue = diamondValues[tile];
        return diamondValue;
    }
}

