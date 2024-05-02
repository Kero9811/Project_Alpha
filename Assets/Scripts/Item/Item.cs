using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Text;

[System.Serializable]
public class ObjData
{
    public string itemName;
    public string desc;
    public int id;
    public string imagePath; // 이미지 저장경로
    //public Sprite image;
}

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public int id;
    public string desc;

    public ItemData Info;
    public ObjData data;

    public virtual void Awake()
    {
        InitItemInfo();
        InitData();
    }

    public virtual void InitItemInfo()
    {
        itemName = Info.ItemName;
        itemSprite = Info.Image;
        id = Info.Id;
        desc = Info.Desc;
    }

    public virtual void InitData() // 권한 없데
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Application.persistentDataPath);
        sb.Append("/");
        sb.Append("Sprites");
        sb.Append("/");
        sb.Append(Info.Id);

        data.itemName = Info.ItemName;
        data.desc = Info.Desc;
        data.id = Info.Id;
        //data.image = Info.Image;
        data.imagePath = sb.ToString();
    }
}