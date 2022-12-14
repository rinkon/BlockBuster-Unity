using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ballLauncherGameObject;
    private BallLauncher ballLauncher;
    private void Start() {
        ballLauncherGameObject = GameObject.Find("BallLauncher");
        ballLauncher = ballLauncherGameObject.GetComponent<BallLauncher>();
    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "ball")
        {
            SoundManagerScript.PlaySound("bonus");
            ballLauncher.counter = ballLauncher.counter + 1;
            Destroy(gameObject);
        }
    }
    
}
