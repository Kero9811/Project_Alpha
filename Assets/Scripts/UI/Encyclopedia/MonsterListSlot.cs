using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MonsterListSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;

    public MonsterData monsterData;

    private string monsterName;
    private Sprite monsterSprite;
    private int monsterId;
    private string desc;

    public void SetMonsterData(MonsterData monsterData)
    {
        if (monsterData != null)
        {
            monsterName = monsterData.MonsterName;
            monsterId = monsterData.MonsterId;
            monsterSprite = monsterData.monsterSprite;
            desc = monsterData.Desc;
        }
        else
        {
            monsterName = null;
            monsterSprite = null;
            monsterId = 0;
            desc = null;
            image.color = new Color(1, 1, 1, 0);
            image.sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponentInParent<EncyclopediaPanel>().ConfirmMonsterInfo(monsterData);
    }
}