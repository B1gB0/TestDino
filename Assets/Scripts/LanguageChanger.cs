using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ad;

public class LanguageChanger : MonoBehaviour
{
	
public GameObject Pn_Exit;
	int reklamacount=0;
  void Start() {
	  reklamacount=PlayerPrefs.GetInt ("RekCount", 1);

	  	if (reklamacount>1){ 	
		AdHandler.instance.ShowInterstitialAd();
		AdHandler.instance.ShowBanner(true);
		 //GameAnalytics.gameAnalytics.InterstitialAd();
		//print("показываем рекламу reklamacount"+reklamacount);

		} 
			  reklamacount++;
		PlayerPrefs.SetInt ("RekCount", reklamacount);
  }
	    void Update()
	
    {
		
					if (Input.GetKeyDown (KeyCode.Escape)) {


			if (Pn_Exit.activeSelf == true)  {
				Pn_Exit.SetActive (false);
				} else { Pn_Exit.SetActive (true);}
				}
				
        
    }
	
	
    private void OnGUI()
    {
       	if  (LanguageHandler.language == LanguageType.English)  {GetComponent<Text>().text = "EN";}
		else  {GetComponent<Text>().text = "RU";}
	
    }

    public void ChangeLanguage()
    {
        LanguageHandler.language = (LanguageHandler.language == LanguageType.English) ? LanguageType.Russian : LanguageType.English;
		if  (LanguageHandler.language == LanguageType.English) {		GameObject.Find ("TextDino").GetComponent<Text>().text="Dinosaur puzzles";}
		else  {		GameObject.Find ("TextDino").GetComponent<Text>().text="Пазлы динозавры";}
    }
	
		public void Rate()
	{
		//PlayerPrefs.SetInt ("reklama", 1);
		//if (NewBanner!=null) NewBanner.Hide();
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.Mamapapa.Dino");
		
	//	AppQuit();
	}
		public void onOpenWeb (string site)
	{
		Application.OpenURL (site);
	}
	
		public void Exit()
	{
		Application.Quit();
		//BigBanner.OnAdLoaded += OnBigBannerLoaded;
		//while (!BigBanner.IsLoaded()) {
			//yield return null;
		//}

	}
	
}

