using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip boxHitSound, boxHitSoundTwo, boxCrush, ballLauncher, bonus, boxRowDown, boxRowUp, gameEnd, clockTicking;
    static AudioSource audioSource;
    static int isMuted;
    void Start()
    {
        boxHitSound = Resources.Load<AudioClip>("box hit sound");
        boxHitSoundTwo = Resources.Load<AudioClip>("boxHitSoundTwo");
        ballLauncher = Resources.Load<AudioClip>("ball launcher");
        boxCrush = Resources.Load<AudioClip>("Box crush");
        bonus = Resources.Load<AudioClip>("bonus");
        boxRowUp = Resources.Load<AudioClip>("boxRowUp");
        boxRowDown = Resources.Load<AudioClip>("boxRowDown");
        gameEnd = Resources.Load<AudioClip>("gameEnd");
        clockTicking = Resources.Load<AudioClip>("clockTicking");

        audioSource = GetComponent<AudioSource>();

        isMuted = PlayerPrefs.GetInt("mutedValue", 0);
    }

    public static void PlaySound(string clip, float delay = 1.0f){
        if(isMuted == 1)
            return;
        audioSource.volume = 1.0f;
        switch (clip){
        case "boxHitSound":
            audioSource.PlayOneShot(boxHitSoundTwo);
            break;
        case "boxCrush":
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(boxCrush);
            break;
        case "ballLauncher":
            audioSource.volume = 0.3f;
            audioSource.PlayOneShot(ballLauncher);
            break;
        case "bonus":
            audioSource.PlayOneShot(bonus);
            break;
        case "boxRowDown":
            audioSource.PlayOneShot(boxRowDown);
            break;
        case "boxRowUp":
            audioSource.PlayOneShot(boxRowUp);
            break;
        case "gameEnd":
            audioSource.PlayOneShot(gameEnd);
            break;
        case "clockTicking":
            audioSource.clip = clockTicking;
            audioSource.loop = true;
            audioSource.PlayDelayed(delay);
            break;
        }
    }

    public static void StopLoopingAndAudioSource(){
        audioSource.Stop();
        audioSource.loop = false;
    }
}
