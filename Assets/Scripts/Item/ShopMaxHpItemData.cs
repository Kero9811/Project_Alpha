using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Max Hp Item", menuName = "Create/ShopItemData/MaxHpItem")]
public class ShopMaxHpItemData : ShopItemData
{
    public int value;

    public override void Use(Player player)
    {
        player.GetMaxHp(value);
    }
}