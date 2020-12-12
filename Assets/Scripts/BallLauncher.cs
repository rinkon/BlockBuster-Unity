using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField]
    private GameObject ball;

    private LauncherPreview launcherPreview;

    public int counter = 1;
    public bool canPull = true;
    private bool canShowPreview = false;
    public float increasedSpeed = 18.0f;

    private void Awake() {
        launcherPreview = GetComponent<LauncherPreview>();
    }

    private void Update()
    {       
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;

        if (Input.GetMouseButtonDown(0) && canPull)
        {
            StartDrag(worldPosition);
            canShowPreview = true;
        }
        else if(Input.GetMouseButton(0) && canShowPreview){
            KeepDragging(worldPosition);
        }
        else if(Input.GetMouseButtonUp(0) && canShowPreview){
            EndDragging(worldPosition);
        }
    }

    private void EndDragging(Vector3 worldPosition)
    {
        launcherPreview.VanishLineRenderer();

        endPosition = worldPosition;

        Vector3 direction = startPosition - endPosition;

        StartCoroutine(BallzRain(direction));

        launcherPreview.VanishLineRenderer();

    }

    private void KeepDragging(Vector3 worldPosition)
    {
        Vector3 direction = worldPosition - startPosition;
        launcherPreview.SetEndPoint(transform.position - direction * 3);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startPosition = worldPosition;
        launcherPreview.SetStartPoint(transform.position);
    }


    IEnumerator BallzRain(Vector3 direction){
        if(direction.magnitude > 0.0f){
            canPull = false;
            canShowPreview = false;
            int localCounter = counter;
            GetComponentInParent<BottomWallScript>().targetBallCount = localCounter;
            while(localCounter > 0){
                GameObject theBall = Instantiate(ball, transform.position, Quaternion.identity);

                theBall.GetComponent<Rigidbody2D>().AddForce(direction.normalized);

                yield return new WaitForSeconds(0.06f);

                localCounter--;
            }
        }
    }

    public void MakeBallsFaster(){
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");

        print("balls " + balls.Length );

        foreach (var ball in balls)
        {
            ball.GetComponent<BallScript>().ballSpeed = increasedSpeed;
        }
    }

}
