using UnityEngine;

public class GmMove : MonoBehaviour
{
    public Transform target;  // 目标点的 Transform
    public float moveDuration = 2f; // 从当前位置到目标点所需时间（秒）

    private Vector3 startPoint; // 开始时的位置
    private float elapsedTime;  // 已经过的时间
    private bool isMoving = false;

    void Update()
    {
     
        // 如果正在移动，进行插值
        if (isMoving)
        {
            elapsedTime += Time.deltaTime; // 更新已过时间

            float t = Mathf.Clamp01(elapsedTime / moveDuration); // 计算时间比例（范围0到1）

            // 使用 Lerp 让物体从 startPoint 移动到 target.position
            transform.position = Vector3.Lerp(startPoint, target.position, t);

            // 如果到达目标点，停止移动
            if (t >= 1)
            {
                isMoving = false; // 停止移动
            }
        }
    }

    // 开始移动
   public void StartMove()
    {
        startPoint = transform.position; // 记录当前位置
        elapsedTime = 0f;                // 重置已过时间
        isMoving = true;                 // 开始移动
    }
}