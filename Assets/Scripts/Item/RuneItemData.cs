using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Create/Rune", order = 2)]
public class RuneItemData : ItemData
{
    [SerializeField] private int cost; // ���� �ʿ� �ڽ�Ʈ
    public bool isEquipped; // ���� ����

    public int Cost => cost;
}