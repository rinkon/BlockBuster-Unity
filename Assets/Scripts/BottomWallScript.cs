using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomWallScript : MonoBehaviour
{
    private int totalBalls = 6;
    [SerializeField]
    private GameObject blockSpawner;
    private int counter = 0;
    [SerializeField]
    private GameObject ballLauncherGameObject;
    private BallLauncher ballLauncher;

    public int targetBallCount = 0;
    [SerializeField]
    private GameObject downArrow;
    bool goDown;
    public static bool firstShotPulled = false;

    private void Start() {
        ballLauncher = ballLauncherGameObject.GetComponent<BallLauncher>();
    }
    void Update()
    {
        if(!firstShotPulled){
            if(downArrow.GetComponent<SpriteRenderer>().color.a >= 1.0f)
                goDown = true;
            if(downArrow.GetComponent<SpriteRenderer>().color.a <= 0.0f)
                goDown = false;

            if(goDown){
                Color tmp = downArrow.GetComponent<SpriteRenderer>().color;
                tmp.a -= 0.04f;
                downArrow.GetComponent<SpriteRenderer>().color = tmp;
            }
            else{
                Color tmp = downArrow.GetComponent<SpriteRenderer>().color;
                tmp.a += 0.04f;
                downArrow.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
        else {
            downArrow.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "ball")
        {
            Destroy(collisionInfo.gameObject);
            counter++;
        }
        if(counter == targetBallCount){
            ballLauncher.canPull = true;
            blockSpawner.SetActive(false);
            blockSpawner.SetActive(true);
            counter = 0;
        }
    }
    void BlinkDownArrow(){
        if(downArrow.activeSelf){
            downArrow.SetActive(false);
        }
        else{
            downArrow.SetActive(true);
        }
    }
    
}
