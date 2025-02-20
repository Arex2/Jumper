using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorSwitchingBehaviour : MonoBehaviour
{
    [SerializeField]
    TileCountdown tileCountdown;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip switchSound;

    [SerializeField]
    GameObject floorA;
    [SerializeField]
    GameObject floorB;
    [SerializeField]
    GameObject floorC;

    //Default
    Color colA = Color.magenta;
    Color colB = Color.blue;
    Color colC = Color.green;
    Color[] defaultCol = new Color[3];
    public Color ColA { get { return colA; } set {  colA = value; UpdateColor(floorA, colA); } }
    public Color ColB { get {  return colB; } set { colB = value; UpdateColor(floorB, colB); } }
    public Color ColC { get {  return colC; } set { colC = value; UpdateColor(floorC, colC); } }

    //public Color[] DefaultCol { get {  return defaultCol; }  }

    bool activeA; 
    bool activeC;

    int jumpCounter = 0;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        defaultCol[0] = colA;
        defaultCol[1] = colB;
        defaultCol[2] = colC;
    }

    private void PlaySwitchSound()
    {
        //audioSource.pitch = Random.Range(1f, 1.3f);
        audioSource.PlayOneShot(switchSound, 0.5f);
    }

    public void DefaultColor()
    { 
        ColA = defaultCol[0];
        ColB = defaultCol[1];
        ColC = defaultCol[2];
    }

    //[SerializeField]
    //PlayerMovement player;


    //private float timerValue = 0.5

    // Update is called once per frame
    /*
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) ///och canJump är true
        {
            //byt platform som är active
            SwitchFloor();
        }
    }*/

    /*
    private void Start()
    {
        tileCountdown = GetComponent<TileCountdown>();
    }
    */



    public void ResetFloor()
    {
        jumpCounter = 0;

        //floorA.SetActive(true);
        SetVisible(floorA);
        activeA = true;
        //floorB.SetActive(false);
        SetHidden(floorB);
        //floorC.SetActive(false);
        SetHidden(floorC);
        activeC = false;
    }

    public void SwitchFloor()
    {
        PlaySwitchSound();
        jumpCounter++;
        if(activeA)//floorA.activeSelf)//floorA = active  //OBS KAN INTE ANVÄNDA DENNA CHECK NÄR JAG ALDRIG SETTER DE INACTIVE!!! MÅSTE KOLLA P ÅNÅGOT ANNAT SÄTT!!!
        {
            //Debug.Log("Når inom activeA");
            //floorB.SetActive(true);
            SetVisible(floorB);
            //floorA.SetActive(false);
            SetHidden(floorA);
            activeA = false;
        } 
        else 
        {
            //Debug.Log("Når inom activeB");
            //floorA.SetActive(true);
            SetVisible(floorA);
            //floorB.SetActive(false);
            SetHidden(floorB);
            activeA = true;
        }
        

        if (activeC && jumpCounter != 2) //OBS DETTA MÅSTE VARA INNAN DET UNDER FÖR ANNARS KÖRS DETTA NÄR jumpCounter = 0; körts
        {
            tileCountdown.ChangeTile(jumpCounter);
        }

        if (jumpCounter == 2) //jumpCounter%2 == 0)//
        {
            if (activeC)
            {
                SetHidden(floorC);
                activeC = false;
            }
            else
            {
                SetVisible(floorC);
                tileCountdown.ChangeTile(jumpCounter);
                activeC = true;
            }

            jumpCounter = 0;
        }

    }

    private void SetHidden(GameObject floor) //istället för setActive false
    {
        //byt tile sprites
        tileCountdown.HideTile(floor.GetComponent<Tilemap>());//floorB
        //set collider inactive
        floor.GetComponent<TilemapCollider2D>().enabled = false;

        //GÖR OUTLINE FAINTLY LESS SATURATED
        /*
        if(floor!=floorC)
        {
            Color oldC = floor.GetComponent<Tilemap>().color;
            Color newC = new Color(oldC.r + 0.5f, oldC.g + 0.5f, oldC.b + 0.5f);//gör hidden/outlines marginnaly lighter and less saturated
            floor.GetComponent<Tilemap>().color = newC;
        }
        */
        

    }

    private void SetVisible(GameObject floor)
    {
        if (floor == floorA || floor == floorB)
        {
            tileCountdown.ShowTile(floor.GetComponent<Tilemap>());
        }
        if (floor == floorA)
        {
            UpdateColor(floor, colA);
        }
        if (floor == floorB)
        {
            UpdateColor(floor, colB);
        }
        if(floor == floorC)
        {
            //UpdateColor(floor, colC);
        }

        floor.GetComponent<TilemapCollider2D>().enabled = true;

        //player.CheckIfInsideBlock();
    }

    private void UpdateColor(GameObject floor, Color color)
    {
        Tilemap tilemap = floor.GetComponent<Tilemap>();
        tilemap.color = color;
    }

}
