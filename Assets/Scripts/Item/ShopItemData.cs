using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopItemData", menuName = "Create/ShopItemData")]
public class ShopItemData : ItemData
{
    public int price;

    public virtual void Use(Player player) { Debug.Log("3"); }
}