using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderScript : MonoBehaviour
{
    [SerializeField]
    private Image loader;

    private void Update() {
        
        if(loader.fillAmount <= 0.0f){
            loader.fillAmount = 1.0f;
        }
        else{
            loader.fillAmount -= 0.01f;
        }

        loader.rectTransform.Rotate(new Vector3(0, 0, -1.0f));
    }
}
