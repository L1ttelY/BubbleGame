using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adsorb : MonoBehaviour
{
    public float attractionForce = 5f; // 吸附力大小
    public int maxPoints = 2;         // pointList 最大容量

    // 定义轴向枚举
    public enum Axis
    {
        x = 0,
        y = 1,
    }
    public Axis selectedAxis = Axis.x; // 选择吸附的轴向，默认为 x 轴

    public List<GameObject> pointList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 判断碰撞物是否为 "point" 标签对象，同时确保列表不超过最大容量
        if (collision.CompareTag("point"))
        {
            if (!pointList.Contains(collision.gameObject) && pointList.Count < maxPoints)
            {
                pointList.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 判断是否为 "point" 标签对象
        if (collision.CompareTag("point"))
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

                        // 根据选定的轴向调整吸附方向
                        if (selectedAxis == Axis.x)
                        {
                            // 仅在 x 轴方向上施加吸附力
                            direction = new Vector2(direction.x, 0);
                        }
                        else if (selectedAxis == Axis.y)
                        {
                            // 仅在 y 轴方向上施加吸附力
                            direction = new Vector2(0, direction.y);
                        }

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
        if (collision.CompareTag("point") && pointList.Contains(collision.gameObject))
        {
            pointList.Remove(collision.gameObject);
        }
    }
}