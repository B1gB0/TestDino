using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ad;
using YG;

public class LanguageChanger : MonoBehaviour
{
    private readonly List<string> _languages = new () { "en", "ru" };
    
    public GameObject Pn_Exit;
    int reklamacount = 0;

    private void Start()
    {
        reklamacount = PlayerPrefs.GetInt("RekCount", 1);

        if (reklamacount > 1)
        {
            AdHandler.instance.ShowInterstitialAd();
            AdHandler.instance.ShowBanner(true);
            //GameAnalytics.gameAnalytics.InterstitialAd();
            //print("показываем рекламу reklamacount"+reklamacount);
        }

        reklamacount++;
        PlayerPrefs.SetInt("RekCount", reklamacount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pn_Exit.activeSelf == true)
            {
                Pn_Exit.SetActive(false);
            }
            else { Pn_Exit.SetActive(true); }
        }
    }

    private void OnGUI()
    {
        YG2.SwitchLanguage(LanguageHandler.language == LanguageType.English ? _languages[0] : _languages[1]);
    }

    public void ChangeLanguage()
    {
        LanguageHandler.language = (LanguageHandler.language == LanguageType.English)
            ? LanguageType.Russian
            : LanguageType.English;
    }

    public void Rate()
    {
        //PlayerPrefs.SetInt ("reklama", 1);
        //if (NewBanner!=null) NewBanner.Hide();
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Mamapapa.Dino");

        //	AppQuit();
    }

    public void onOpenWeb(string site)
    {
        Application.OpenURL(site);
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