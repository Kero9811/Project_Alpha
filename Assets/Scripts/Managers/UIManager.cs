using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;



public class UIManager : MonoBehaviour
{
    private GameObject gamePanel;
    private GameObject invenPanel;
    private GameObject talkPanel;

    Dictionary<string, GameObject> panelDic;

    public HpUIHandler h_Handler;
    public MpUIHandler m_Handler;
    public MoneyUIHandler moneyHandler;

    private Transform gamePanelTf;
    private Transform invenPanelTf;
    public Transform shopPanelTf;
    private Transform contentListTf;

    public TextMeshProUGUI talkText;

    private GameObject[] contentListObjs = new GameObject[4];
    private GameObject[] canvasPanels;

    private Animator gamePanelAnim;
    private Animator invenPanelAnim;

    public bool isInvenOpen = false; // 인벤 열려 있는 지 체크
    public bool isTalkOpen = false; // 토크창 열려 있는 지 체크
    public bool isShopOpen = false; // 상점창 열려 있는 지 체크
    private int curContentIdx = 0; // 현재 열려 있는 Content 번호 ( Inventory를 열고 play해야 정상작동, 다르게 하고 싶을 경우 수정)

    private void Awake()
    {
        if (!GameManager.Instance.CheckIsGameScene()) { return; }

        GameObject uiCanvas = GameObject.FindWithTag("Canvas");

        gamePanelTf = uiCanvas.transform.Find("GamePanel");
        invenPanelTf = uiCanvas.transform.Find("InventoryPanel");
        shopPanelTf = uiCanvas.transform.Find("ShopPanel");

        contentListTf = invenPanelTf.GetChild(0).GetChild(2).GetChild(1);

        InitContentList(); // contentListObjs 초기화

        h_Handler = gamePanelTf.GetComponentInChildren<HpUIHandler>();
        m_Handler = gamePanelTf.GetComponentInChildren<MpUIHandler>();
        moneyHandler = gamePanelTf.GetComponentInChildren<MoneyUIHandler>();

        invenPanelAnim = invenPanelTf.gameObject.GetComponentInChildren<Animator>();

        gamePanel = gamePanelTf.gameObject;
        invenPanel = invenPanelTf.gameObject;
        talkPanel = uiCanvas.transform.Find("TalkPanel").gameObject;

        talkText = talkPanel.GetComponentInChildren<TextMeshProUGUI>();

        panelDic = new Dictionary<string, GameObject>()
        {
            {"Game", gamePanel},
            {"Inven", invenPanel},
            {"Talk", talkPanel }
        };

        PanelOpen("Game");
    }

    private void Update()
    {
        if (isTalkOpen) { return; }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInvenOpen)
            {
                PanelOpen("Inven");

                //GameObject obj = invenPanelTf.GetChild(0).gameObject;
                //if (!obj.activeSelf)
                //{
                //    obj.SetActive(true);
                //}
                isInvenOpen = true;
                invenPanelAnim.SetBool("isOpen", true);
            }
            else
            {
                isInvenOpen = false;
                invenPanelAnim.SetBool("isOpen", false);
                //PanelOpen("Game");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket) && isInvenOpen == true)
        {
            // UI 왼쪽 페이지로
            ChangeContentUIPage(curContentIdx - 1);

        }
        else if (Input.GetKeyDown(KeyCode.RightBracket) && isInvenOpen == true)
        {
            // UI 오른쪽 페이지로
            ChangeContentUIPage(curContentIdx + 1);
        }
    }

    private void InitContentList()
    {
        for (int i = 0; i < contentListObjs.Length; i++)
        {
            contentListObjs[i] = contentListTf.GetChild(i).gameObject;
        }
    }

    private void ChangeContentUIPage(int targetIdx)
    {
        if (targetIdx < 0)
        {
            targetIdx = contentListObjs.Length - 1;
        }
        else if (targetIdx >= contentListObjs.Length)
        {
            targetIdx = 0;
        }

        contentListObjs[curContentIdx].GetComponent<Animator>().SetBool("InOut", false);
        contentListObjs[targetIdx].SetActive(true);
        contentListObjs[targetIdx].GetComponent<Animator>().SetBool("InOut", true);
        curContentIdx = targetIdx;
    }

    public void PanelOpen(string panelName)
    {
        foreach (var row in panelDic)
        {
            row.Value.SetActive(row.Key == panelName);
        }
    }

    public void UpdateGameUI()
    {
        h_Handler.OnChangeMaxHp();
        h_Handler.OnChangeHp();

        m_Handler.OnChangeMaxMp();
        m_Handler.OnChangeMp();

        moneyHandler.OnChangeMoney();
    }
}