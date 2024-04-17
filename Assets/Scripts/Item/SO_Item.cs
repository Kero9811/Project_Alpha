using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create/Item", order = 1)]
public class SO_Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int cost;
    [SerializeField] private int id; // 능력 해금 및 능력치 증가 아이템 구분을 위한 id (인벤 구분 지표로도 가능)
                                     // (0 ~ 100 : 스토리 아이템, 101 ~ 200 : 능력 해금 아이템, 201 ~ 300 : 능력치 증가
    [SerializeField] private string desc;

    public string ItemName => itemName;
    public int Cost => cost;
    public int Id => id;
    public string Desc => desc;
}