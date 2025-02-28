using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    float variabel = 0f;
    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        variabel += Random.Range(0.1f, 1f);
    }



    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        variabel += Time.deltaTime * speed;
        //var step = height * Time.deltaTime; // calculate distance to move
        var sinus = Mathf.Sin(0 + (variabel)); //för sinus våg
        //var distance = Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x); //FÖRUTSÄTTER ATT DEN RÖR sig horizontellt
        //distance = distance / 1; //gör till mellan 0 och 1;
        //sinus = sinus * distance;
        //transform.position();
        //var futureYpos = sinus + transform.position.y;

        transform.position += new Vector3(0, sinus / 10000);

        Debug.Log("Varaibel:  " + variabel + "  sinus: " + sinus);
    }




}
