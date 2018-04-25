using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

    public static AdManager Instance { set; get; }

    InterstitialAd interstitial;

    // Use this for initialization
    void Start()
    {
        //Request Ads
        RequestBanner();
        RequestInterstitial();
    }

    public void showInterstitialAd()
    {
        if (PlayerPrefs.HasKey("noads"))
            return;
        //Show Ad
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }

    }

    private void RequestBanner()
    {
        if (PlayerPrefs.HasKey("noads"))
            return;
#if UNITY_EDITOR
        string adUnitId = "unused";
    #elif UNITY_ANDROID
			string adUnitId = "ca-app-pub-4918355276995760/7637437473";
    #elif UNITY_IPHONE
			string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
    #else
			string adUnitId = "unexpected_platform";
    #endif

        // Create a 320x50 banner at the bottom of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        if (PlayerPrefs.HasKey("noads"))
            return;
#if UNITY_EDITOR
        string adUnitId = "unused";
    #elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-4918355276995760/1016188807";
    #elif UNITY_IPHONE
			string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
    #else
			string adUnitId = "unexpected_platform";
    #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.

        interstitial.LoadAd(request);
    }
}
