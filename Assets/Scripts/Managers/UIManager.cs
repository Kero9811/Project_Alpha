using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class UIManager : MonoBehaviour
{
    public HpUIHandler h_Handler;
    public MpUIHandler m_Handler;
    public MoneyUIHandler moneyHandler;

    private Transform gamePanelTf;
    private Transform invenPanelTf;

    private Animator gamePanelAnim;
    private Animator invenPanelAnim;

    private bool isOpen = false; // 열려 있는 지 체크

    private void Awake()
    {
        if (!GameManager.Instance.CheckIsGameScene()) { return; }

        GameObject uiCanvas = GameObject.FindWithTag("Canvas");

        gamePanelTf = uiCanvas.transform.Find("GamePanel");
        invenPanelTf = uiCanvas.transform.Find("InventoryPanel");

        h_Handler = gamePanelTf.GetComponentInChildren<HpUIHandler>();
        m_Handler = gamePanelTf.GetComponentInChildren<MpUIHandler>();
        moneyHandler = gamePanelTf.GetComponentInChildren<MoneyUIHandler>();

        invenPanelAnim = invenPanelTf.gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;


            if (!isOpen)
            {
                GameObject obj = invenPanelTf.GetChild(0).gameObject;

                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                }

                invenPanelAnim.SetBool("isOpen", isOpen);
            }
            else
            {
                invenPanelAnim.SetBool("isOpen", isOpen);
            }
        }
    }
}