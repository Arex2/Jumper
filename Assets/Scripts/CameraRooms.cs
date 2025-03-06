using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Directions
{
    Left,
    Right,
    Up,
    Down,
}

public class CameraRooms : MonoBehaviour  //OBS BORDE ÄNDRA, LÄGGA DET HÄR PÅ KAMERAN, och HA ON TRIGGER MED TAG - "RoomSwitcher" för att truigga MOVE
{
    FloorSwitchingBehaviour m_FloorSwitching;

    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);
    //[SerializeField] private int nextPos;
    //[SerializeField] private bool right;
    //[SerializeField] private bool left;
    [SerializeField] private Directions dir;
    private bool hasBeenTriggered;

    private void Start()
    {
        m_FloorSwitching = GameObject.Find("Ground").GetComponent<FloorSwitchingBehaviour>();
    }


    private Vector3 enterPos;
    private Vector3 exitPos;
    private bool IsInNewRoom() //compares positions
    {
        float difference = 0;
        //Debug.Log("enter: " + enterPos + " exit: " + exitPos + " difference X: " + (enterPos.x - exitPos.x));    
        //compare horizontal
        if(dir == Directions.Left || dir == Directions.Right)
        {
            difference = enterPos.x - exitPos.x;
        }
        //compare vertical
        if(dir == Directions.Up || dir == Directions.Down)
        {
            difference = enterPos.y - exitPos.y;
        }
        difference = Mathf.Abs(difference);
        if (difference > 0.25)
            return true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        enterPos = collision.transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            exitPos = collision.transform.position;

            /*
    //only trigger if player is alive
    if (collision.GetComponent<PlayerMovement>().CurrentHealth > 0)
    {

    }
    */

            //reset blocks
            m_FloorSwitching.ResetFloor();
            if (IsInNewRoom())
            {
                if (hasBeenTriggered)
                {
                    //kör move fast reverse
                    Move(false);
                    hasBeenTriggered = false;
                }
                else
                {
                    Move(true);
                    hasBeenTriggered = true;
                }
            }
        }




    }

    private void MoveAlongX(int posOrNeg)
    {
        cam.transform.position = new Vector3(transform.position.x + (posOrNeg * 14), transform.position.y) + offset;
    }

    private void MoveAlongY(int posOrNeg)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (posOrNeg * 11)) + offset;
    }

    private void Move(bool enter)
    {
        if (enter) //NORMAL WAY
        {
            switch (dir)
            {
                case Directions.Left:
                    cam.transform.position = new Vector3(cam.transform.position.x - 14, cam.transform.position.y) + offset;
                    return;
                case Directions.Right:
                    cam.transform.position = new Vector3(cam.transform.position.x + 14, cam.transform.position.y) + offset;
                    return;
                case Directions.Up:
                    cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 11) + offset;
                    return;
                case Directions.Down:
                    cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 11) + offset;
                    return;
            }
        }
        else //REVERSED
        {
            switch (dir)
            {
                case Directions.Left:
                    cam.transform.position = new Vector3(cam.transform.position.x + 14, cam.transform.position.y) + offset;
                    return;
                case Directions.Right:
                    cam.transform.position = new Vector3(cam.transform.position.x - 14, cam.transform.position.y) + offset;
                    return;
                case Directions.Up:
                    cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 11) + offset;
                    return;
                case Directions.Down:
                    cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 11) + offset;
                    return;
            }
        }


    }

    private bool CheckWithinCameraView(GameObject obj)
    {


        return false;
    }
}
