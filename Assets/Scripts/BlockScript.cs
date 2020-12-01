using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TextMeshPro text;
    private int hitsRemaining = 5;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        UpdateVisualState();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "ball")
        {
            if (hitsRemaining > 1) 
                hitsRemaining -= 1;
            else
                Destroy(gameObject);
            UpdateVisualState();   
        }
    }

    private void UpdateVisualState()
    {
        text.SetText(hitsRemaining.ToString());
        spriteRenderer.color = Color.Lerp(Color.white, Color.yellow, hitsRemaining / 10.0f);
    }

    public void SetHits(int hits)
    {
        hitsRemaining = hits;
        UpdateVisualState();
    }
}
