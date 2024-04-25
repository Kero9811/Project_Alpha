using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneItem : Item
{
    public int cost; // 장착 코스트
    public bool isEquipped; // 장착 여부

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