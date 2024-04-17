using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create/Item", order = 1)]
public class SO_Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int cost;
    [SerializeField] private int id; // �ɷ� �ر� �� �ɷ�ġ ���� ������ ������ ���� id (�κ� ���� ��ǥ�ε� ����)
                                     // (0 ~ 100 : ���丮 ������, 101 ~ 200 : �ɷ� �ر� ������, 201 ~ 300 : �ɷ�ġ ����
    [SerializeField] private string desc;

    public string ItemName => itemName;
    public int Cost => cost;
    public int Id => id;
    public string Desc => desc;
}