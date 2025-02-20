using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Killzone : MonoBehaviour
{
    //[SerializeField] private Transform spawnPos;

    private void Start()
    {
        this.GetComponent<TilemapRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        if(collisionObj.CompareTag("Player"))
        {
            PlayerMovement playerScript = collisionObj.GetComponent<PlayerMovement>();
            //collisionObj.transform.position = spawnPos.position;
            //collisionObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if(playerScript.CurrentHealth > 0)
            {
                playerScript.TakeDamage(3);
                //playerScript.Invoke("Respawn", 0.5f);
            }
        }
    }
}
