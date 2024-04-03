using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] Animator anim;
    public void LoadingLevel(int levelNum)
    {
        LevelManager.Instance.LoadLevel(levelNum + 1, anim);
        
        FirebaseManager.LogEvent("Bt_Play");
        

    }
}