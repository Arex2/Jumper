using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontFix : MonoBehaviour 
{
    //OBS this script is not written by me.
    //It is the second fix from this video:
    // https://www.youtube.com/watch?v=ccYJOT7bUUY


    public Font[] fonts;

    void Start()
    {
        for(int i = 0; i < fonts.Length; i++)
        {
            fonts[i].material.mainTexture.filterMode = FilterMode.Point;
        }
    }
}
