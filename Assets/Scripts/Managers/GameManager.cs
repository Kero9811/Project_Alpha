using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager instance;
    public static GameManager Instance => instance;
    #endregion

    #region 매니저

    private UIManager uiManager;
    private InventoryManager inventoryManager;
    private EncyclopediaManager encyclopediaManager;
    private DataManager dataManager;
    private DialogueManager dialogueManager;
    public UIManager UI => uiManager;
    public InventoryManager Inven => inventoryManager;
    public EncyclopediaManager Encyclopedia => encyclopediaManager;
    public DataManager Data => dataManager;
    public DialogueManager Dialogue => dialogueManager;

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

        GameObject invenObj = new GameObject();
        invenObj.name = "InventoryManager";
        invenObj.transform.parent = transform.parent;
        inventoryManager = invenObj.AddComponent<InventoryManager>();

        GameObject encycObj = new GameObject();
        encycObj.name = "EncyclopediaManager";
        encycObj.transform.parent = transform.parent;
        encyclopediaManager = encycObj.AddComponent<EncyclopediaManager>();

        GameObject dataObj = new GameObject();
        dataObj.name = "DataManager";
        dataObj.transform.parent = transform.parent;
        dataManager = dataObj.AddComponent<DataManager>();

        GameObject dialogueObj = new GameObject();
        dialogueObj.name = "DialogueManager";
        dialogueObj.transform.parent = transform.parent;
        dialogueManager = dialogueObj.AddComponent<DialogueManager>();
    }

    /// <summary>
    /// 이 씬이 게임씬이 맞는지 확인하는 함수
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
