using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    public MpUIHandler m_Handler;

    private void Awake()
    {
        instance = this;
    }


}