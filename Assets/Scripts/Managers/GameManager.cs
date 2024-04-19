using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    #region ¸Å´ÏÀú

    private UIManager uiManager;
    public UIManager UI => uiManager;

    #endregion


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        InitManagers();
    }

    private void InitManagers()
    {
        GameObject uiObj = new GameObject();
        uiObj.name = "UIManager";
        uiObj.transform.parent = transform.parent;
        uiManager = uiObj.AddComponent<UIManager>();
    }

    public bool CheckIsGameScene()
    {
        Scene curScene = SceneManager.GetActiveScene();
        if (curScene.name == "TitleScene")
        {
            print(curScene.name);
            return false;
        }
        else { return  true; }
    }
}
