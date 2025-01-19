using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class smallBubbleXifu : MonoBehaviour
{
    public float attractionForce = 5f; // 吸附力大小
    public int maxPoints = 2;          // pointList 最大容量

    public UnityEvent unityEvent;
    public List<GameObject> pointList = new List<GameObject>();

    // 定义延迟时间
    public float eventDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("smallBubble"))
        {
            if (!pointList.Contains(collision.gameObject) && pointList.Count < maxPoints)
            {
                pointList.Add(collision.gameObject);

                // 开启一个延迟调用协程
                StartCoroutine(InvokeUnityEventWithDelay());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 判断是否为 "smallBubble" 标签对象
        if (collision.CompareTag("smallBubble"))
        {
            // 持续施加吸附力
            foreach (GameObject point in pointList)
            {
                if (point != null)
                {
                    Rigidbody2D rb = point.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        // 计算吸附力的方向，指向当前物体的中心
                        Vector2 direction = (transform.position - point.transform.position).normalized;
                        // 对刚体施加力
                        rb.AddForce(direction * attractionForce);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 如果碰撞物离开当前碰撞器，则从列表中移除它
        if (collision.CompareTag("smallBubble") && pointList.Contains(collision.gameObject))
        {
            pointList.Remove(collision.gameObject);
        }
    }

    // 定义一个协程，用于延迟调用 UnityEvent
    private IEnumerator InvokeUnityEventWithDelay()
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(eventDelay);

        // 调用 UnityEvent
        unityEvent?.Invoke();
    }
}