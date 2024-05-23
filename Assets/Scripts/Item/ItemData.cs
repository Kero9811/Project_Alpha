using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create/Item", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int id;
    [SerializeField] private Sprite image;
    [SerializeField] private string desc;

    public string ItemName => itemName;
    public int Id => id;
    public Sprite Image => image;
    public string Desc => desc;
}