﻿using UnityEngine;
using System;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;

public class AdsManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917";

    private static AdsManager _instance;
    [SerializeField]
    private GameObject loader, gameEndPopup, blurImage, blockSpawner;

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
        this.rewardedAd.OnAdOpening += OpenedAd;
        this.rewardedAd.LoadAd(request);
    }

    private void UserEarned(object sender, Reward e)
    {
        loader.SetActive(false);
        gameEndPopup.SetActive(false);
        blurImage.SetActive(false);
        blockSpawner.GetComponent<BlockSpawner>().ReviveAwarded();
        blockSpawner.SetActive(false);
        blockSpawner.SetActive(true);
    }

    private void OpenedAd(object sender, EventArgs e)
    {
        loader.SetActive(false);
        gameEndPopup.SetActive(true);
    }

    private void AdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        // hide the loader
        UserChoseToWatchAd();
    }

    public void ReviveTapped(){

        if(Application.internetReachability == NetworkReachability.NotReachable){
            Debug.Log("Fucking check internet connection");
        }
        else{
            // show the loader
            loader.SetActive(true);
            gameEndPopup.SetActive(false);
            RequestRewardAd();
        }
    }

}

