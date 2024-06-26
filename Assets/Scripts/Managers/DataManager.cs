using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerStatus
{
    public int maxHp = 3;
    public int curHp = 3;
    public int maxMp = 100;
    public int curMp;

    public int damage = 2;
    public int magicDamage = 4;

    public int curGold;
    public int maxGold;

    public bool canDoubleJump;
    public bool canWallSlide;
}

[System.Serializable]
public class InventoryItem
{
    public List<int> storyItemIdList;
    public List<int> abilityItemIdList;
    public List<int> ownRuneItemIdList;
    public List<int> equipRuneItemIdList;
}

[System.Serializable]
public class EncyclopediaKillCount
{
    public Dictionary<int, int> encyclopediaDataDict = new Dictionary<int, int>()
    {
        {1, 0},
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
        {6, 0},
        {7, 0},
        {8, 0},
        {9, 0},
        {10, 0},
    };
}

[System.Serializable]
public class SceneName
{
    public string sceneName;
}

[System.Serializable]
public class NpcTalkIndex
{
    public int shopNpcIndex;
    public int villegerNpcIndex;
}

public class DataManager : MonoBehaviour
{
    private PlayerStatus playerData = new PlayerStatus();
    public PlayerStatus PlayerData => playerData;
    private InventoryItem inventoryItem = new InventoryItem();
    private EncyclopediaKillCount encycData = new EncyclopediaKillCount();
    private SceneName sceneName = new SceneName();
    private NpcTalkIndex npcTalkIndex = new NpcTalkIndex();
    private string path;
    private string playerDataFileName = "PlayerStatusData";
    private string storyItemFileName = "StoryItemData";
    private string abilityItemFileName = "AbilityItemData";
    private string ownRuneItemFileName = "OwnRuneItemData";
    private string equipRuneItemFileName = "EquipRuneItemData";
    private string encyclopediaDataFileName = "EncyclopediaCountData";
    private string sceneFileName = "SceneData";
    private string npcTalkIndexFileName = "TalkIndexData";

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
        string storyItemIdData = JsonConvert.SerializeObject(inventoryItem.storyItemIdList);
        string abilityItemIdData = JsonConvert.SerializeObject(inventoryItem.abilityItemIdList);
        string ownRuneItemIdData = JsonConvert.SerializeObject(inventoryItem.ownRuneItemIdList);
        string equipRuneItemIdData = JsonConvert.SerializeObject(inventoryItem.equipRuneItemIdList);
        string encyclopediaData = JsonConvert.SerializeObject(encycData.encyclopediaDataDict);
        string sceneData = JsonUtility.ToJson(sceneName, true);
        string talkData = JsonUtility.ToJson(npcTalkIndex, true);

