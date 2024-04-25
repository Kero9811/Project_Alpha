using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Create/Rune", order = 2)]
public class RuneItemData : ItemData
{
    [SerializeField] private int cost; // 장착 필요 코스트
    public bool isEquipped; // 장착 여부

    public int Cost => cost;
}