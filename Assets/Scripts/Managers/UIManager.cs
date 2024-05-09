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

    public bool isInvenOpen = false; // �κ� ���� �ִ� �� üũ
    public bool isTalkOpen = false; // ��ũâ ���� �ִ� �� üũ
    public bool isShopOpen = false; // ����â ���� �ִ� �� üũ
    private int curContentIdx = 0; // ���� ���� �ִ� Content ��ȣ ( Inventory�� ���� play�ؾ� �����۵�, �ٸ��� �ϰ� ���� ��� ����)

    private void Awake()
    {
        if (!GameManager.Instance.CheckIsGameScene()) { return; }

        GameObject uiCanvas = GameObject.FindWithTag("Canvas");

        gamePanelTf = uiCanvas.transform.Find("GamePanel");
        invenPanelTf = uiCanvas.transform.Find("InventoryPanel");
        shopPanelTf = uiCanvas.transform.Find("ShopPanel");

        contentListTf = invenPanelTf.GetChild(0).GetChild(2).GetChild(1);

        InitContentList(); // contentListObjs �ʱ�ȭ

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
            // UI ���� ��������
            ChangeContentUIPage(curContentIdx - 1);

        }
        else if (Input.GetKeyDown(KeyCode.RightBracket) && isInvenOpen == true)
        {
            // UI ������ ��������
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