using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelList : MonoBehaviour
{
    public List<Button> levelButtonList;

    private void Awake()
    {
        for (int i = 0; i < UnlockedLevelManager.Instance.numberOfUnlockedLevels; i++)
        {
            var button = levelButtonList[i];
            var canvasGroup = button.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            button.image.color = UnlockedLevelManager.Instance.unlockedColor;
        }
    }
}
