using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagicUp ItemData", menuName = "Create/ShopItemData/MagicUp ItemData")]
public class ShopMagicItemData : ShopItemData
{
    public int value;

    public override void Use(Player player)
    {
        player.P_Attack.MagicDmgUp(value);
    }
}