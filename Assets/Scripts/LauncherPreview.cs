using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherPreview : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 startPosition;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetStartPoint(Vector3 worldPosition){
        startPosition = worldPosition;
        lineRenderer.SetPosition(0, worldPosition);
    }

    public void SetEndPoint(Vector3 worldPosition){
        lineRenderer.SetPosition(1, worldPosition);
    }

    public void VanishLineRenderer(){
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
