﻿using UnityEngine;
using System;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;

public class AdsManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.rewardedAd = new RewardedAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);

        StartCoroutine(ShowAd());
    }

    private void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded()) {
            print("ad loaded");
            this.rewardedAd.Show();
        }
    }

    IEnumerator ShowAd()
    {
        
        yield return new WaitForSeconds(3.0f);
        UserChoseToWatchAd();
    }

}

