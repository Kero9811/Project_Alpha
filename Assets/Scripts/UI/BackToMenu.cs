using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    Player player;
    public GameObject panel;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    public void Continue()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.Data.SavePlayerData(player);

        GameManager.Instance.Scene.StartSceneLoad("TitleScene");
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.Data.SavePlayerData(player);
        GameManager.Instance.Data.SaveSceneData(GameManager.Instance.Scene.nextScene);

        Application.Quit();
    }
}
