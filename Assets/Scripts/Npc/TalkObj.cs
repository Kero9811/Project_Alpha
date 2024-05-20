using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkObj : MonoBehaviour
{
    TalkTrigger talkTrigger;

    [SerializeField] private int objId;
    private int talkIndex;
    [SerializeField] private int startTalkIndex; // ��ȭ ������ �ε��� (���� ����)
    [SerializeField] private int lastTalkIndex; // ��ȭ ������ �ε��� (���� ����)
    [SerializeField] private int spawnItemTalkIndex;

    [SerializeField] private bool isShop;
    [SerializeField] private bool isVilleger;
    public bool isTalked;

    private GameObject shopPanel;

    [SerializeField] private GameObject itemPrefab;
    public int ObjId => objId;

    private void Awake()
    {
        talkTrigger = GetComponentInChildren<TalkTrigger>();
    }

    private void Start()
    {
        GameManager.Instance.Data.LoadTalkIndex(startTalkIndex, isShop, isVilleger, isTalked);
        if (isTalked)
        {
            talkIndex = startTalkIndex - 1;
        }
    }

    // Ư�� ������ �Ϸ��ϸ� startTalkIndex�� lastTalkIndex�� ���� �����Ͽ� ����� ��ȭ�� ����
    public void Talk(Player player)
    {
        if (talkIndex >= lastTalkIndex && GameManager.Instance.UI.isTalkOpen)
        {
            talkIndex = startTalkIndex - 1;
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