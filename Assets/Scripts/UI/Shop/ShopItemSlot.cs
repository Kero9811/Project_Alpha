using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ShopItemData shopItemData;
    

    public void SetShopItemData(ShopItemData itemData)
    {
        shopItemData = itemData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponentInParent<ShopItemList>().ConfirmItem(shopItemData);

        if (eventData.clickCount >= 2)
        {
            GetComponentInParent<ShopItemList>().BuyItem(shopItemData);
        }
    }
}