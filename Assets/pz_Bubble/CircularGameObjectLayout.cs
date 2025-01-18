using UnityEngine;
using System.Collections.Generic;

namespace CustomUILayout
{
    /// <summary>
    /// 圆形排列的运行时脚本
    /// </summary>
    public class CircularGameObjectLayout : MonoBehaviour
    {
        // 半径
        [SerializeField] private float radius = 5f;
        // 子物体之间的角度差
        [SerializeField] private float angleDelta = 30f;
        // 起始方向 0-Right 1-Up 2-Left 3-Down
        [SerializeField] private int startDirection = 0;
        // 是否控制子物体大小
        [SerializeField] private bool controlChildSize = false;
        // 子物体大小 (仅在 controlChildSize 为 true 时生效)
        [SerializeField] private Vector3 childSize = Vector3.one;

        // 刷新布局方法（供运行时或编辑器调用）
        public void RefreshLayout()
        {
            // 获取所有子物体
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.gameObject.activeSelf)
                {
                    children.Add(child);
                }
            }

            // 计算总角度
            float totalAngle = (children.Count - 1) * angleDelta;
            // 中心分布的角度偏移量
            float halfAngle = totalAngle * 0.5f;

            // 遍历子物体并设置环形位置
            for (int i = 0; i < children.Count; i++)
            {
                Transform child = children[i];
                float angle = angleDelta * (startDirection < 2 ? children.Count - 1 - i : i) - halfAngle + startDirection * 90f;

                // 计算圆周上的位置
                float x = radius * Mathf.Cos(angle * Mathf.PI / 180f);
                float y = radius * Mathf.Sin(angle * Mathf.PI / 180f);

                // 更新子物体的位置
                child.localPosition = new Vector3(x, y, child.localPosition.z);

                // 如果启用了子物体大小控制，则更新大小
                if (controlChildSize)
                {
                    child.localScale = childSize;
                }
            }
        }
    }
}