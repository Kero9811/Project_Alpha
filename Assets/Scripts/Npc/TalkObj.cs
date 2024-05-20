using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkObj : MonoBehaviour
{
    TalkTrigger talkTrigger;

    [SerializeField] private int objId;
    public int talkIndex;
    [SerializeField] private int startTalkIndex; // ��ȭ ������ �ε��� (���� ����)
    [SerializeField] private int lastTalkIndex; // ��ȭ ������ �ε��� (���� ����)
    [SerializeField] private int spawnItemTalkIndex;
    [SerializeField] private int spawnSecondItemTalkIndex;

    [SerializeField] private bool isShop;
    [SerializeField] private bool isVilleger;

    private GameObject shopPanel;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject secondItemPrefab;
    public int ObjId => objId;

    public Dictionary<int, int> talkIndexPair = new Dictionary<int, int>
    {
        { 10, 11 },
        { 11, 18 },
        { 17, 18 }
    };

    private void Awake()
    {
        talkTrigger = GetComponentInChildren<TalkTrigger>();
    }

    private void Start()
    {
        GameManager.Instance.Data.LoadTalkIndex(ref startTalkIndex, isShop, isVilleger, ref talkIndex);
    }

    // Ư�� ������ �Ϸ��ϸ� startTalkIndex�� lastTalkIndex�� ���� �����Ͽ� ����� ��ȭ�� ����
    public void Talk(Player player)
    {
        if (talkIndex >= lastTalkIndex && GameManager.Instance.UI.isTalkOpen)
        {
            talkIndex = lastTalkIndex - 1;
            startTalkIndex = talkIndex;
            GameManager.Instance.UI.isTalkOpen = false;
            GameManager.Instance.UI.PanelOpen("Game");
            if (isShop)
            {
                // �����̸� ǰ���г� ����
                GameObject obj = GameManager.Instance.UI.shopPanelTf.gameObject;
                obj.SetActive(true);
                obj.GetComponentInChildren<ShopItemList>().player = player;
                GameManager.Instance.UI.isShopOpen = true;
            }
            GameManager.Instance.Data.SaveTalkIndex(startTalkIndex, isShop, isVilleger);
            talkTrigger.interactionKey.SetActive(true);
            return;
        }

        if (isVilleger && talkIndex == spawnItemTalkIndex)
        {
            GameObject item = Instantiate(itemPrefab);
            item.transform.position = transform.position + (Vector3)((Vector2.right * 2f) + Vector2.up);
        }

        if (isVilleger && talkIndex == spawnSecondItemTalkIndex)
        {
            GameObject item = Instantiate(secondItemPrefab);
            item.transform.position = player.transform.position;
        }

        if (startTalkIndex != 0 && !isShop)
        {
            if (talkIndexPair[startTalkIndex] != lastTalkIndex)
            {
                lastTalkIndex = talkIndexPair[startTalkIndex];
                talkIndex = startTalkIndex;
            }
        }

        string talkData = GameManager.Instance.Talk.GetTalk(objId, talkIndex);
        talkIndex++;

        if (false == GameManager.Instance.UI.isTalkOpen)
        {
            GameManager.Instance.UI.isTalkOpen = true;
            GameManager.Instance.UI.PanelOpen("Talk");
        }

        GameManager.Instance.UI.talkText.text = talkData;
    }
}