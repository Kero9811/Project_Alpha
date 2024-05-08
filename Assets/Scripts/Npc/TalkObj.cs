using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkObj : MonoBehaviour
{
    [SerializeField] private int objId;
    private int talkIndex;
    [SerializeField] private int startTalkIndex; // ��ȭ ������ �ε��� (���� ����)
    [SerializeField] private int lastTalkIndex; // ��ȭ ������ �ε��� (���� ����)

    [SerializeField] private bool isShop;
    public int ObjId => objId;

    // Ư�� ������ �Ϸ��ϸ� startTalkIndex�� lastTalkIndex�� ���� �����Ͽ� ����� ��ȭ�� ����
    public void Talk()
    {
        if (talkIndex >= lastTalkIndex && GameManager.Instance.UI.isTalkOpen)
        {
            talkIndex = startTalkIndex - 1;
            GameManager.Instance.UI.isTalkOpen = false;
            GameManager.Instance.UI.PanelOpen("Game");
            if (isShop)
            {
                // �����̸� ǰ���г� ����
            }
            return;
        }

        string talkData = GameManager.Instance.Dialogue.GetTalk(objId, talkIndex);
        talkIndex++;

        if (false == GameManager.Instance.UI.isTalkOpen)
        {
            GameManager.Instance.UI.isTalkOpen = true;
            GameManager.Instance.UI.PanelOpen("Talk");
        }

        GameManager.Instance.UI.talkText.text = talkData;
    }
}