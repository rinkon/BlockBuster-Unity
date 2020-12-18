﻿using UnityEngine;
using System;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917";

    private bool isRevive = false;
    private bool isWinBalls = false;

    private static AdsManager _instance;
    [SerializeField]
    private GameObject loader, gameEndPopup, blurImage, blockSpawner, ballLauncher;

    public static AdsManager Instance()
    {
      if (_instance == null)
      {
        _instance = new AdsManager();
      }
      return _instance;
    }
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
        else{
            Debug.Log("Ad not loaded");
        }
    }

    public void RequestRewardAd(){
        this.rewardedAd = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.OnUserEarnedReward += UserEarned;
        this.rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
        this.rewardedAd.OnAdFailedToLoad += AdFailedToLoad;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        this.rewardedAd.LoadAd(request);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        if(isRevive){
            Toast.Instance.Show("See full ad to Recover", 3f, Toast.ToastColor.Dark);
            // SoundManagerScript.StopLoopingAndAudioSource();
            isRevive = false;
            loader.SetActive(false);
            // blurImage.SetActive(false);
            gameEndPopup.SetActive(true);
        }
        else if(isWinBalls){
            Toast.Instance.Show("See full ad to Win ball", 3f, Toast.ToastColor.Dark);
            isWinBalls = false;
            blurImage.SetActive(false);
            loader.SetActive(false);
            ballLauncher.GetComponent<BallLauncher>().WinBallsEnded();
        }
    }

    private void UserEarned(object sender, Reward e)
    {
        loader.SetActive(false);
        if(isRevive){
            gameEndPopup.SetActive(false);
            blurImage.SetActive(false);
            blockSpawner.GetComponent<BlockSpawner>().ReviveAwarded();
            blockSpawner.SetActive(false);
            blockSpawner.SetActive(true);
            isRevive = false;
            SoundManagerScript.StopLoopingAndAudioSource();
            SoundManagerScript.PlaySound("boxRowUp");
        }
        if(isWinBalls){
            Toast.Instance.Show("Bonus ball Won", 3f, Toast.ToastColor.Dark);
            SoundManagerScript.PlaySound("bonus");
            isWinBalls = false;
            blurImage.SetActive(false);
            ballLauncher.GetComponent<BallLauncher>().WinBallsEnded();
            ballLauncher.GetComponent<BallLauncher>().counter += 1;
        }
    }

    private void AdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        Toast.Instance.Show("Failed to load ads, please try after sometimes.", 3f, Toast.ToastColor.Dark);
        if(isWinBalls){
            ballLauncher.GetComponent<BallLauncher>().WinBallsEnded();
            isWinBalls = false;
        }
        isRevive = false;
        SoundManagerScript.StopLoopingAndAudioSource();
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        // hide the loader
        UserChoseToWatchAd();
    }

    public void ReviveTapped(){

        if(Application.internetReachability == NetworkReachability.NotReachable){
            // Debug.Log("Fucking check internet connection");
            Toast.Instance.Show("No internet connection", 3f, Toast.ToastColor.Dark);
        }
        else{
            // show the loader
            loader.SetActive(true);
            gameEndPopup.SetActive(false);
            isRevive = true;
            RequestRewardAd();
        }
    }

    public void WinBalls(){
        if(Application.internetReachability == NetworkReachability.NotReachable){
            // Debug.Log("Fucking check internet connection bitches");
            Toast.Instance.Show("No internet connection", 3f, Toast.ToastColor.Dark);
        }
        else{
            loader.SetActive(true);
            blurImage.SetActive(true);
            isWinBalls = true;
            RequestRewardAd();
        }
    }
}

