using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    private bool playerisIn;
    public GameObject interactionKey;
    TalkObj talkObj;
    Player player;

    private void Awake()
    {
        interactionKey.SetActive(false);
    }

    private void Start()
    {
        talkObj = GetComponentInParent<TalkObj>();
    }

    private void Update()
    {
        if (GameManager.Instance.UI.isShopOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.UI.shopPanelTf.gameObject.SetActive(false);
                GameManager.Instance.UI.isShopOpen = false;
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerisIn)
            {
                interactionKey.SetActive(false);
                talkObj.Talk(player);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerisIn = true;
            player = collision.GetComponent<Player>();
            interactionKey.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerisIn = false;
            interactionKey.SetActive(false);
        }
    }
}