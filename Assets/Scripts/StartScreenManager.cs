using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro highScore;
    private void Start() {
        int highScoreInt = PlayerPrefs.GetInt("highScore", 0);
        print(highScoreInt);
        highScore.SetText(highScoreInt.ToString());
    }

    public void PlayTheGame(){
        SceneManager.LoadScene(1);
    }
}
