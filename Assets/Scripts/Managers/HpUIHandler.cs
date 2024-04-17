using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIHandler : MonoBehaviour
{
    private GameObject[] hpImagesObj;

    private int activeCount;

    public Player player;

    private void Awake()
    {
        // 데이터 받아와서 활성화 개수 조정
        activeCount = player.MaxHp;

        hpImagesObj = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            hpImagesObj[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        OnChangeMaxHp();
        OnChangeHp();
    }

    public void OnChangeHp()
    {
        int curHpCount = player.CurHp;
        activeCount = player.MaxHp;

        for (int i = 0; i < curHpCount; i++)
        {
            hpImagesObj[i].GetComponent<Image>().color = Color.red;
        }

        for (int i = curHpCount; i < activeCount; i++)
        {
            hpImagesObj[i].GetComponent<Image>().color = Color.white;
        }
    }

    public void OnChangeMaxHp()
    {
        for (int i = 0; i < activeCount; i++)
        {
            hpImagesObj[i].SetActive(true);
        }

        for (int i = activeCount; i < 10; i++)
        {
            hpImagesObj[i].SetActive(false);
        }
    }
}