using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpUIHandler : MonoBehaviour
{
    // ���� ���̺� �� �����Ϳ��� �ҷ������� �ؼ� ����
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

    // �ִ� ������ �ø��鼭 �̹����� ������ ���̶�� �߰� �� ����
    public void OnChangeMaxMp()
    {

    }
}