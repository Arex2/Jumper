using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.WSA;

public class TileCountdown : MonoBehaviour
{
    //använde mig av den här tutorialen när jag skapade detta https://www.youtube.com/watch?v=M5b_OJPY8P0

    [SerializeField]
    Tile[] tiles; //alla nummer som behövs

    [SerializeField]
    Tile hiddenTile; //när tiles är hidden

    [SerializeField]
    Tilemap tilemap;

    private void Start()
    {
        
    }
    public void HideTile(Tilemap tilemap)
    {
        foreach (Tile t in tilemap.GetTilesBlock(tilemap.cellBounds))
        {
            if (t == null) continue;
            if (t == hiddenTile) continue;
            tilemap.SwapTile(t, hiddenTile);
        }
    }

    public void ShowTile(Tilemap tilemap)
    {
        foreach (Tile t in tilemap.GetTilesBlock(tilemap.cellBounds))
        {
            if (t == null) continue;
            if (t == tiles[1]) continue;
            tilemap.SwapTile(t, tiles[1]);
        }
    }

    public void ChangeTile(int i)
    {
        //Debug.Log(" i är: " + i);
        foreach (Tile t in tilemap.GetTilesBlock(tilemap.cellBounds))
        {
            if (t == null) continue;
            if(t == tiles[i]) continue;
            tilemap.SwapTile(t, tiles[i]);
            //Debug.Log("Tile for c " + tiles[i].name + " i är: " + i);
        }



        /*
        foreach (Vector3Int pos in )

        foreach(UnityEngine.Tilemaps.Tile t in tilemap.GetTilesBlock(tilemap.cellBounds))
        {
            if(t == null) continue;

            t.Equals(tiles[i]);
            //t.sprite = sprites[i];
            //tilemap.SetTile(tiles[i]):
            tilemap.SetTile(new Vector3Int(t.transform.GetPosition(), tiles[i])); //tileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), tile);
        }
        */
    }
}
