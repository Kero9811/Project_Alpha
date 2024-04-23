using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterAnim : MonoBehaviour
{
    public void SetUnactiveObj()
    {
        gameObject.SetActive(false);
    }

    public void PanelInvenToGame()
    {
        GameManager.Instance.UI.PanelOpen("Game");
    }
}