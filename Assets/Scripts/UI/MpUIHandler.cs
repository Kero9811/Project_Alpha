using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpUIHandler : MonoBehaviour
{
    // 추후 세이브 된 데이터에서 불러오든지 해서 수정
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

    public void OnChangeMp()
    {
        mpImage.fillAmount = (float)player.CurMp / player.MaxMp;
    }

    // 최대 마나를 늘리면서 이미지를 변경할 것이라면 추가 및 수정
    public void OnChangeMaxMp()
    {

    }
}