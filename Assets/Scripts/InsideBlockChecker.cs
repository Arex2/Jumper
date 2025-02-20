using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InsideBlockChecker : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;
    //kod som jag använde som inspiration: https://stackoverflow.com/a/65126538
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ContactPoint2D c = collision.GetContact(1);
        //Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
        //Vector3Int cellPosition = tilemap.layoutGrid.WorldToCell(c.point);
        //TileBase t = tilemap.GetTile(cellPosition);
        //t.GetTileData(cellPosition,tilemap,);





        //kolla att jämförelseobjektet inte är normal ground
        if (collision.gameObject.name != "Ground")
        {

            //kolla om det finns en tile i cellpositionen playern har:
            //first get the tilemap
            Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                //get possible cell position from player position
                Vector3Int cellPos = tilemap.layoutGrid.WorldToCell(transform.position);

                //Check if tilemap has a tile in that position
                if (tilemap.HasTile(cellPos))
                {
                    //Debug.Log("Player is within a tile.");
                    //should only happen once aka when player health reaches <= 0 and never after health is at or below 0
                    if (player.CurrentHealth > 0)
                    {
                        //Debug.Log("KILL!");
                        //Kill player
                        player.TakeDamage(3);
                    }
                }
            }
        }
    }

    #region TRASH:
    //[SerializeField]
    //LayerMask layerToCheck;
    // Start is called before the first frame update
    /*
    void Start()
    {
        //player = GetComponentInParent<PlayerMovement>();
        //Debug.Log("start at least happens");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("space hapens");
            //Invoke("CheckIfInsideBlock", 0.1f);// CreateSphereCheck();
        }

        //RaycastHit2D hitL = Physics2D.Raycast(this.transform.position, Vector3.right, 1f, layerToCheck);
        //RaycastHit2D hitR = Physics2D.Raycast(this.transform.position, Vector3.left, dist, layerToCheck);
        //Debug.DrawLine(transform.position, hitL.point);
        //Vector3 raycastDir = this.transform.position - Vector3.right;
        //Vector3 raycastDir2 = this.transform.position - Vector3.left;
        //Debug.DrawRay(this.transform.position, Vector3.right/3, Color.red);
        //Debug.DrawRay(this.transform.position, Vector3.left/3, Color.red);
    }

    public void CheckIfInsideBlock()
    {

        var dist = 0.1f;
        RaycastHit2D hitL = Physics2D.Raycast(this.transform.position, Vector3.right, dist, layerToCheck);
        RaycastHit2D hitR = Physics2D.Raycast(this.transform.position, Vector3.left, dist, layerToCheck);
        Debug.DrawLine(transform.position, hitL.point);


        if ((hitL.collider != null && hitL.transform.name != "Ground") || (hitR.collider != null && hitR.transform.name != "Ground")) //var && förut
        {
            //Kill player
            player.TakeDamage(3);
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("happens");
        if (collision.transform.name != "Ground")// && currentHealth > 0)
        {
            Debug.Log("Kill");
            player.TakeDamage(3);

        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("happens");
        if (collision.transform.name != "Ground")// && currentHealth > 0)
        {
            Debug.Log("Kill");
            player.TakeDamage(3);

        }
    }
    

    private void CreateSphereCheck()
    {
        Debug.Log("create sphere");
        Collider2D col = Physics2D.OverlapCircle(transform.position, 0.3f, layerMask);

        if (col != null && col.transform.name != "Ground" )
        {
            Debug.Log("Kill pls");
        }
    }

    private void SendLineCheck()
    {
        Vector3 direction = new Vector3(transform.position - lastPosition);
        Ray ray = new Ray(lastPosition, direction);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, hit, direction.magnitude))
        {
            // Do something if hit
        }

        this.lastPosition = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        //Debug.Log("Gizmos being drawn");
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.27f);
    }
    */
    #endregion
}
