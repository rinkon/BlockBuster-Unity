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

    private void Start() {
        ballLauncher = ballLauncherGameObject.GetComponent<BallLauncher>();
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
    
}
