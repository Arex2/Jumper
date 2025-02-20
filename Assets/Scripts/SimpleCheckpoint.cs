using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCheckpoint : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.SetSpawn(transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
