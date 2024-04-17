using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public int cost;
    public int id;
    public string desc;

    public SO_Item info;

    private void Awake()
    {
        itemName = info.ItemName;
        cost = info.Cost;
        id = info.Id;
        desc = info.Desc;
    }

    public abstract void Use();
}