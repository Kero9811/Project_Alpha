using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeUp ItemData", menuName = "Create/ShopItemData/MeleeUp ItemData")]
public class ShopMeleeItemData : ShopItemData
{
    public int value;

    public override void Use(Player player)
    {
        player.P_Attack.MeleeDmgUp(value);
    }
}