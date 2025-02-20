using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform target1, target2;
    [SerializeField] private float moveSpeed = 2f;

    private Transform currentTarget;
    [SerializeField]  private Vector2 tar1, tar2;

    void Start()
    {
        currentTarget = target2;
    }

    void FixedUpdate()
    {

        if (transform.position == target1.position)
        {
            currentTarget = target2;
        }
        if (transform.position == target2.position)
        {
            currentTarget = target1;
        }
        //Debug.Log("Say something im giving up on you");
        //PIVOT POINT IS A PROBLEM
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime); //BUGG DEN FORTSÄTTER INTE RÖRA PÅ SIG
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.transform.position.y > transform.position.y )
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(RemoveParent(collision, 1f)); //vill hålla kvar som child så att den hänger med trots att den hoppar över platformen

            //kan ju lägga till en trigger collider som täcker området över och ändra till on trigger exit för att göra till null parent

            //Invoke("removeParent", 1f); //added so some movement is kept while jumping
            collision.transform.SetParent(null);

        }
    }

    /*
    IEnumerator RemoveParent(Collision2D c, float time)
    {
        yield return new WaitForSeconds(time);
        //IF still standing on 
        c.transform.SetParent(null);
    }
    */



}
