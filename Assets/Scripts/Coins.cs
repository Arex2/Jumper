using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private float moveSpeed = 0.5f;
    private float speed;
    private float distance = 0.25f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        speed = moveSpeed;
    }

    void FixedUpdate()
    {
        if (transform.position.y <= startPos.y - distance)//if coin is below min dist
        {
            speed = moveSpeed;
        }
        else if (transform.position.y >= startPos.y + distance) //if coin is  above max dist
        {
            speed = -moveSpeed;
        }
        Move(speed);
    }

    private void Move(float speed)
    {
       transform.Translate(new Vector2(0, speed) * Time.deltaTime); //move
    }
}
