using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageToPlayer;  // 设置陷阱对玩家造成的伤害

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取 PlayerLife 脚本并调用 TakeDamage 方法
            PlayerLife player = other.GetComponent<PlayerLife>();

            if (player != null)  // 确保 player 不为空
            {
                player.TakeDamage(damageToPlayer);  // 造成伤害，扣除玩家的健康值
            }
        }
    }
}

