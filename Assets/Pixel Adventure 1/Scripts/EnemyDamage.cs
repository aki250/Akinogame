using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageToPlayer;  // ��������������ɵ��˺�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ��ȡ PlayerLife �ű������� TakeDamage ����
            PlayerLife player = other.GetComponent<PlayerLife>();

            if (player != null)  // ȷ�� player ��Ϊ��
            {
                player.TakeDamage(damageToPlayer);  // ����˺����۳���ҵĽ���ֵ
            }
        }
    }
}

