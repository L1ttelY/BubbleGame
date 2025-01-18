using UnityEngine;
using System.Collections.Generic;

namespace CustomUILayout
{
    /// <summary>
    /// Բ�����е�����ʱ�ű�
    /// </summary>
    public class CircularGameObjectLayout : MonoBehaviour
    {
        // �뾶
        [SerializeField] private float radius = 5f;
        // ������֮��ĽǶȲ�
        [SerializeField] private float angleDelta = 30f;
        // ��ʼ���� 0-Right 1-Up 2-Left 3-Down
        [SerializeField] private int startDirection = 0;
        // �Ƿ�����������С
        [SerializeField] private bool controlChildSize = false;
        // �������С (���� controlChildSize Ϊ true ʱ��Ч)
        [SerializeField] private Vector3 childSize = Vector3.one;

        // ˢ�²��ַ�����������ʱ��༭�����ã�
        public void RefreshLayout()
        {
            // ��ȡ����������
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.gameObject.activeSelf)
                {
                    children.Add(child);
                }
            }

            // �����ܽǶ�
            float totalAngle = (children.Count - 1) * angleDelta;
            // ���ķֲ��ĽǶ�ƫ����
            float halfAngle = totalAngle * 0.5f;

            // ���������岢���û���λ��
            for (int i = 0; i < children.Count; i++)
            {
                Transform child = children[i];
                float angle = angleDelta * (startDirection < 2 ? children.Count - 1 - i : i) - halfAngle + startDirection * 90f;

                // ����Բ���ϵ�λ��
                float x = radius * Mathf.Cos(angle * Mathf.PI / 180f);
                float y = radius * Mathf.Sin(angle * Mathf.PI / 180f);

                // �����������λ��
                child.localPosition = new Vector3(x, y, child.localPosition.z);

                // ����������������С���ƣ�����´�С
                if (controlChildSize)
                {
                    child.localScale = childSize;
                }
            }
        }
    }
}