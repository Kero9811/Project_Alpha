using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpUIHandler : MonoBehaviour
{
    // 추후 세이브 된 데이터에서 불러오든지 해서 수정
    public Player player;

    public Image mpImage;

    private void Start()
    {
        OnChangeMp();
    }

    public void OnChangeMp()
    {
        mpImage.fillAmount = (float)player.CurMp / player.MaxMp;
    }
}