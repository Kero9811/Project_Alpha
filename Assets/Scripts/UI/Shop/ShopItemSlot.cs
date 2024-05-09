using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ShopItemData shopItemData;
    private Image image;
    private TextMeshProUGUI priceText;
    ShopItemList shopItemList;

    public bool isSold;

    private void Awake()
    {
        image = GetComponent<Image>();
        priceText = transform.parent.Find("PriceText").GetComponent<TextMeshProUGUI>();
        shopItemList = GetComponentInParent<ShopItemList>();

        image.sprite = shopItemData.Image;
        priceText.text = shopItemData.price.ToString();
    }

    public void SetShopItemData()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (priceText == null)
        {
            priceText = transform.parent.Find("PriceText").GetComponent<TextMeshProUGUI>();
        }

        if (isSold)
        {
            image.color = new Color(1, 1, 1, .5f);
            priceText.color = new Color(1, 1, 1, .5f);
        }
        else
        {
            image.color = new Color(1, 1, 1, 1);
            priceText.color = new Color(1, 1, 1, 1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSold) { return; }

        GetComponentInParent<ShopItemList>().ConfirmItem(shopItemData);

        if (eventData.clickCount >= 2)
        {
            // 돈 없어도 return
            if (isSold || shopItemList.player.CurGold < shopItemData.price) { Debug.Log("돈이 부족하거나, 이미 구입한 상품"); return; }
            isSold = true;
            GetComponentInParent<ShopItemList>().BuyItem(shopItemData);
            SetShopItemData();
        }
    }
}