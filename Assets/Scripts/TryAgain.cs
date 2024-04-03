using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TryAgain : MonoBehaviour
{
    Button tryAgainBtn;
    [SerializeField] Animator anim;

    void Awake()
    {
        tryAgainBtn = GetComponent<Button>();
    } 

    void Start()
    {
        tryAgainBtn.onClick.AddListener(delegate { LevelManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex, anim); });
    }
}