        File.WriteAllText(path + playerDataFileName, data);
        File.WriteAllText(path + storyItemFileName, storyItemIdData);
        File.WriteAllText(path + abilityItemFileName, abilityItemIdData);
        File.WriteAllText(path + ownRuneItemFileName, ownRuneItemIdData);
        File.WriteAllText(path + equipRuneItemFileName, equipRuneItemIdData);
        File.WriteAllText(path + encyclopediaDataFileName, encyclopediaData);
        File.WriteAllText(path + sceneFileName, sceneData);
        File.WriteAllText(path + npcTalkIndexFileName, talkData);
    }

    public void LoadData()
    {
        string playerDataPath = path + playerDataFileName;
        string storyItemDataPath = path + storyItemFileName;
        string abilityItemDataPath = path + abilityItemFileName;
        string ownRuneItemDataPath = path + ownRuneItemFileName;
        string equipRuneItemDataPath = path + equipRuneItemFileName;
        string encyclopediaDataPath = path + encyclopediaDataFileName;
        string sceneDataPath = path + sceneFileName;
        string npcTalkDataPath = path + npcTalkIndexFileName;

        if (false == File.Exists(playerDataPath) ||
            false == File.Exists(storyItemDataPath) ||
            false == File.Exists(abilityItemDataPath) ||
            false == File.Exists(ownRuneItemDataPath) ||
            false == File.Exists(equipRuneItemDataPath) ||
            false == File.Exists(encyclopediaDataPath) || 
            false == File.Exists(sceneDataPath) || 
            false == File.Exists(npcTalkDataPath))
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
            string ownRuneItemData = File.ReadAllText(ownRuneItemDataPath);
            string equipRuneItemData = File.ReadAllText(equipRuneItemDataPath);
            string encyclopediaData = File.ReadAllText(encyclopediaDataPath);
            string sceneData = File.ReadAllText(sceneDataPath);
            string talkData = File.ReadAllText(npcTalkDataPath);

            playerData = JsonUtility.FromJson<PlayerStatus>(data);
            inventoryItem.storyItemIdList = JsonConvert.DeserializeObject<List<int>>(storyItemData);
            inventoryItem.abilityItemIdList = JsonConvert.DeserializeObject<List<int>>(abilityItemData);
            inventoryItem.ownRuneItemIdList = JsonConvert.DeserializeObject<List<int>>(ownRuneItemData);
            inventoryItem.equipRuneItemIdList = JsonConvert.DeserializeObject<List<int>>(equipRuneItemData);
            encycData.encyclopediaDataDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(encyclopediaData);
            sceneName = JsonUtility.FromJson<SceneName>(sceneData);
            npcTalkIndex = JsonUtility.FromJson<NpcTalkIndex>(talkData);
            GameManager.Instance.Scene.SetSceneName(sceneName.sceneName);
        }

    }

    public void SaveTalkIndex(int talkIndex, bool isShop, bool isVilleger)
    {
        if (isShop)
        {
            npcTalkIndex.shopNpcIndex = talkIndex;
        }
        else if (isVilleger)
        {
            npcTalkIndex.villegerNpcIndex = talkIndex;
        }

        SaveData();
    }

    public void LoadTalkIndex(ref int talkIndex, bool isShop, bool isVilleger, ref int curTalkIndex)
    {
        if (isShop)
        {
            talkIndex = npcTalkIndex.shopNpcIndex;

            if (npcTalkIndex.shopNpcIndex != 0)
            {
                curTalkIndex = talkIndex;
            }
        }
        else if (isVilleger)
        {
            talkIndex = npcTalkIndex.villegerNpcIndex;

            if (npcTalkIndex.villegerNpcIndex != 0)
            {
                curTalkIndex = talkIndex;
            }
        }
    }

    public void SaveSceneData(string curSceneName)
    {
        if (curSceneName == "TitleScene")
        {
            sceneName.sceneName = GameManager.Instance.Scene.prevScene;
        }
        else
        {
            sceneName.sceneName = curSceneName;
        }

        SaveData();
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
        playerData.maxGold = player.MaxGold;

        playerData.canDoubleJump = player.P_Move.CanDoubleJump;
        playerData.canWallSlide = player.P_Move.CanWallSlide;

        SaveData();
    }

    public void LoadPlayerData(Player player)
    {
        player.SetPlayerStat(playerData);
        player.P_Attack.SetPlayerDamage(playerData);
        player.P_Move.SetPlayerAbility(playerData);
    }

    public void SaveInvenItem(List<ItemData> storyList, List<ItemData> abilityList)
    {
        if (storyList != null)
        {
            if (inventoryItem.storyItemIdList == null)
            {
                inventoryItem.storyItemIdList = new List<int>();
            }

            for (int i = 0; i < storyList.Count; i++)
            {
                if (!inventoryItem.storyItemIdList.Contains(storyList[i].Id))
                {
                    inventoryItem.storyItemIdList.Add(storyList[i].Id);
                }
            }
        }

        if (abilityList != null)
        {
            if (inventoryItem.abilityItemIdList == null)
            {
                inventoryItem.abilityItemIdList = new List<int>();
            }

            for (int i = 0; i < abilityList.Count; i++)
            {
                if (!inventoryItem.abilityItemIdList.Contains(abilityList[i].Id))
                {
                    inventoryItem.abilityItemIdList.Add(abilityList[i].Id);
                }
            }
        }

        SaveData();
    }

    public void SaveRuneItem(List<RuneItemData> equipRuneList, List<RuneItemData> ownRuneList)
    {
        if (equipRuneList != null)
        {
            List<int> runeList = new List<int>();

            for (int i = 0; i < equipRuneList.Count; i++)
            {
                runeList.Add(equipRuneList[i].Id);
            }

            inventoryItem.equipRuneItemIdList = null;
            inventoryItem.equipRuneItemIdList = runeList;
        }

        if (ownRuneList != null)
        {
            List<int> runeList = new List<int>();

            for (int i = 0; i < ownRuneList.Count; i++)
            {
                runeList.Add(ownRuneList[i].Id);
            }

            inventoryItem.ownRuneItemIdList = null;
            inventoryItem.ownRuneItemIdList = runeList;
        }

        SaveData();
    }

    public void SaveRuneItem(Queue<RuneItemData> runeItems)
    {
        if (runeItems != null)
        {
            Queue<RuneItemData> runeItemsCopy = new Queue<RuneItemData>(runeItems);

            List<int> runeList = new List<int>();

            while(runeItemsCopy.Count > 0)
            {
                runeList.Add(runeItemsCopy.Dequeue().Id);
            }

            inventoryItem.ownRuneItemIdList = null;
            inventoryItem.ownRuneItemIdList = runeList;
        }

        SaveData();
    }


    /// <summary>
    /// 인벤 아이템 불러오는 메서드 (인벤 로드 최초 1회만 실행)
    /// </summary>
    /// <param name="storyList">스토리아이템</param>
    /// <param name="abilityList">능력해금아이템</param>
    public void LoadInvenItem(List<ItemData> storyList, List<ItemData> abilityList, ItemData[] itemDatas)
    {
        if (inventoryItem.storyItemIdList != null)
        {
            for (int i = 0; i < inventoryItem.storyItemIdList.Count; i++)
            {
                storyList.Add(FindItemWithId(inventoryItem.storyItemIdList[i], itemDatas));
            }
        }

        if (inventoryItem.abilityItemIdList != null)
        {
            for (int i = 0; i < inventoryItem.abilityItemIdList.Count; i++)
            {
                abilityList.Add(FindItemWithId(inventoryItem.abilityItemIdList[i], itemDatas));
            }
        }
    }

    public void LoadRuneItem(List<RuneItemData> equipRuneList, List<RuneItemData> ownRuneList, RuneItemData[] runeItemDatas)
    {
        if (inventoryItem.equipRuneItemIdList != null)
        {
            for (int i = 0; i < inventoryItem.equipRuneItemIdList.Count; i++)
            {
                equipRuneList.Add(FindItemWithId(inventoryItem.equipRuneItemIdList[i], runeItemDatas));
            }
        }

        if (inventoryItem.ownRuneItemIdList != null)
        {
            for (int i = 0; i < inventoryItem.ownRuneItemIdList.Count; i++)
            {
                ownRuneList.Add(FindItemWithId(inventoryItem.ownRuneItemIdList[i], runeItemDatas));
            }
        }
    }

    private void InitEncyclopedia(MonsterListData monsterListData)
    {
        encycData.encyclopediaDataDict = new();

        for (int i = 0; i < monsterListData.MonsterDatas.Length; i++)
        {
            encycData.encyclopediaDataDict.
                Add(monsterListData.MonsterDatas[i].MonsterId, monsterListData.MonsterKillCounts[i]);
        }
    }

    public void SaveEncyclopedia(MonsterData monsterData)
    {
        if (encycData.encyclopediaDataDict == null) { print("1"); }
        encycData.encyclopediaDataDict[monsterData.MonsterId]++;
        SaveData();
    }

    public void LoadEncyclopedia(MonsterListData monsterListData)
    {
        if (encycData.encyclopediaDataDict == null)
        {
            InitEncyclopedia(monsterListData);
            return;
        }

        for (int i = 0; i < monsterListData.MonsterDatas.Length; i++)
        {
            monsterListData.MonsterKillCounts[i] = encycData.encyclopediaDataDict[monsterListData.MonsterDatas[i].MonsterId];
        }

        GameManager.Instance.Encyclopedia.monsterDictionary = new Dictionary<int, int>(encycData.encyclopediaDataDict);
    }

    private T FindItemWithId<T>(int id, T[] itemDatas) where T : ItemData
    {
        for (int i = 0; i < itemDatas.Length; i++)
        {
            if (itemDatas[i].Id == id)
            {
                return itemDatas[i];
            }
        }

        return null;
    }
}