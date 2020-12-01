using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _ballSpeed = 10.0f;
    private float prevY = 0.0f;
    private float curY = 0.0f;
    private float sameYDurration = 0.0f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();    
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        _rb.velocity = _rb.velocity.normalized * _ballSpeed;
        
        curY = transform.position.y;
        if(prevY == curY){
            sameYDurration += Time.deltaTime;
        }
        else{
            sameYDurration = 0.0f;
            prevY = curY;
        }
        if(sameYDurration > 5.0f){
            print("same height for long");
            _rb.AddForce(new Vector3(0f, -6.0f, 0.0f));
            sameYDurration = 0;
        }
        
    }
}
