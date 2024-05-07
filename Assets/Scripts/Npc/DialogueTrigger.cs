using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerisIn;
    [SerializeField] private GameObject interactionKey;

    private void Awake()
    {
        interactionKey.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerisIn)
            {
                Debug.Log("Npc와 대화 시도");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerisIn = true;
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