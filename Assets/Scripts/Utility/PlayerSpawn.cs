using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject playerPrefab;

    private void Awake()
    {
        GameObject player = Instantiate(playerPrefab);

        SetSpawnPos(player);
    }

    private void SetSpawnPos(GameObject player)
    {
        if (GameManager.Instance.Scene.isDead)
        {
            player.transform.position = spawnPoints[1].position;
            GameManager.Instance.Scene.isDead = false;
            return;
        }

        if(GameManager.Instance.Scene.prevScene == "TitleScene")
        {
            player.transform.position = spawnPoints[0].position;
        }
        else if(GameManager.Instance.Scene.prevScene == "ForestScene" ||
                GameManager.Instance.Scene.prevScene == "DungeonScene")
        {
            if (GameManager.Instance.Scene.nextScene == "ForestScene" 
                && GameManager.Instance.Scene.prevScene == "TitleScene")
            {
                player.transform.position = spawnPoints[0].position;
                return;
            }

            if (GameManager.Instance.Scene.nextScene == "DungeonScene")
            {
                player.transform.position = spawnPoints[0].position;
                return;
            }

            if (GameManager.Instance.Scene.nextScene == "ForestScene")
            {
                player.transform.position = spawnPoints[1].position;
                return;
            }

            player.transform.position = spawnPoints[2].position;
        }
    }
}
