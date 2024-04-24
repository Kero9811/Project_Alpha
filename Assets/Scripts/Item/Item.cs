using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public int cost;
    public int id;
    public string desc;

    public ItemData Info;

    private void Awake()
    {
        InitItemInfo();
    }

    public void InitItemInfo()
    {
        itemName = Info.ItemName;
        cost = Info.Cost;
        id = Info.Id;
        desc = Info.Desc;
    }

    public abstract void Use();
}