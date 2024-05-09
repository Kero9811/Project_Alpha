using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Text;

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
        itemSprite = Info.Image;
        id = Info.Id;
        desc = Info.Desc;
    }

    public virtual void Use() { }
}