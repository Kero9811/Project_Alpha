using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerStatus
{
    public int maxHp;
    public int curHp;
    public int maxMp;
    public int curMp;

    public int damage;
    public int magicDamage;

    public int curGold;
}

[System.Serializable]
public class InventoryItem
{
    public List<Item> storyItemList;
    public List<Item> abilityItemList;
}

public class DataManager : MonoBehaviour
{
    private PlayerStatus playerData = new PlayerStatus();
    private InventoryItem inventoryItem = new InventoryItem();
    private string path;
    private string playerDataFileName = "PlayerStatusData";
    private string storyItemFileName = "StoryItemData";
    private string abilityItemFileName = "AbilityItemData";

    private void Awake()
    {
        path = Application.persistentDataPath + "/";
    }

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData, true);
        string storyItemData = JsonConvert.SerializeObject(inventoryItem.storyItemList);
        string abilityItemData = JsonConvert.SerializeObject(inventoryItem.abilityItemList);

        File.WriteAllText(path + playerDataFileName, data);
        File.WriteAllText(path + storyItemFileName, storyItemData);
        File.WriteAllText(path + abilityItemFileName, abilityItemData);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + playerDataFileName);
        string storyItemData = File.ReadAllText(path + storyItemFileName);
        string abilityItemData = File.ReadAllText(path + abilityItemFileName);

        playerData = JsonUtility.FromJson<PlayerStatus>(data);
        inventoryItem.storyItemList = JsonConvert.DeserializeObject<List<Item>>(storyItemData);
        inventoryItem.abilityItemList = JsonConvert.DeserializeObject<List<Item>>(abilityItemData);
    }

    public void SavePlayerData(Player player)
    {
        playerData.maxHp = player.MaxHp;
        playerData.curHp = player.CurHp;
        playerData.maxMp = player.MaxMp;
        playerData.curMp = player.CurMp;

        playerData.damage = player.P_Attack.Damage;
        playerData.magicDamage = player.P_Attack.MagicDamage;

        playerData.curGold = player.CurGold;
    }

    public void LoadPlayerData(Player player)
    {
        player.SetPlayerStat(playerData);
        player.P_Attack.SetPlayerDamage(playerData);
    }

    public void SaveInvenItem(List<Item> storyList, List<Item> abilityList)
    {
        inventoryItem.storyItemList = new List<Item>(storyList);
        inventoryItem.abilityItemList = new List<Item>(abilityList);
    }

    /// <summary>
    /// 인벤 아이템 불러오는 메서드
    /// </summary>
    /// <param name="storyList">스토리아이템</param>
    /// <param name="abilityList">능력해금아이템</param>
    public void LoadInvenItem(List<Item> storyList, List<Item> abilityList)
    {
        storyList = new List<Item>(inventoryItem.storyItemList);
        abilityList = new List<Item>(inventoryItem.abilityItemList);
    }
}