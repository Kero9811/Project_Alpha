using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ShopItemListData
{
    public Dictionary<int, bool> shopItemDic = new Dictionary<int, bool>();
}

public class ShopItemList : MonoBehaviour
{
    ShopItemSlot[] shopItemSlots;
    [SerializeField] private ShopItemData[] shopItemDatas;

    private void Awake()
    {
        shopItemSlots = GetComponentsInChildren<ShopItemSlot>();
    }

    public void ConfirmItem(ShopItemData itemData)
    {

    }

    public void BuyItem(ShopItemData itemData)
    {

    }
}