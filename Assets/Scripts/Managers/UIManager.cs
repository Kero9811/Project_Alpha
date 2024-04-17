using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    public HpUIHandler h_Handler;
    public MpUIHandler m_Handler;

    private void Awake()
    {
        instance = this;

        if (!GameManager.Instance.CheckIsGameScene()) { return; }

        h_Handler = GameObject.FindWithTag("Canvas").transform.Find("HpUI").GetComponent<HpUIHandler>();
        m_Handler = GameObject.FindWithTag("Canvas").transform.Find("MpUI").GetComponentInChildren<MpUIHandler>();
    }



}