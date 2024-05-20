using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkObj : MonoBehaviour
{
    TalkTrigger talkTrigger;

    [SerializeField] private int objId;
    private int talkIndex;
    [SerializeField] private int startTalkIndex; // 대화 시작할 인덱스 (변경 가능)
    [SerializeField] private int lastTalkIndex; // 대화 종료할 인덱스 (변경 가능)
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

    // 특정 조건을 완료하면 startTalkIndex와 lastTalkIndex의 값을 변경하여 출력할 대화를 변경
    public void Talk(Player player)
    {
        if (talkIndex >= lastTalkIndex && GameManager.Instance.UI.isTalkOpen)
        {
            talkIndex = startTalkIndex - 1;
            GameManager.Instance.UI.isTalkOpen = false;
            GameManager.Instance.UI.PanelOpen("Game");
            if (isShop)
            {
                // 상인이면 품목패널 띄우기
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