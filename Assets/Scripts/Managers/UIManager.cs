using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HpUIHandler h_Handler;
    public MpUIHandler m_Handler;
    public MoneyUIHandler moneyHandler;

    private void Awake()
    {
        if (!GameManager.Instance.CheckIsGameScene()) { return; }

        Transform gamePanelTf = GameObject.FindWithTag("Canvas").transform.Find("GamePanel");

        h_Handler = gamePanelTf.GetComponentInChildren<HpUIHandler>();
        m_Handler = gamePanelTf.GetComponentInChildren<MpUIHandler>();
        moneyHandler = gamePanelTf.GetComponentInChildren<MoneyUIHandler>();
    }
}