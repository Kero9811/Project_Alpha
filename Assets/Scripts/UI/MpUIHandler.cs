using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpUIHandler : MonoBehaviour
{
    public Player player;
    private Image mpImage;

    private void Awake()
    {
        mpImage = GetComponent<Image>();
    }

    private void Start()
    {
        OnChangeMp();
    }

    private void OnEnable()
    {
        OnChangeMp();
    }

    public void OnChangeMp()
    {
        if (player == null) { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }
        mpImage.fillAmount = (float)player.CurMp / player.MaxMp;
    }
}