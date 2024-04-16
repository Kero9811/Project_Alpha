using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpUIHandler : MonoBehaviour
{
    // ���� ���̺� �� �����Ϳ��� �ҷ������� �ؼ� ����
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