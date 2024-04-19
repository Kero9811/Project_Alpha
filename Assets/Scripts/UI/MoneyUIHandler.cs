using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUIHandler : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public Image moneyImage;

    public Player player;

    public void OnChangeMoney()
    {
        moneyText.text = player.CurGold.ToString();
    }
}