// Скрипт взят с сайт http://unityblog.ru/
// Библиотека https://github.com/googleads/googleads-mobile-unity/releases


using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Ad
{
	public class AdHandler : MonoBehaviour
	{
		

		
		 string bannerId="ca-app-pub-6059708192820176/6386736996";
		 string interstitialId="ca-app-pub-6059708192820176/6386736996";
		 string rewardedId="";
		 
// Девочки ca-app-pub-6059708192820176~4927521600
 				
		private InterstitialAd _interstitialAd;
		private BannerView _banner;
		private RewardedAd _rewardedAd;
		
		public static AdHandler instance;
		private bool _noAds;
		
		private string noAdsKey = "NoAds";
		public event Action GetReward;
		public bool ShowingBanner {get; private set;}
/*
bool bstart=false;

	 if (!AdHandler.instance.ShowingBanner)
	AdHandler.instance.ShowBanner(true);

	 if (AdHandler.instance.ShowingBanner)
	AdHandler.instance.ShowBanner(false);

public void StartPesnia ()
{
			if (!bstart) {
Play_ReklamaMini();
	bstart=true;
}



int bstart=0;
public void StartPesnia ()
{
		bstart++;
			if (bstart==6) {
Play_ReklamaMini();

}


using Ad;

    public void Play_ReklamaMini()
    {

AdHandler.instance.ShowBanner(true);

    }
   public void PlayReklama()
    {
 AdHandler.instance.ShowInterstitialAd();

  }
  
  IEnumerator StartReklama ()
{

	yield return new WaitForSeconds(12f);
	AdHandler.instance.ShowBanner(true);			
}
		StartCoroutine("StartReklama");
*/ 		
		
// Чтобы реклама не убиралась		
		private void Awake()
		{
			
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
				Destroy(gameObject);
		
		}
		
// Стартуем		
		private void Start()
		{
		
// Включить тестовые ID		
		//bannerId="ca-app-pub-3940256099942544/1033173712";
	//interstitialId="ca-app-pub-3940256099942544/6300978111";
	//rewardedId="ca-app-pub-3940256099942544/5224354917";
		
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1;
			
// Проверяем рекламу			
			MobileAds.Initialize(status => {});

// Грузим всю рекламу

	//		RequestRewardedVideo();
			if (!_noAds)
			{
				RequestInterstitialAd();
				RequestBanner();
			}


/*
			SceneManager.sceneLoaded += (t,i) =>
			{
				if (PlayerPrefs.GetInt("GameLose",0)==1)
				{
					PlayerPrefs.SetInt("GameLose",0);
					ShowInterstitialAd();
				}
			};
	*/		
		}
		
// Отключаем рекламу		
		public void RemoveAds()
		{
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1;
		}
		
// Показываем баннер		
		public void ShowBanner(bool show)
		{
			/*
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1; 
			if (!_noAds)
			{
				if (show){
				_banner?.Show();
					ShowingBanner=true;
				}
				else{
				_banner?.Hide();
					ShowingBanner=false;
				}
			}
			*/

		}
// Получаем баннер		
		public void RequestBanner()
		{
			_banner = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Bottom);
			AdRequest newRequest = new AdRequest.Builder().Build();
			_banner?.LoadAd(newRequest);
			_banner?.Hide();
			//GameAnalytics.gameAnalytics.BannerAd();
		}
		
// Получаем межстраничку		
		public void RequestInterstitialAd()
		{
			_interstitialAd = new InterstitialAd(interstitialId);
			AdRequest request = new AdRequest.Builder().Build();
			_interstitialAd?.LoadAd(request);
		}
// Показываем межстраничку		
		public void ShowInterstitialAd()
		{
			/*
			_noAds = PlayerPrefs.GetInt(noAdsKey, 0) == 1; 
			if (!_noAds) 
			{
				if (_interstitialAd!= null && _interstitialAd.IsLoaded())
				{
					_interstitialAd?.Show();
				//	GameAnalytics.gameAnalytics.InterstitialAd();
					RequestInterstitialAd();
				}
			}
			*/
		}
		
		public void HandleRewardBasedVideoRewarded(object sender, Reward args)
		{
			GetReward?.Invoke();
		}
		public void HandleRewardedAdClosed(object sender, EventArgs args)
		{
			RequestRewardedVideo();
		}
		private void RequestRewardedVideo()
		{
			_rewardedAd = new RewardedAd(rewardedId);
	    
			_rewardedAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
			_rewardedAd.OnAdClosed += HandleRewardedAdClosed;
	    
			AdRequest request = new AdRequest.Builder ().Build();
			_rewardedAd?.LoadAd(request);
		}
		public void ShowRewardAd()
		{
			if (_rewardedAd.IsLoaded())
			{
				_rewardedAd?.Show();
				//GameAnalytics.gameAnalytics.RewardedAd();
			} 
		}
	}
}