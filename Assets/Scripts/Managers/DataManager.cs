using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerStatus //TODO: 능력 해금 여부도 저장해야함
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
    //public List<Item> storyItemList;
    //public List<Item> abilityItemList;

    public List<ObjData> storyItemDataList;
    public List<ObjData> abilityItemDataList;
}

public class DataManager : MonoBehaviour
{
    private PlayerStatus playerData = new PlayerStatus();
    private InventoryItem inventoryItem = new InventoryItem();
    private string path;
    private string playerDataFileName = "PlayerStatusData";
    private string storyItemFileName = "StoryItemData";
    private string abilityItemFileName = "AbilityItemData";

    private bool checkData = false;

    private void Awake()
    {
        path = Application.persistentDataPath + "/";
    }

    private void Start()
    {
        LoadData();

        if (false == checkData)
        {
            SaveData();
            LoadData();
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData, true);

        //string storyItemData = JsonConvert.SerializeObject(inventoryItem.storyItemList, Formatting.Indented,
        //    new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });

        string storyItemData = JsonConvert.SerializeObject(inventoryItem.storyItemDataList, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        //string abilityItemData = JsonConvert.SerializeObject(inventoryItem.abilityItemList, Formatting.Indented,
        //    new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    });

        string abilityItemData = JsonConvert.SerializeObject(inventoryItem.abilityItemDataList, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        File.WriteAllText(path + playerDataFileName, data);
        File.WriteAllText(path + storyItemFileName, storyItemData);
        File.WriteAllText(path + abilityItemFileName, abilityItemData);
    }

    public void LoadData()
    {
        string playerDataPath = path + playerDataFileName;
        string storyItemDataPath = path + storyItemFileName;
        string abilityItemDataPath = path + abilityItemFileName;

        if (false == File.Exists(playerDataPath) ||
            false == File.Exists(storyItemDataPath) ||
            false == File.Exists(abilityItemDataPath))
        {
            checkData = false;
            return;
        }
        else
        {
            checkData = true;

            string data = File.ReadAllText(playerDataPath);
            string storyItemData = File.ReadAllText(storyItemDataPath);
            string abilityItemData = File.ReadAllText(abilityItemDataPath);

            playerData = JsonUtility.FromJson<PlayerStatus>(data);
            //inventoryItem.storyItemList = JsonConvert.DeserializeObject<List<Item>>(storyItemData);
            //inventoryItem.abilityItemList = JsonConvert.DeserializeObject<List<Item>>(abilityItemData);

            inventoryItem.storyItemDataList = JsonConvert.DeserializeObject<List<ObjData>>(storyItemData);
            inventoryItem.abilityItemDataList = JsonConvert.DeserializeObject<List<ObjData>>(abilityItemData);
        }

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

    // 일단 못 씀
    //public void SaveInvenItem(List<Item> storyList, List<Item> abilityList)
    //{
    //    if (storyList != null)
    //    {
    //        if (inventoryItem.storyItemList == null)
    //        {
    //            inventoryItem.storyItemList = new List<Item>(storyList);
    //        }
    //        else
    //        {
    //            for (int i = 0; i < storyList.Count; i++)
    //            {
    //                if (!inventoryItem.storyItemList.Contains(storyList[i]))
    //                {
    //                    inventoryItem.storyItemList.Add(storyList[i]);
    //                }
    //            }
    //        }
    //    }

    //    if (abilityList != null)
    //    {
    //        if (inventoryItem.abilityItemList == null)
    //        {
    //            inventoryItem.abilityItemList = new List<Item>(abilityList);
    //        }
    //        else
    //        {
    //            for (int i = 0; i < abilityList.Count; i++)
    //            {
    //                if (!inventoryItem.abilityItemList.Contains(abilityList[i]))
    //                {
    //                    inventoryItem.abilityItemList.Add(abilityList[i]);
    //                }
    //            }
    //        }
    //    }

    //    SaveData();
    //}

    // 테스트용
    public void SaveInvenItemData(List<ObjData> storyList, List<ObjData> abilityList)
    {
        if (storyList != null)
        {
            for (int i = 0; i < storyList.Count; i++)
            {
                if (inventoryItem.storyItemDataList == null)
                {
                    inventoryItem.storyItemDataList = new List<ObjData>(storyList);
                }
                else
                {
                    if (!inventoryItem.storyItemDataList.Contains(storyList[i]))
                    {
                        inventoryItem.storyItemDataList.Add(storyList[i]);
                    }
                }
            }
        }

        if (abilityList != null)
        {
            for (int i = 0; i < abilityList.Count; i++)
            {
                if (inventoryItem.abilityItemDataList == null)
                {
                    inventoryItem.abilityItemDataList = new List<ObjData>(abilityList);
                }
                else
                {
                    if (!inventoryItem.abilityItemDataList.Contains(abilityList[i]))
                    {
                        inventoryItem.abilityItemDataList.Add(abilityList[i]);
                    }
                }
            }
        }

        SaveData();
    }

    // 일단 못 씀
    /// <summary>
    /// 인벤 아이템 불러오는 메서드
    /// </summary>
    /// <param name="storyList">스토리아이템</param>
    /// <param name="abilityList">능력해금아이템</param>
    //public void LoadInvenItem(List<Item> storyList, List<Item> abilityList)
    //{
    //    if (inventoryItem.storyItemList != null)
    //    {
    //        if (storyList == null)
    //        {
    //            storyList = new List<Item>(inventoryItem.storyItemList);
    //        }
    //        else
    //        {
    //            for (int i = 0; i < inventoryItem.storyItemList.Count; i++)
    //            {
    //                if (!storyList.Contains(inventoryItem.storyItemList[i]))
    //                {
    //                    storyList.Add(inventoryItem.storyItemList[i]);
    //                }
    //            }
    //        }
    //    }

    //    if (inventoryItem.abilityItemList != null)
    //    {
    //        if (abilityList == null)
    //        {
    //            abilityList = new List<Item>(inventoryItem.abilityItemList);
    //        }
    //        else
    //        {
    //            for (int i = 0; i < inventoryItem.abilityItemList.Count; i++)
    //            {
    //                if (!abilityList.Contains(inventoryItem.abilityItemList[i]))
    //                {
    //                    abilityList.Add(inventoryItem.abilityItemList[i]);
    //                }
    //            }
    //        }
    //    }
    //}

    // 테스트중
    public void LoadInvenItemData(List<ObjData> storyList, List<ObjData> abilityList)
    {
        if (inventoryItem.storyItemDataList != null)
        {
            for (int i = 0; i < inventoryItem.storyItemDataList.Count; i++)
            {
                if (!storyList.Contains(inventoryItem.storyItemDataList[i]))
                {
                    storyList.Add(inventoryItem.storyItemDataList[i]);
                }
            }
        }

        if (inventoryItem.abilityItemDataList != null)
        {
            for (int i = 0; i < inventoryItem.abilityItemDataList.Count; i++)
            {
                if (!abilityList.Contains(inventoryItem.abilityItemDataList[i]))
                {
                    abilityList.Add(inventoryItem.abilityItemDataList[i]);
                }
            }
        }
    }
}