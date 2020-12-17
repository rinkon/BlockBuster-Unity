using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip boxHitSound, boxHitSoundTwo, boxCrush, ballLauncher, bonus, boxRowDown, boxRowUp;
    static AudioSource audioSource;
    void Start()
    {
        boxHitSound = Resources.Load<AudioClip>("box hit sound");
        boxHitSoundTwo = Resources.Load<AudioClip>("boxHitSoundTwo");
        ballLauncher = Resources.Load<AudioClip>("ball launcher");
        boxCrush = Resources.Load<AudioClip>("Box crush");
        bonus = Resources.Load<AudioClip>("bonus");
        boxRowUp = Resources.Load<AudioClip>("boxRowUp");
        boxRowDown = Resources.Load<AudioClip>("boxRowDown");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip){
        switch (clip){
        case "boxHitSound":
            audioSource.PlayOneShot(boxHitSoundTwo);
            break;
        case "boxCrush":
            audioSource.PlayOneShot(boxCrush);
            break;
        case "ballLauncher":
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
        

        }

    }
}
