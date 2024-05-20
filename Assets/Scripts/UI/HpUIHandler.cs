using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIHandler : MonoBehaviour
{
    private GameObject[] hpImagesObj;

    [SerializeField] private Sprite fullImage;
    [SerializeField] private Sprite emptyImage;

    private int activeCount;

    public Player player;

    //private void Awake()
    //{
    //    if (player == null)
    //    { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    //    activeCount = player.MaxHp;

    //    hpImagesObj = new GameObject[transform.childCount];
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        hpImagesObj[i] = transform.GetChild(i).gameObject;
    //    }
    //}

    private void Start()
    {
        if (player == null)
        { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

        activeCount = player.MaxHp;

        hpImagesObj = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            hpImagesObj[i] = transform.GetChild(i).gameObject;
        }

        OnChangeMaxHp();
        OnChangeHp();
    }

    private void OnEnable()
    {
        if(hpImagesObj == null) { return; }

        OnChangeMaxHp();
        OnChangeHp();
    }

    public void OnChangeHp()
    {
        int curHpCount = player.CurHp;

        for (int i = 0; i < curHpCount; i++)
        {
            hpImagesObj[i].GetComponent<Animator>().SetBool("isFull", true);

        }

        for (int i = curHpCount; i < activeCount; i++)
        {
            hpImagesObj[i].GetComponent<Animator>().SetBool("isFull", false);
        }
    }

    public void OnChangeMaxHp()
    {
        if (player == null) { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }
        activeCount = player.MaxHp;

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