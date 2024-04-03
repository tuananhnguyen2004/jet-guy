using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public int unlockedLevelNum;
    public float volumeValue;
    public float musicValue;
}

public class ReadWriteData : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider masterVolumeSlider;
    static ReadWriteData _instance;
    public static ReadWriteData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<ReadWriteData>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        GameData gameData = new GameData();
        gameData.unlockedLevelNum = UnlockedLevelManager.Instance.numberOfUnlockedLevels;
        gameData.volumeValue = masterVolumeSlider.value;
        gameData.musicValue = musicSlider.value;

        string json = JsonUtility.ToJson(gameData, true);
        PlayerPrefs.SetString("GameData", json);
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString("GameData");
        var gameData = JsonUtility.FromJson<GameData>(json);
        UnlockedLevelManager.Instance.numberOfUnlockedLevels = gameData.unlockedLevelNum;
        masterVolumeSlider.value = gameData.volumeValue;
        musicSlider.value = gameData.musicValue;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            LoadData();
        }
    }
}


