using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region �̱���
    private static GameManager instance;
    public static GameManager Instance => instance;
    #endregion

    #region �Ŵ���

    private UIManager uiManager;
    private InventoryManager inventoryManager;
    private EncyclopediaManager encyclopediaManager;
    private DataManager dataManager;
    private TalkManager talkManager;
    private SceneLoader sceneLoader;
    public UIManager UI => uiManager;
    public InventoryManager Inven => inventoryManager;
    public EncyclopediaManager Encyclopedia => encyclopediaManager;
    public DataManager Data => dataManager;
    public TalkManager Talk => talkManager;
    public SceneLoader Scene => sceneLoader;

    #endregion


    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        InitManagers();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitManagers()
    {
        GameObject uiObj = new GameObject();
        uiObj.name = "UIManager";
        uiObj.transform.parent = transform;
        uiManager = uiObj.AddComponent<UIManager>();

        GameObject invenObj = new GameObject();
        invenObj.name = "InventoryManager";
        invenObj.transform.parent = transform;
        inventoryManager = invenObj.AddComponent<InventoryManager>();

        GameObject encycObj = new GameObject();
        encycObj.name = "EncyclopediaManager";
        encycObj.transform.parent = transform;
        encyclopediaManager = encycObj.AddComponent<EncyclopediaManager>();

        GameObject dataObj = new GameObject();
        dataObj.name = "DataManager";
        dataObj.transform.parent = transform;
        dataManager = dataObj.AddComponent<DataManager>();

        GameObject talkObj = new GameObject();
        talkObj.name = "TalkManager";
        talkObj.transform.parent = transform;
        talkManager = talkObj.AddComponent<TalkManager>();

        GameObject sceneObj = new GameObject();
        sceneObj.name = "SceneLoader";
        sceneObj.transform.parent = transform;
        sceneLoader = sceneObj.AddComponent<SceneLoader>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiManager.OnSceneChanged();
        sceneLoader.OnSceneLoad();
    }

    /// <summary>
    /// �� ���� ���Ӿ��� �´��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
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
