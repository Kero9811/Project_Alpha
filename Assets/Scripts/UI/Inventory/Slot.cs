using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image image;

    public Item item;

    public ObjData itemData;

    public RuneItem runeItem;

    public bool isEquipInvenSlot = false;

    private string itemName;
    private Sprite itemSprite;
    private int cost;
    private int id;
    private string desc;
    private bool isEquipped;

    public void SetItem(Item item)
    {
        if (item != null)
        {
            itemName = item.itemName;
            itemSprite = item.itemSprite;
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

    public void SetItemData(ObjData item)
    {
        if (item != null)
        {
            itemName = item.itemName;
            itemSprite = SpriteDeserializer.LoadSpriteFromImage(item.imagePath);
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

    public void SetItem(RuneItem runeItem)
    {
        if (runeItem != null)
        {
            itemName = runeItem.itemName;
            itemSprite = runeItem.itemSprite;
            cost = runeItem.cost;
            id = runeItem.id;
            desc = runeItem.desc;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null || runeItem != null)
        {
            print("¹ÝÂ¦!");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null || runeItem != null)
        {
            print("¹ÝÂ¦ ²¨Áü!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (false == isEquipInvenSlot)
        {
            //GetComponentInParent<Inventory>().ConfirmItemInfo(item);
            GetComponentInParent<Inventory>().ConfirmItemInfo(itemData);
        }
        else if (true == isEquipInvenSlot)
        {
            GetComponentInParent<EquipInven>().ConfirmItemInfo(runeItem);

            if (eventData.clickCount >= 2 && runeItem != null && !runeItem.isEquipped)
            {
                GetComponentInParent<EquipInven>().EquipRune(runeItem);
            }
            else if (eventData.clickCount >= 2 && runeItem != null && runeItem.isEquipped)
            {
                GetComponentInParent<EquipInven>().UnEquipRune(runeItem);
            }
        }
    }
}