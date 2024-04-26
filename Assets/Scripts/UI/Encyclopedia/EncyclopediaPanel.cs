using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaPanel : MonoBehaviour
{
    [SerializeField] private Transform monsterListParent;
    [SerializeField] private Transform monsterImageParent;
    [SerializeField] private Transform monsterDescParent;

    private MonsterListSlot[] monsterListSlots;

    [SerializeField] private MonsterListData monsterListData;

    private void Awake()
    {
        monsterListSlots = GetComponentsInChildren<MonsterListSlot>();
        InitMonsterName();
    }

    private void InitMonsterName()
    {
        for (int i = 0;  i < monsterListSlots.Length; i++)
        {
            monsterListSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = monsterListData.MonsterNames[i];
        }
    }

    private void UpdatePanel(MonsterData monsterData)
    {
        if (monsterData != null)
        {
            //monsterImageParent.GetComponent<Image>().
        }
    }
}