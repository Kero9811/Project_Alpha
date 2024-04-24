using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image image;

    public Item item;

    private string itemName;
    private Sprite itemSprite;
    private int cost;
    private int id;
    private string desc;

    public void SetItem(Item item)
    {
        if (item != null)
        {
            itemName = item.itemName;
            itemSprite = item.itemSprite;
            cost = item.cost;
            id = item.id;
            desc = item.desc;
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
            image.color = new Color(1, 1, 1, 0);
            image.sprite = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            print("¹ÝÂ¦!");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            print("¹ÝÂ¦ ²¨Áü!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponentInParent<Inventory>().ConfirmItemInfo(item);
    }
}