using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaPanel : MonoBehaviour
{
    [SerializeField] private Transform monsterListParent;
    [SerializeField] private Transform monsterImageParent;
    [SerializeField] private Transform monsterDescParent;

    private MonsterListSlot[] monsterListSlots;

    [SerializeField] private MonsterListData monsterListData;

    private void Awake()
    {
        monsterListSlots = monsterListParent.GetComponentsInChildren<MonsterListSlot>();
    }

    private void OnEnable()
    {
        UpdateKillInfo();
        InitMonsterName();
        InitMonsterData();
    }

    private void OnDisable()
    {
        ResetMonsterConfirmUI();
    }

    private void InitMonsterName()
    {
        for (int i = 0;  i < monsterListSlots.Length; i++)
        {
            if (monsterListData.MonsterKillCounts[i] != 0)
            {
                monsterListSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = monsterListData.MonsterNames[i];
            }
            else
            {
                monsterListSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = "???";
            }
        }
    }

    private void InitMonsterData()
    {
        for (int i = 0; i < monsterListSlots.Length; i++)
        {
            if (monsterListData.MonsterKillCounts[i] != 0)
            {
                if (monsterListSlots[i].monsterData == null)
                {
                    MonsterData targetData = monsterListData.MonsterDatas[i];
                    monsterListSlots[i].monsterData = targetData;
                    monsterListSlots[i].SetMonsterData(targetData);
                }
            }
        }
    }

    public void ConfirmMonsterInfo(MonsterData monsterData)
    {
        if (monsterData != null)
        {
            MonsterData targetData = FindTargetMonsterData(monsterData);
            
            if (targetData != null)
            {
                monsterDescParent.gameObject.SetActive(true);

                monsterImageParent.GetComponent<Image>().sprite = targetData.monsterSprite;
                monsterImageParent.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                monsterDescParent.Find("MonsterNameText").GetComponent<TextMeshProUGUI>().text = targetData.MonsterName;
                monsterDescParent.Find("MonsterDescText").GetComponent<TextMeshProUGUI>().text = targetData.Desc;
                monsterDescParent.Find("MonsterKillText").GetComponent<TextMeshProUGUI>().text =
                    $"Kill : {GameManager.Instance.Encyclopedia.monsterDictionary[targetData.MonsterId]}";
            }
        }
        else
        {
            ResetMonsterConfirmUI();
        }
    }

    private void ResetMonsterConfirmUI()
    {
        monsterImageParent.GetComponent<Image>().sprite = null;
        monsterImageParent.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        monsterDescParent.Find("MonsterNameText").GetComponent<TextMeshProUGUI>().text = "";
        monsterDescParent.Find("MonsterDescText").GetComponent<TextMeshProUGUI>().text = "";
        monsterDescParent.Find("MonsterKillText").GetComponent<TextMeshProUGUI>().text = "";

        monsterDescParent.gameObject.SetActive(false);
    }

    private MonsterData FindTargetMonsterData(MonsterData monsterData)
    {
        for (int i = 0; i < monsterListData.MonsterDatas.Length; i++)
        {
            if (monsterListData.MonsterDatas[i].MonsterId == monsterData.MonsterId)
            {
                return monsterListData.MonsterDatas[i];
            }
        }

        return null;
    }

    private void UpdateKillInfo()
    {
        for (int i = 0; i < monsterListSlots.Length; i++)
        {
            int killCount = 0;
            // 해당 키가 존재하는지 확인하고, 존재할 경우 해당 값으로 설정
            if (GameManager.Instance.Encyclopedia.monsterDictionary.TryGetValue(monsterListData.MonsterDatas[i].MonsterId, out killCount))
            {
                monsterListData.MonsterKillCounts[i] = killCount;
            }
            else
            {
                // 해당 키가 존재하지 않을 경우 0으로 설정
                monsterListData.MonsterKillCounts[i] = 0;
            }
        }
    }
}