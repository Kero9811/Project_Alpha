using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string prevScene = "TitleScene";
    public string nextScene = "TownScene";
    public bool isDead = false;
    GameObject loadingPanel;
    Player player;

    private void Awake()
    {
        loadingPanel = GameObject.FindWithTag("Canvas").transform.Find("LoadingPanel").gameObject;
    }

    public void OnSceneLoad()
    {
        loadingPanel = GameObject.FindWithTag("Canvas").transform.Find("LoadingPanel").gameObject;
        if (SceneManager.GetActiveScene().name != "TitleScene")
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    public void SetSceneName(string name)
    {
        prevScene = nextScene;
        nextScene = name;
    }

    public void StartSceneLoad(string sceneName)
    {
        prevScene = SceneManager.GetActiveScene().name;
        nextScene = sceneName;

        if (prevScene == "" && nextScene == "")
        {
            prevScene = "TitleScene";
            nextScene = "TownScene";
        }

        if(nextScene == "TitleScene")
        {
            GameManager.Instance.Data.SaveSceneData(prevScene);
        }
        else
        {
            GameManager.Instance.Data.SaveSceneData(nextScene);
        }

        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        if (player != null)
        {
            GameManager.Instance.Data.SavePlayerData(player);
        }
        loadingPanel.SetActive(true);

        if (nextScene == "")
        {
            nextScene = "TownScene";
            GameManager.Instance.Data.SaveSceneData(nextScene);
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
            }

            yield return null;
        }

        //loadingPanel.SetActive(false);
    }
}
