using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System.IO;

[System.Serializable]
public class ShopItemListData
{
    public bool[] shopItemSold;
}

public class ShopItemList : MonoBehaviour
{
    ShopItemSlot[] shopItemSlots;
    [SerializeField] private ShopItemData[] shopItemDatas;
    [SerializeField] private Transform shopItemNameTf;
    [SerializeField] private Transform shopItemDescTf;

    private ShopItemListData shopItemListData = new ShopItemListData();
    private string path;
    private string shopItemListDataFileName = "ShopItemData";
    private bool checkData;

    public Player player;

    private void Awake()
    {
        path = Application.persistentDataPath + "/";
        shopItemSlots = GetComponentsInChildren<ShopItemSlot>();

        LoadShopItemData();

        if (!checkData)
        {
            SaveShopItemList();
            LoadShopItemData();
        }
    }

    private void Start()
    {
        //LoadShopItemData();

        //if (!checkData)
        //{
        //    SaveShopItemList();
        //    LoadShopItemData();
        //}
    }

    private void OnEnable()
    {
        LoadShopItemList();
    }

    private void OnDisable()
    {
        shopItemNameTf.GetComponent<TextMeshProUGUI>().text = "";
        shopItemDescTf.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void SaveShopItemData()
    {
        string data = JsonConvert.SerializeObject(shopItemListData.shopItemSold);

        File.WriteAllText(path + shopItemListDataFileName, data);
    }

    private void LoadShopItemData()
    {
        string shopItemDataPath = path + shopItemListDataFileName;

        if (false == File.Exists(shopItemDataPath))
        {
            checkData = false;
            return;
        }
        else
        {
            checkData= true;
            
            string data = File.ReadAllText(shopItemDataPath);

            shopItemListData.shopItemSold = JsonConvert.DeserializeObject<bool[]>(data);
        }
    }

    private void SaveShopItemList()
    {
        if (shopItemListData.shopItemSold == null)
        {
            shopItemListData.shopItemSold = new bool[shopItemSlots.Length];
        }

        for (int i = 0; i < shopItemSlots.Length; i++)
        {
            shopItemListData.shopItemSold[i] = shopItemSlots[i].isSold;
        }

        SaveShopItemData();
    }

    private void LoadShopItemList()
    {
        for (int i = 0; i < shopItemSlots.Length; i++)
        {
            shopItemSlots[i].isSold = shopItemListData.shopItemSold[i];
            shopItemSlots[i].SetShopItemData();
        }
    }

    public void ConfirmItem(ShopItemData itemData)
    {
        shopItemNameTf.GetComponent<TextMeshProUGUI>().text = itemData.ItemName;
        shopItemDescTf.GetComponent<TextMeshProUGUI>().text = itemData.Desc;
    }

    public void BuyItem(ShopItemData itemData)
    {
        player.UseGold(itemData.price);

        if (itemData is ShopMaxHpItemData)
        {
            ShopMaxHpItemData data = (ShopMaxHpItemData)itemData;
            data.Use(player);
        }
        else if (itemData is ShopMeleeItemData)
        {
            ShopMeleeItemData data = (ShopMeleeItemData)itemData;
            data.Use(player);
        }
        else if(itemData is ShopMagicItemData)
        {
            ShopMagicItemData data = (ShopMagicItemData)itemData;
            data.Use(player);
        }

        SaveShopItemList();
    }
}