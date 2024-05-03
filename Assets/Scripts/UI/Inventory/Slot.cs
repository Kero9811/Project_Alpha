using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image image;

    public Item item;

    public ItemData itemData;

    public RuneItemData runeItemData;

    public bool isEquipInvenSlot = false;

    private string itemName;
    private Sprite itemSprite;
    private int cost;
    private int id;
    private string desc;
    private bool isEquipped;

    public void SetItem(ItemData item)
    {
        if (item != null)
        {
            itemName = item.ItemName;
            itemSprite = item.Image;
            id = item.Id;
            desc = item.Desc;
            image.color = new Color(1, 1, 1, 1);
            image.sprite = itemSprite;
        }
        else
        {
            itemName = null;
            itemSprite = null;
            id = 0;
            desc = null;
            image.color = new Color(1, 1, 1, 0);
            image.sprite = null;
        }
    }

    public void SetItemData(ObjData item)
    {
        if (item != null)
        {
            itemName = item.itemName;
            itemSprite = item.image;
            id = item.id;
            desc = item.desc;
            image.color = new Color(1, 1, 1, 1);
            image.sprite = itemSprite;
        }
        else
        {
            itemName = null;
            itemSprite = null;
            id = 0;
            desc = null;
            image.color = new Color(1, 1, 1, 0);
            image.sprite = null;
        }
    }

    public void SetItem(RuneItemData runeItem)
    {
        if (runeItem != null)
        {
            itemName = runeItem.ItemName;
            itemSprite = runeItem.Image;
            cost = runeItem.Cost;
            id = runeItem.Id;
            desc = runeItem.Desc;
            isEquipped = runeItem.isEquipped;
            image.color = new Color(1, 1, 1, 1);
            image.sprite = itemSprite;
        }
        else
        {
            itemName = null;
            itemSprite = null;
            cost = 0;
            id = 0;
            desc = null;
            isEquipped = false;
            image.color = new Color(1, 1, 1, 0);
            image.sprite = null;
        }
    }

    //public void SetItem(RuneItem runeItemData)
    //{
    //    if (runeItemData != null)
    //    {
    //        itemName = runeItemData.itemName;
    //        itemSprite = runeItemData.itemSprite;
    //        cost = runeItemData.cost;
    //        id = runeItemData.id;
    //        desc = runeItemData.desc;
    //        isEquipped = runeItemData.isEquipped;
    //        image.color = new Color(1, 1, 1, 1);
    //        image.sprite = itemSprite;
    //    }
    //    else
    //    {
    //        itemName = null;
    //        itemSprite = null;
    //        cost = 0;
    //        id = 0;
    //        desc = null;
    //        isEquipped = false;
    //        image.color = new Color(1, 1, 1, 0);
    //        image.sprite = null;
    //    }
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null || runeItemData != null)
        {
            print("¹ÝÂ¦!");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null || runeItemData != null)
        {
            print("¹ÝÂ¦ ²¨Áü!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (false == isEquipInvenSlot)
        {
            GetComponentInParent<Inventory>().ConfirmItemInfo(itemData);
            //GetComponentInParent<Inventory>().ConfirmItemInfo(itemData);
        }
        else if (true == isEquipInvenSlot)
        {
            GetComponentInParent<EquipInven>().ConfirmItemInfo(runeItemData);

            if (eventData.clickCount >= 2 && runeItemData != null && !runeItemData.isEquipped)
            {
                GetComponentInParent<EquipInven>().EquipRune(runeItemData);
            }
            else if (eventData.clickCount >= 2 && runeItemData != null && runeItemData.isEquipped)
            {
                GetComponentInParent<EquipInven>().UnEquipRune(runeItemData);
            }
        }
    }
}