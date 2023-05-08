using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class ItemGenerate : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tileset;
    void Start()
    {

        tilemap = GetComponent<Tilemap>();
        tileset = new TileBase[3];
        tileset[0] = Resources.Load<TileBase>("Diamond1");
        tileset[1] = Resources.Load<TileBase>("Diamond2");
        tileset[2] = Resources.Load<TileBase>("Diamond3");

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
        tilemap.SetTile(cellPosition, null);
        tilemap.SetTile(cellPosition + Vector3Int.down, null);
        Debug.Log("Collision detected");
        return 0;
    }
}

