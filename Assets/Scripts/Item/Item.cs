using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public int id;
    public string desc;

    public ItemData Info;

    public virtual void Awake()
    {
        InitItemInfo();
    }

    public virtual void InitItemInfo()
    {
        itemName = Info.ItemName;
        id = Info.Id;
        desc = Info.Desc;
    }
}