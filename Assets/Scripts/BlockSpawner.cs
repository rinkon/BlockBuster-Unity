using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private BlockScript blockPrefab, inverseBlock;
    [SerializeField]
    private GameObject bonusPrefab, rootBall, gameEndPopup, blurImage;
    [SerializeField]
    private BallLauncher ballLauncher;
    private int rowCount = 0;

    private List<BlockScript> blockSpawned = new List<BlockScript>();
    private List<GameObject> bonusSpawned = new List<GameObject>();

    public float width;

    private float minBlockYPosition = 100.0f;
    [SerializeField]

    private TextMeshPro score;
    
    public bool shouldGoUp = false;
    public bool shouldGoUpTwoRows = false;
    public int goReverseCount = 0;
    [SerializeField]
    private GameObject loader;

    private void OnEnable() 
    {
        SpawnRow();
        score.SetText((rowCount - 1).ToString());
        print("Spawned blocks: " + blockSpawned.Count);
    }

    private void SpawnRow()
    {
        if(shouldGoUp){
            goReverseCount = 2;
        }
        if(shouldGoUpTwoRows){
            goReverseCount = 3;
        }
        // move blocks down or up
        foreach (var block in blockSpawned)
        {
            if (block != null)
            {
                if(shouldGoUp)
                    block.transform.position += Vector3.up * width;
                else if(shouldGoUpTwoRows)
                    block.transform.position += Vector3.up * width * 2;
                else 
                    block.transform.position += Vector3.down * width;
                
                if(block.transform.position.y <= minBlockYPosition){
                    minBlockYPosition = block.transform.position.y;
                    if((minBlockYPosition - 0.3f) <= rootBall.transform.position.y){
                        gameEndPopup.SetActive(true);
                        blurImage.SetActive(true);
                        ballLauncher.canPull = false;
                    }
                }
            }
        }
        // move bonuses down or up
        foreach (var bonus in bonusSpawned)
        {
            if (bonus != null)
            {
                if(shouldGoUp)
                    bonus.transform.position += Vector3.up * width;
                else if(shouldGoUpTwoRows)
                    bonus.transform.position += Vector3.up * width * 2;
                else {
                    bonus.transform.position += Vector3.down * width;
                }
                if(bonus.transform.position.y <= minBlockYPosition){
                    minBlockYPosition = bonus.transform.position.y;
                    if((minBlockYPosition - 0.3f) <= rootBall.transform.position.y){
                        gameEndPopup.SetActive(true);
                        blurImage.SetActive(true);
                        ballLauncher.canPull = false;
                    }
                }
            }
        }

        // Generate rows of blocks and bonus
        if(goReverseCount == 0){
            rowCount++;
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
                else if( rndm > 30 && rndm < 36 )
                {
                    var bonus = Instantiate(bonusPrefab, GetPosition(i), Quaternion.identity);
                    bonusSpawned.Add(bonus);
                }
                else if(rndm > 40 && rndm < 43 && rowCount > 2)
                {
                    var block = Instantiate(inverseBlock, GetPosition(i), Quaternion.identity);
                    int hits = UnityEngine.Random.Range(1, 3) + rowCount;
                    block.SetHits(hits);
                    blockSpawned.Add(block);
                }
            }
        }
        if(goReverseCount > 0){
            goReverseCount--;
        }
        if (shouldGoUp) shouldGoUp = false;
        if (shouldGoUpTwoRows) shouldGoUpTwoRows = false;
        minBlockYPosition = 100.0f;
    }

    private Vector3 GetPosition(int i)
    {
        return transform.position + Vector3.right * i * width;
    }

    public void StartFirstScene(){
        int highScore = PlayerPrefs.GetInt("highScore", 0);

        if ((rowCount - 1) > highScore )
        {
            print("highscore " + highScore.ToString());
            print("rowCount " + rowCount.ToString());
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(0);
    }

    public void InverseBlockPower(){
        shouldGoUp = true;
    }
    public void ReviveAwarded(){
        shouldGoUpTwoRows = true;
        ballLauncher.canPull = true;
    }
}
