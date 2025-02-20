using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);
    //[SerializeField] private int nextPos;
    //[SerializeField] private bool right;
    //[SerializeField] private bool left;
    [SerializeField] private Directions dir;
    private bool hasBeenTriggered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasBeenTriggered)
        {
            //kör move fast reverse
            hasBeenTriggered = false;
        }

        Move();
    }

    private void MoveAlongX(int posOrNeg)
    {
        cam.transform.position = new Vector3(transform.position.x + (posOrNeg * 14), transform.position.y) + offset;
    }

    private void MoveAlongY(int posOrNeg)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (posOrNeg * 11)) + offset;
    }

    private void Move()
    {
        switch(dir)
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

    private bool CheckWithinCameraView(GameObject obj)
    {


        return false;
    }
}
