using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Monster monster;

    private void Awake()
    {
        monster = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(monster.Damage, monster.transform);
        }
    }
}