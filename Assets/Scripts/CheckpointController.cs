using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Script ska ligga på player, checkpoint tilemap ska vara osynligt och en trigger och ha taggen "Checkpoint"
public class CheckpointController : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //om collision är checkpoint
        if (collision.CompareTag("Checkpoint"))
        {
            //Debug.Log("Checkpoint reached");
            //dubbel kolla om det finns en tile i cellpositionen playern har:
            //first get the tilemap
            Tilemap tilemap = collision.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                //Debug.Log("tilemap!=null");
                //get possible cell position from player position
                Vector3Int cellPos = tilemap.layoutGrid.WorldToCell(transform.position);
                Vector3Int cellPos2 = new Vector3Int(cellPos.x + 1, cellPos.y, cellPos.z); //dessa två för att den egentligen tar cellen till höger eller vänster om actual cellen som söks ibland
                Vector3Int cellPos3 = new Vector3Int(cellPos.x - 1, cellPos.y, cellPos.z);
                //Debug.Log("cellPos set: " + cellPos);
                //Check if tilemap has a tile in that position
                if (tilemap.HasTile(cellPos))
                {
                    //Debug.Log("HasTile");
                    //gör detta till spawnpositionen!
                    player.SetSpawn(createPosition(cellPos));
                    //Debug.Log("Setting spawn point to " + cellPos);
                }
                else if (tilemap.HasTile(cellPos2))
                {
                    //Debug.Log("HasTile");
                    //gör detta till spawnpositionen!
                    player.SetSpawn(createPosition(cellPos2));
                    //Debug.Log("Setting spawn point to " + cellPos);
                }
                else if (tilemap.HasTile(cellPos3))
                {
                    //Debug.Log("HasTile");
                    //gör detta till spawnpositionen!
                    
                    player.SetSpawn(createPosition(cellPos3));
                    //Debug.Log("Setting spawn point to " + cellPos);
                }
            }
        }
    }

    private Vector3Int createPosition(Vector3Int orgPos)
    {
        //Debug.Log("Checkpoint reached");
        Vector3Int result = new Vector3Int(orgPos.x, orgPos.y + 1, orgPos.z); return result;
    }
}
