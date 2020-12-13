using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject leftWall, rightWall, topWall, bottomWall, blockSpawner;
    [SerializeField]
    private Button speedUpButton, winBallsButton;
    private float horizontalDistance;
    private float verticalDistance;
    private float width;
    private bool isBottomWallSet = false;

    void Awake()
    {
        // Debug.Log("Screen width " + Screen.width);

        // Debug.Log("Left wall size: " + leftWall.GetComponent<BoxCollider2D>().bounds.size);
        // print("Left wall position: " + Camera.main.WorldToViewportPoint(leftWall.transform.position));
        // print("Left wall world position: " + Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 10.0f)));
        // print("Right wall position: " + Camera.main.WorldToViewportPoint(rightWall.transform.position));
        // print("Right wall world position: " + Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 10.0f)));
        PositionWalls();
        CalculateDistances();
        GetBlockWidth();
        PositionBlockSpawner();

        // print("Horizontal distance: " + horizontalDistance);

        
    }
    private void Update() {
        if(!isBottomWallSet){
            float tempX = bottomWall.transform.position.x;
            bottomWall.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(bottomWall.transform.position.x, Camera.main.WorldToScreenPoint(winBallsButton.transform.position).y + 100.0f, 0.0f));
            bottomWall.transform.position = new Vector3(tempX, bottomWall.transform.position.y, 0);
            isBottomWallSet = true;
        }
    }

    private void PositionBlockSpawner()
    {
        blockSpawner.transform.position = new Vector2(0.0f - (horizontalDistance/2) + (0.3f + width - 0.6f), 0.0f + (verticalDistance/2) - (0.3f + width - 0.6f));
    }

    private void CalculateDistances()
    {
        horizontalDistance = Vector3.Distance(leftWall.transform.position, rightWall.transform.position);
        verticalDistance = Vector3.Distance(topWall.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, 10.0f)));
    }

    private void GetBlockWidth(){
        width = (horizontalDistance - 0.6f * 7.0f) / 8.0f;
        width += 0.6f;
        blockSpawner.GetComponent<BlockSpawner>().width = width;
    }

    private void PositionWalls(){
        leftWall.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 10.0f));
        rightWall.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 10.0f));
        topWall.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 10.0f));
        bottomWall.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, 10.0f));

        // print("Button Position " + Camera.main.WorldToScreenPoint(winBallsButton.transform.position));
        // print("BottomWall Position " + Camera.main.WorldToScreenPoint(bottomWall.transform.position));
        
        // bottomWall.transform.position = new Vector2(bottomWall.transform.position.x, winBallsButton.transform.position.y);
        // winBallsButton.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.16f, 10.0f));
        // speedUpButton.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.10f, 10.0f));

    }
}
