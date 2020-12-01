using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private BlockScript blockPrefab;
    [SerializeField]
    private GameObject bonusPrefab, rootBall;
    private int rowCount = 0;

    private List<BlockScript> blockSpawned = new List<BlockScript>();
    private List<GameObject> bonusSpawned = new List<GameObject>();

    public float width;

    private float minBlockYPosition = 100.0f;
    [SerializeField]

    private TextMeshPro score;

    private void OnEnable() 
    {
        print("OnEnable width: " + width);   
        SpawnRow();
        score.SetText("SCORE: " + (rowCount - 1).ToString());
    }

    private void SpawnRow()
    {
        foreach (var block in blockSpawned)
        {
            if (block != null)
            {
                block.transform.position += Vector3.down * width;
                if(block.transform.position.y <= minBlockYPosition){
                    minBlockYPosition = block.transform.position.y;
                    if((minBlockYPosition - 0.3f) <= rootBall.transform.position.y){
                        SceneManager.LoadScene("MainScene");
                    }
                }
            }
        }
        foreach (var bonus in bonusSpawned)
        {
            if (bonus != null)
            {
                bonus.transform.position += Vector3.down * width;
                if(bonus.transform.position.y <= minBlockYPosition){
                    minBlockYPosition = bonus.transform.position.y;
                    if((minBlockYPosition - 0.3f) <= rootBall.transform.position.y){
                        SceneManager.LoadScene("MainScene");
                    }
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            int rndm = UnityEngine.Random.Range(0, 100);
            if (rndm <= 30)
            {
                var block = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);   
                int hits = UnityEngine.Random.Range(1, 3) + rowCount;
                block.SetHits(hits);
                blockSpawned.Add(block);
            }
            else if(rndm > 30 && rndm < 35 )
            {
                var bonus = Instantiate(bonusPrefab, GetPosition(i), Quaternion.identity);
                bonusSpawned.Add(bonus);
            }
        }
        rowCount++;
    }

    private Vector3 GetPosition(int i)
    {
        return transform.position + Vector3.right * i * width;
    }
}
