using System.Collections;
using UnityEngine;

public class StickyFlyborading : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")  // ����ҽӴ�̤�壬��Ϊ̤�������
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // ����Э�̣��ӳ�һ֡�ٽ���������ϵ��ȷ���ڸ�����״̬�ȶ�ʱ����
            StartCoroutine(ResetParentAfterDelay(collision.gameObject));
        }
    }

    // Э���ӳ�һִ֡�н�����ӹ�ϵ����
    IEnumerator ResetParentAfterDelay(GameObject player)
    {
        yield return null;  // �ȴ�һ֡
        player.transform.SetParent(null);  // ����������ϵ
    }
}
