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

    private void Start()
    {
        OnChangeMoney();
    }

    private void OnEnable()
    {
        OnChangeMoney();
    }

    public void OnChangeMoney()
    {
        if (player == null) { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }
        moneyText.text = player.CurGold.ToString();
    }
}