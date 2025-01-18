using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class SoftBody : MonoBehaviour
{
    public SpriteShapeController spriteShape;
    public Transform[] points; // 所有的控制点
    public const float splineOffset = 0.5f;
    public GameObject xiaopaopao; // 小泡泡预制体
    public Quaternion spawnRotation = Quaternion.identity; // 初始生成的旋转角度

    public float bubbleForce = 5f; // 小泡泡抛出的初始力度
    private Collider2D softBodyCollider; // SoftBody 的碰撞体

    private void Awake()
    {
        softBodyCollider = GetComponent<Collider2D>(); // 获取当前物体的碰撞体
        if (softBodyCollider == null)
        {
            Debug.LogWarning("SoftBody 物体未附加 Collider2D 组件！");
        }
        UpdateVerticies();
    }

    private void Update()
    {
        // 每帧更新形状
        UpdateVerticies();

        // 按下 B 键时创建一个小泡泡
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateSmallBubble();
        }
    }

    public void CreateSmallBubble()
    {
        // 获取鼠标的世界位置
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 确保 z 值为 0

        // 检测鼠标是否在 SoftBody 的碰撞体内
        if (softBodyCollider != null && softBodyCollider.OverlapPoint(mousePosition))
        {
            Debug.Log("鼠标在 SoftBody 的碰撞体范围内，无法生成小泡泡！");
            return; // 如果鼠标在碰撞体范围内，则停止生成小泡泡
        }

        // 找到离鼠标最近的点
        Transform closestPoint = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform point in points)
        {
            float distance = Vector2.Distance(mousePosition, point.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        if (closestPoint == null)
        {
            Debug.LogWarning("没有找到最近的点！");
            return;
        }

        // 在最近点的位置生成小泡泡
        Vector3 spawnOffset = closestPoint.position; // 出生位置为最近点的位置
        GameObject bubble = Instantiate(xiaopaopao, spawnOffset, spawnRotation);

        // 计算方向：从泡泡位置指向鼠标位置
        Vector2 direction = (mousePosition - spawnOffset).normalized;

        // 给泡泡施加力
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * bubbleForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("小泡泡预制体未附加 Rigidbody2D 组件！");
        }

        // 启动小泡泡的渐变效果
        StartCoroutine(BubbleLifeCycle(bubble));
    }

    private IEnumerator BubbleLifeCycle(GameObject bubble)
    {
        float lifeTime = 2f; // 小泡泡存活时间
        float expandSpeed = 1f; // 扩大的速度倍率
        SpriteShapeRenderer spriteRenderer = bubble.GetComponent<SpriteShapeRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("小泡泡未附加 SpriteShapeRenderer 组件！");
            yield break;
        }

        // 循环控制透明度和缩放
        float elapsedTime = 0f;
        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;

            // 逐渐扩大
            float scale = Mathf.Lerp(0.26f, 0.5f, elapsedTime / lifeTime);
            bubble.transform.localScale = new Vector3(scale, scale, 1f);

            // 逐渐虚化
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / lifeTime);
            spriteRenderer.color = color;

            yield return null; // 等待下一帧
        }

        // 泡泡破裂，销毁
        Destroy(bubble);
    }

    public void UpdateVerticies()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 _vertex = points[i].localPosition;
            Vector2 _towardsCenter = (Vector2.zero - _vertex).normalized;
            float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;

            try
            {
                spriteShape.spline.SetPosition(i, (_vertex));
            }
            catch
            {
                Debug.Log("<color=red>距离过长</color>");
                spriteShape.spline.SetPosition(i, (_vertex - _towardsCenter * (_colliderRadius + splineOffset)));
            }

            Vector2 LT = spriteShape.spline.GetLeftTangent(i);
            Vector2 newRT = Vector2.Perpendicular(_towardsCenter) * LT.magnitude;
            Vector2 newLT = Vector2.zero - (newRT);

            spriteShape.spline.SetRightTangent(i, newRT);
            spriteShape.spline.SetLeftTangent(i, newLT);
        }
    }
}