using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDemoBlock());
    }

    IEnumerator MoveDemoBlock(){
        while(true){
            transform.position = transform.position + Vector3.down;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        print("yes");
    }
}
