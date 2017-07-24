using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdMob : MonoBehaviour {

	private RewardBasedVideoAd rewardBasedVideoAd;

	void Start()
	{
		DontDestroyOnLoad (this.gameObject);
		showBannerAd(); 
		//ShowRewardBasedAd ();
	}
	//------------------------Google Admob---------------------
	private void showBannerAd()
	{
		string adID = "ca-app-pub-3940256099942544/6300978111";

		//For Testing in the Device
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
			.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
			.Build();

		//For Production When Submit App
		//AdRequest request = new AdRequest.Builder().Build();

		BannerView bannerAd = new BannerView(adID, AdSize.SmartBanner, AdPosition.Bottom);
		bannerAd.LoadAd(request);
	}
	private void setRewardBasedAd ()
	{
		rewardBasedVideoAd = RewardBasedVideoAd.Instance;
		rewardBasedVideoAd.OnAdClosed += HandleOnAdClosed;
		rewardBasedVideoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		rewardBasedVideoAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
		rewardBasedVideoAd.OnAdLoaded += HandleOnAdLoaded;
		rewardBasedVideoAd.OnAdOpening += HandleOnAdOpening;
		rewardBasedVideoAd.OnAdRewarded += HandleOnAdRewarded;
		rewardBasedVideoAd.OnAdStarted += HandleOnAdStarted;
	}

	private void LoadRewardBasedAd(){
		#if UNITY_EDITOR
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3175379836062388/6485873158";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		rewardBasedVideoAd.LoadAd (new AdRequest.Builder ().Build (), adUnitId);
	}

		private void ShowRewardBasedAd(){
			if (rewardBasedVideoAd.IsLoaded ()) {
				rewardBasedVideoAd.Show ();
			} else {
				Debug.Log ("Error Loading reward based videoAd");
			}
		}

		public void HandleOnAdLoaded(object sender,EventArgs args){

		}

		public void HandleOnAdFailedToLoad(object sender,AdFailedToLoadEventArgs args){
		//try reload

		}

		public void HandleOnAdOpening(object sender,EventArgs args){
		//pause the game
		}

		public void HandleOnAdStarted(object sender,EventArgs args){

		}

		public void HandleOnAdClosed(object sender,EventArgs args){

		}

		public void HandleOnAdRewarded(object sender,Reward args){
		Debug.Log ("Reward recived!!!");

		}

		public void HandleOnAdLeavingApplication(object sender,EventArgs args){

		}
		//------------------------------------------------------------------------
}
