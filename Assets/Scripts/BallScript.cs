using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    public float ballSpeed = 8.0f;
    private float prevY = 0.0f;
    private float curY = 0.0f;
    private float sameYDurration = 0.0f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        _rb.velocity = _rb.velocity.normalized * ballSpeed;
        
        curY = transform.position.y;
        if(prevY == curY){
            sameYDurration += Time.deltaTime;
        }
        else{
            sameYDurration = 0.0f;
            prevY = curY;
        }
        if(sameYDurration > 3.0f){
            _rb.AddForce(new Vector3(0f, -60.0f, 0.0f));
            sameYDurration = 0;
        }
    }
}
