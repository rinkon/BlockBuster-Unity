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
    public int goReverseCount = 0;

    private void OnEnable() 
    {
        SpawnRow();
        score.SetText("SCORE: " + (rowCount - 1).ToString());
    }

    private void SpawnRow()
    {
        if(shouldGoUp){
            goReverseCount = 2;
            // shouldGoUp = false;
        }
        foreach (var block in blockSpawned)
        {
            if (block != null)
            {
                if(!shouldGoUp)
                    block.transform.position += Vector3.down * width;
                else {
                    block.transform.position += Vector3.up * width;
                }
                if(block.transform.position.y <= minBlockYPosition){
                    minBlockYPosition = block.transform.position.y;
                    if((minBlockYPosition - 0.3f) <= rootBall.transform.position.y){
                        // SceneManager.LoadScene("MainScene");
                        gameEndPopup.SetActive(true);
                        blurImage.SetActive(true);
                        ballLauncher.canPull = false;
                        // StartCoroutine(FadeIn(canvasGroup));
                    }
                }
            }
        }
        foreach (var bonus in bonusSpawned)
        {
            if (bonus != null)
            {
                if(!shouldGoUp)
                    bonus.transform.position += Vector3.down * width;
                else {
                    bonus.transform.position += Vector3.up * width;
                }
                if(bonus.transform.position.y <= minBlockYPosition){
                    minBlockYPosition = bonus.transform.position.y;
                    if((minBlockYPosition - 0.3f) <= rootBall.transform.position.y){
                        // SceneManager.LoadScene("MainScene");
                        gameEndPopup.SetActive(true);
                        blurImage.SetActive(true);
                        ballLauncher.canPull = false;
                        // StartCoroutine(FadeIn(canvasGroup));
                    }
                }
            }
        }

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
                else if(rndm > 30 && rndm < 35 )
                {
                    var bonus = Instantiate(bonusPrefab, GetPosition(i), Quaternion.identity);
                    bonusSpawned.Add(bonus);
                }
                else if(rndm > 40 && rndm < 50 && rowCount > 2)
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
        
    }

    private Vector3 GetPosition(int i)
    {
        return transform.position + Vector3.right * i * width;
    }

    public IEnumerator FadeIn(CanvasGroup canvasGroup){
        float lerptime = 0.5f;
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageCompleted = timeSinceStarted/lerptime;

        while(true){
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageCompleted = timeSinceStarted/lerptime;

            float currentValue = Mathf.Lerp(0, 1, percentageCompleted);

            canvasGroup.alpha = currentValue;

            if (percentageCompleted >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        print("done");
    }
}
