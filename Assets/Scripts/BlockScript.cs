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
    [SerializeField]
    private ParticleSystem boxplosion;

    string[] colors = {"#009da7", "#00ff86","#ff6e49", "#de0062", "#ff2420", "#00c89b", "#083a59", "#9f003f", "#cb3522"};

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color boxColor;
        if(gameObject.tag != "inverseBlock"){
            ColorUtility.TryParseHtmlString(colors[UnityEngine.Random.Range(0, 9)], out boxColor);
            spriteRenderer.color = boxColor;
        }
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
            {
                ParticleSystem localBoxPlosion = Instantiate(boxplosion, gameObject.transform.position, Quaternion.identity);
                ParticleSystem.MainModule mp = localBoxPlosion.main;
                mp.startColor = spriteRenderer.color;
                localBoxPlosion.Play();
                Destroy(localBoxPlosion, 0.20f);
                if(gameObject.tag == "inverseBlock"){
                    GameObject.FindWithTag("blockSpawner").GetComponent<BlockSpawner>().shouldGoUp = true;
                    // GameObject.FindWithTag("blockSpawner").GetComponent<BlockSpawner>().goReverseCount = 2;
                }
                Destroy(gameObject);
            }
            UpdateVisualState();
        }
    }

    private void UpdateVisualState()
    {
        text.SetText(hitsRemaining.ToString());
    }

    public void SetHits(int hits)
    {
        hitsRemaining = hits;
        UpdateVisualState();
    }
}
