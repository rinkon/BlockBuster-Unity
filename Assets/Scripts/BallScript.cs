using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _ballSpeed = 10.0f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();    
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        _rb.velocity = _rb.velocity.normalized * _ballSpeed;
    }
}
