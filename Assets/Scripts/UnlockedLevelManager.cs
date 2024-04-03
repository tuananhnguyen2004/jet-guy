using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockedLevelManager : MonoBehaviour
{
    static UnlockedLevelManager _instance;
    public static UnlockedLevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<UnlockedLevelManager>();
            }
            return _instance;
        }
    }

    public int numberOfUnlockedLevels;
    public Color unlockedColor;

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

    public void AddLevel()
    {
        ++numberOfUnlockedLevels;
    }
}
