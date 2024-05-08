using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopItemData", menuName = "Create/ShopItemData")]
public class ShopItemData : ItemData
{
    public bool isSold;
    public int price;
}