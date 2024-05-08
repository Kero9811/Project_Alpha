using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    private bool playerisIn;
    [SerializeField] private GameObject interactionKey;
    TalkObj talkObj;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerisIn)
            {
                talkObj.Talk();
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