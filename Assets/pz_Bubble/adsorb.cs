using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adsorb : MonoBehaviour
{
    public float attractionForce = 5f; // 吸附力大小
    private int maxPoints = 2;         // pointList 最大容量

    public List<GameObject> pointList = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 判断碰撞物是否为 "point" 标签对象，同时确保列表不超过最大容量
        if (collision.tag == "point")
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
        if (collision.tag == "point")
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
                        rb.AddForce(direction * attractionForce);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 如果碰撞物离开当前碰撞器，则从列表中移除它
        if (collision.tag == "point" && pointList.Contains(collision.gameObject))
        {
            pointList.Remove(collision.gameObject);
        }
    }
   
}