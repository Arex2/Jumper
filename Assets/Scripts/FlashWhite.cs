using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhite : MonoBehaviour
{

    private Color baseColor;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        baseColor = gameObject.GetComponent<SpriteRenderer>().color;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.color = Color.white;
        Invoke("ResetColor", 0.3f);
    }

    private void ResetColor()
    {
        spriteRenderer.color = baseColor;
    }
}
