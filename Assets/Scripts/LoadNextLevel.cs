using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] Animator changeSceneAnim;
    [SerializeField] GameObject finishedScene;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                finishedScene.SetActive(true);
            }
            else
            {
                var sceneIndex = SceneManager.GetActiveScene().buildIndex;
                LevelManager.Instance.LoadLevel(sceneIndex + 1, changeSceneAnim);
                
                if (sceneIndex - 1 == UnlockedLevelManager.Instance.numberOfUnlockedLevels)
                {
                    UnlockedLevelManager.Instance.AddLevel();
                    Debug.Log("level unlocked");
                }
            }
            ReadWriteData.Instance.SaveData();
        }
    }
}
