using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        string targetSceneName = GameManager.Instance.Scene.nextScene;

        if(targetSceneName == "TitleScene")
        {
            targetSceneName = GameManager.Instance.Scene.prevScene;
        }

        GameManager.Instance.Scene.StartSceneLoad(targetSceneName);
    }
}
