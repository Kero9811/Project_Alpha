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

    [SerializeField] private bool isShop;
    public int ObjId => objId;

    private void Awake()
    {
        talkTrigger = GetComponentInChildren<TalkTrigger>();
    }

    // 특정 조건을 완료하면 startTalkIndex와 lastTalkIndex의 값을 변경하여 출력할 대화를 변경
    public void Talk()
    {
        if (talkIndex >= lastTalkIndex && GameManager.Instance.UI.isTalkOpen)
        {
            talkIndex = startTalkIndex - 1;
            GameManager.Instance.UI.isTalkOpen = false;
            GameManager.Instance.UI.PanelOpen("Game");
            if (isShop)
            {
                // 상인이면 품목패널 띄우기
            }
            talkTrigger.interactionKey.SetActive(true);
            return;
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