using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestToDungeon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GameManager.Instance.Scene.StartSceneLoad("DungeonScene");
        }
    }
}
