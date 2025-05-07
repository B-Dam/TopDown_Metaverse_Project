using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private string SaveKey => "PlayerData";

    public void Save()
    {
        string json = JsonUtility.ToJson(GameSession.Instance.PlayerData);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            GameSession.Instance.PlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }
}