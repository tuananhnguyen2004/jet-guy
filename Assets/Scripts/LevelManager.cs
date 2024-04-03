using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;
    static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
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

    public async void LoadLevel(int index, Animator anim)
    {
        if (index == 0)
        {
            AudioManager.Instance.PlaySound("Theme");
        }
        Singleton.Instance.CloseOptionsWindow();
        anim.SetTrigger("isEndTrans");
        await Task.Delay(1000);
        SceneManager.LoadSceneAsync(index);
        this.currentLevel = index;

        FirebaseManager.LogEvent($"Play_LV_{ this.currentLevel}");

    }
}
