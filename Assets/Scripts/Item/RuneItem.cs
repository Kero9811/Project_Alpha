using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneItem : Item
{
    public int cost; // ���� �ڽ�Ʈ
    public bool isEquipped; // ���� ����

    public RuneItemData runeInfo;

    public override void Awake()
    {
        InitItemInfo();
    }

    public override void InitItemInfo()
    {
        itemName = runeInfo.ItemName;
        id = runeInfo.Id;
        desc = runeInfo.Desc;
        cost = runeInfo.Cost;
        isEquipped = runeInfo.isEquipped;
    }
}