using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro highScore;
    [SerializeField]
    private Button muteButton;
    private void Start() {
        int highScoreInt = PlayerPrefs.GetInt("highScore", 0);
        print(highScoreInt);
        highScore.SetText(highScoreInt.ToString());

        int muted = PlayerPrefs.GetInt("mutedValue", 0);
        if(muted == 0){
            // mutebutton text to mute
        }
        else{
            // mute button text to unmute
        }

    }

    public void PlayTheGame(){
        SceneManager.LoadScene(1);
    }
    public void MutePressed(){
        int muted = PlayerPrefs.GetInt("mutedValue", 0);
        if(muted == 0){
            muted = 1;
            // mute button text to unmute
        }
        else{
            muted = 0;
            // mute button text to mute
        }
        PlayerPrefs.SetInt("mutedValue", muted);
    }

    public void RateUsPressed(){
        Application.OpenURL("market://details?id=com.voocatta.beatthebox");
    }
}
