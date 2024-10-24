using System.Collections;
using UnityEngine;

public class StickyFlyborading : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")  // 当玩家接触踏板，归为踏板的子类
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // 启动协程，延迟一帧再解除父物体关系，确保在父物体状态稳定时操作
            StartCoroutine(ResetParentAfterDelay(collision.gameObject));
        }
    }

    // 协程延迟一帧执行解除父子关系操作
    IEnumerator ResetParentAfterDelay(GameObject player)
    {
        yield return null;  // 等待一帧
        player.transform.SetParent(null);  // 解除父物体关系
    }
}
