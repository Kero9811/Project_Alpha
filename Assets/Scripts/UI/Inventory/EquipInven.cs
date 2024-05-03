using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipInven : MonoBehaviour
{
    [SerializeField] private List<RuneItemData> runeItems = new List<RuneItemData>(); // Dictionary가 좋을 수도 있음 (탐색을 위해)
    [SerializeField] private List<RuneItemData> equipItems = new List<RuneItemData>();

    private Slot[] runeSlots;
    private Slot[] equipSlots;
    private CostSlot[] costSlots;

    public RuneItemData[] runeItemDatas;

    private int usingCost;

    [SerializeField] private Transform runeSlotParent;
    [SerializeField] private Transform equipParent;
    [SerializeField] private Transform costParent;
    [SerializeField] private Transform descParent;
    [SerializeField] private Transform costDescParent;

    private void Awake()
    {
        runeSlots = new Slot[runeSlotParent.childCount];
        for (int i = 0; i < runeSlotParent.childCount; i++)
        {
            runeSlots[i] = runeSlotParent.GetChild(i).GetComponentInChildren<Slot>();
            runeSlots[i].isEquipInvenSlot = true;
        }

        equipSlots = new Slot[equipParent.childCount];
        for (int i = 0; i < equipParent.childCount; i++)
        {
            equipSlots[i] = equipParent.GetChild(i).GetComponentInChildren<Slot>();
            equipSlots[i].isEquipInvenSlot = true;
        }

        costSlots = new CostSlot[costParent.childCount];
        for (int i = 0; i < costParent.childCount; i++)
        {
            costSlots[i] = costParent.GetChild(i).GetComponent<CostSlot>();
        }

        UpdateRunePage();
    }

    private void Start()
    {
        GameManager.Instance.Data.LoadRuneItem(equipItems, runeItems, runeItemDatas);

        UpdateRunePage();  
    }

    private void OnEnable()
    {
        if (GameManager.Instance?.Inven != null)
        {
            AddRunesFromQueue(GameManager.Instance.Inven.runeItemQueue);
        }
    }

    private void OnDisable()
    {
        ResetRuneConfirmUI();
    }

    public void UpdateRunePage()
    {
        // 룬 업데이트
        int i = 0;
        for (; i < runeItems.Count && i < runeSlots.Length; i++)
        {
            //runeItems[i].InitItemInfo();
            runeSlots[i].runeItemData = runeItems[i];
            runeSlots[i].SetItem(runeItems[i]);
        }
        for (; i < runeSlots.Length; i++)
        {
            runeSlots[i].runeItemData = null;
            runeSlots[i].SetItem(null);
        }

        // 장착 룬 업데이트
        int j = 0;
        for (; j < equipItems.Count && j < equipSlots.Length; j++)
        {
            // 주석 풀었을때 isEquipped가 안 바뀐다면 runeitemdata가 안 바뀌는거
            //equipItems[j].InitItemInfo();
            equipSlots[j].runeItemData = equipItems[j];
            equipSlots[j].SetItem(equipItems[j]);
        }
        for (; j < equipSlots.Length; j++)
        {
            equipSlots[j].runeItemData = null;
            equipSlots[j].SetItem(null);
        }

        // 아무것도 장착 하지 않았을 경우와 종료 후 다시 시작했을 경우
        if (usingCost == 0)
        {
            for (int p = 0; p < equipItems.Count; p++)
            {
                usingCost += equipItems[p].Cost;
            }
        }

        // 코스트 업데이트
        int k = 0;
        for (; k < usingCost; k++)
        {
            costSlots[k].ChangeToOn();
        }
        for(; k < costSlots.Length; k++)
        {
            costSlots[k].ChangeToOff();
        }
    }

    public void AddRuneToPage(RuneItemData runeItem)
    {
        if (runeItems.Count < runeSlots.Length && equipItems.Count < equipSlots.Length)
        {
            if (false == runeItem.isEquipped)
            {
                runeItems.Add(runeItem);
            }
            else if (true == runeItem.isEquipped)
            {
                equipItems.Add(runeItem);
            }
        }
        else
        {
            // 게임 구조상 발생할 일 없음 (동일하다는 조건은 될 수도 있음)
            Debug.Log("Inventory is Full");
        }
    }

    public void ConfirmItemInfo(RuneItemData runeItem)
    {
        if (runeItem != null)
        {
            if (false == runeItem.isEquipped)
            {
                RuneItemData targetRune= runeItems.Find(x => x.Id == runeItem.Id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = targetRune.Image;
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetRune.ItemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetRune.Desc;

                costDescParent.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1); // 코스트 이미지
                costDescParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"x {runeItem.Cost}"; // 코스트 텍스트
            }
            else if (true == runeItem.isEquipped)
            {
                RuneItemData targetRune = equipItems.Find(x => x.Id == runeItem.Id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = targetRune.Image;
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetRune.ItemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetRune.Desc;

                costDescParent.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1); // 코스트 이미지
                costDescParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"x {runeItem.Cost}"; // 코스트 텍스트
            }
        }
        else
        {
            ResetRuneConfirmUI();
        }
    }

    private void ResetRuneConfirmUI()
    {
        Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
        targetImage.sprite = null;
        targetImage.color = new Color(1, 1, 1, 0);
        descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = "";
        descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = "";

        costDescParent.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0); // 코스트 이미지
        costDescParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""; // 코스트 텍스트
    }

    public void AddRunesFromQueue(Queue<RuneItemData> runeQueue)
    {
        if (runeQueue.Count > 0)
        {
            while (runeQueue.Count > 0)
            {
                AddRuneToPage(runeQueue.Dequeue());
            }

            UpdateRunePage();

            GameManager.Instance.Inven.runeItemQueue.Clear();

            GameManager.Instance.Data.SaveRuneItem(null, runeItems);
        }
    }

    public void EquipRune(RuneItemData runeItem)
    {
        RuneItemData targetRune = runeItems.Find(x => x.Id == runeItem.Id);

        // cost가 10을 넘거나 장착 슬롯이 다 찼을 경우 장착 불가 (cost는 10으로 fix or 진행하면서 늘려가는 걸로 수정)
        if(targetRune.Cost + usingCost > 10 || equipSlots[equipSlots.Length - 1].runeItemData != null)
        {
            Debug.Log("코스트 혹은 장착 가능한 칸이 부족합니다.");
        }
        else
        {
            targetRune.isEquipped = true;
            equipItems.Add(targetRune);
            usingCost += targetRune.Cost;
            runeItems.Remove(targetRune);
            UpdateRunePage();

            GameManager.Instance.Data.SaveRuneItem(equipItems, runeItems);
        }
    }

    public void UnEquipRune(RuneItemData runeItem)
    {
        RuneItemData targetRune = equipItems.Find(x => x.Id == runeItem.Id);
        targetRune.isEquipped = false;
        runeItems.Add(targetRune);
        equipItems.Remove(targetRune);
        usingCost -= targetRune.Cost;
        UpdateRunePage();

        GameManager.Instance.Data.SaveRuneItem(equipItems, runeItems);
    }
}