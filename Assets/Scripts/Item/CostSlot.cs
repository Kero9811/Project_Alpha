using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites; // [0] : On, [1] : Off

    public void ChangeToOn()
    {
        image.sprite = sprites[0];
    }

    public void ChangeToOff()
    {
        image.sprite = sprites[1];
    }
}