using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField]
    Image img;

    Color color;
    float speed = 5f;

    bool picked;

    FloorSwitchingBehaviour floorScript;

    // Start is called before the first frame update
    void Start()
    {
        floorScript = GameObject.Find("Ground").GetComponent<FloorSwitchingBehaviour>();
    }

    /*
    void FixedUpdate()
    {
        img.color = color;
    }
    */


    // kod från: https://discussions.unity.com/t/making-a-color-wheel-color-picker/400146/29

    /*  
	/!\ Don't forget to make the texture readable 
	(Select your texture : in Inspector 
	[Texture Import Setting] > Texture Type > Advanced > Read/Write enabled > True  then Apply).
	*/
    public Texture2D colorPicker;

    public Rect colorPanelRect = new Rect(0, 0, 200, 200);

    void OnGUI()
    {
        GUI.DrawTexture(colorPanelRect, colorPicker);
        
        if (GUI.RepeatButton(colorPanelRect, ""))
        {
            Vector2 pickpos = Event.current.mousePosition;
            float aaa = pickpos.x - colorPanelRect.x;

            float bbb = pickpos.y - colorPanelRect.y;

            int aaa2 = (int)(aaa * (colorPicker.width / (colorPanelRect.width + 0.0f)));

            int bbb2 = (int)((colorPanelRect.height - bbb) * (colorPicker.height / (colorPanelRect.height + 0.0f)));

            Color col = colorPicker.GetPixel(aaa2, bbb2);

            //Debug.Log(aaa2 + "||||||" + bbb2);
            //target.renderer.material.color = col;
            color = col;
            img.color = color;// Color.LerpUnclamped(img.color, color, speed * Time.deltaTime);
            picked = true;
        }
    }


    public void PickColorA() 
    { 
        if(picked)
        floorScript.ColA = color;
    }

    public void PickColorB() {
        if (picked)
        floorScript.ColB = color; }

    public void PickColorC() {
        if (picked)
        floorScript.ColC = color; }

    public void Default()
    {
        floorScript.DefaultColor();
    }
}
