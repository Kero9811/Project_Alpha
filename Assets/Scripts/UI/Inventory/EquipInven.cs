using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipInven : MonoBehaviour
{
    [SerializeField] private List<RuneItem> runeItems = new List<RuneItem>(); // Dictionary�� ���� ���� ���� (Ž���� ����)
    [SerializeField] private List<RuneItem> equipItems = new List<RuneItem>();

    private Slot[] runeSlots;
    private Slot[] equipSlots;
    private CostSlot[] costSlots;

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

    private void OnEnable()
    {
        if (GameManager.Instance?.Inven != null)
        {
            AddRunesFromQueue(GameManager.Instance.Inven.runeItemQueue);
        }
    }

    public void UpdateRunePage()
    {
        // �� ������Ʈ
        int i = 0;
        for (; i < runeItems.Count && i < runeSlots.Length; i++)
        {
            //runeItems[i].InitItemInfo();
            runeSlots[i].runeItem = runeItems[i];
            runeSlots[i].SetItem(runeItems[i]);
        }
        for (; i < runeSlots.Length; i++)
        {
            runeSlots[i].runeItem = null;
            runeSlots[i].SetItem(null);
        }

        // ���� �� ������Ʈ
        int j = 0;
        for (; j < equipItems.Count && j < equipSlots.Length; j++)
        {
            // �ּ� Ǯ������ isEquipped�� �� �ٲ�ٸ� runeitemdata�� �� �ٲ�°�
            //equipItems[j].InitItemInfo();
            equipSlots[j].runeItem = equipItems[j];
            equipSlots[j].SetItem(equipItems[j]);
        }
        for (; j < equipSlots.Length; j++)
        {
            equipSlots[j].runeItem = null;
            equipSlots[j].SetItem(null);
        }

        // �ڽ�Ʈ ������Ʈ
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

    public void AddRuneToPage(RuneItem runeItem)
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
            // ���� ������ �߻��� �� ���� (�����ϴٴ� ������ �� ���� ����)
            Debug.Log("Inventory is Full");
        }
    }

    public void ConfirmItemInfo(RuneItem runeItem)
    {
        if (runeItem != null)
        {
            if (false == runeItem.isEquipped)
            {
                RuneItem targetRune= runeItems.Find(x => x.id == runeItem.id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = targetRune.itemSprite;
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetRune.itemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetRune.desc;

                costDescParent.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1); // �ڽ�Ʈ �̹���
                costDescParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"x {runeItem.cost}"; // �ڽ�Ʈ �ؽ�Ʈ
            }
            else if (true == runeItem.isEquipped)
            {
                RuneItem targetRune = equipItems.Find(x => x.id == runeItem.id);
                Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
                targetImage.sprite = targetRune.itemSprite;
                targetImage.color = new Color(1, 1, 1, 1);
                descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = targetRune.itemName;
                descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = targetRune.desc;

                costDescParent.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1); // �ڽ�Ʈ �̹���
                costDescParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"x {runeItem.cost}"; // �ڽ�Ʈ �ؽ�Ʈ
            }
        }
        else
        {
            Image targetImage = descParent.Find("ItemImage").GetComponent<Image>();
            targetImage.sprite = null;
            targetImage.color = new Color(1, 1, 1, 0);
            descParent.Find("ItemNameText").GetComponent<TextMeshProUGUI>().text = "";
            descParent.Find("ItemDescText").GetComponent<TextMeshProUGUI>().text = "";

            costDescParent.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0); // �ڽ�Ʈ �̹���
            costDescParent.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""; // �ڽ�Ʈ �ؽ�Ʈ
        }
    }

    public void AddRunesFromQueue(Queue<RuneItem> runeQueue)
    {
        if (runeQueue.Count > 0)
        {
            while (runeQueue.Count > 0)
            {
                AddRuneToPage(runeQueue.Dequeue());
            }

            UpdateRunePage();

            GameManager.Instance.Inven.runeItemQueue.Clear();
        }
    }

    public void EquipRune(RuneItem runeItem)
    {
        RuneItem targetRune = runeItems.Find(x => x.id == runeItem.id);

        // cost�� 10�� �Ѱų� ���� ������ �� á�� ��� ���� �Ұ� (cost�� 10���� fix or �����ϸ鼭 �÷����� �ɷ� ����)
        if(targetRune.cost + usingCost > 10 || equipSlots[equipSlots.Length - 1].runeItem != null)
        {
            Debug.Log("�ڽ�Ʈ Ȥ�� ���� ������ ĭ�� �����մϴ�.");
        }
        else
        {
            targetRune.isEquipped = true;
            equipItems.Add(targetRune);
            usingCost += targetRune.cost;
            runeItems.Remove(targetRune);
            UpdateRunePage();
        }
    }

    public void UnEquipRune(RuneItem runeItem)
    {
        RuneItem targetRune = equipItems.Find(x => x.id == runeItem.id);
        targetRune.isEquipped = false;
        runeItems.Add(targetRune);
        equipItems.Remove(targetRune);
        usingCost -= targetRune.cost;
        UpdateRunePage();
    }
}