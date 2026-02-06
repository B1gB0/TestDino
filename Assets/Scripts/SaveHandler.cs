using UnityEngine;

public static class SaveHandler
{
    private const string levelsBuyKey = "LevelsBuy";

    public static void SaveBuyState(bool state)
    {
        if (PlayerPrefs.HasKey(levelsBuyKey))
        {
            PlayerPrefs.SetString(levelsBuyKey, state.ToString());
        }
        else
        {
            PlayerPrefs.SetString(levelsBuyKey, state.ToString());
        }
    }

    public static string GetBuyState()
    {
        if (PlayerPrefs.HasKey(levelsBuyKey))
            return PlayerPrefs.GetString(levelsBuyKey);

        return "";
    }
}
