using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }
    void FixedUpdate()
    {
        //transform.rotation = Quaternion.Euler(0,0,0);
        transform.Rotate(0f,0f,0.75f);
    }
}
