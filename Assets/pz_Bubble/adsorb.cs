using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adsorb : MonoBehaviour
{
    public float attractionForce = 5f; // ��������С
    public int maxPoints = 2;         // pointList �������

    // ��������ö��
    public enum Axis
    {
        x = 0,
        y = 1,
    }
    public Axis selectedAxis = Axis.x; // ѡ������������Ĭ��Ϊ x ��

    public List<GameObject> pointList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ж���ײ���Ƿ�Ϊ "point" ��ǩ����ͬʱȷ���б������������
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
        // �ж��Ƿ�Ϊ "point" ��ǩ����
        if (collision.CompareTag("point"))
        {
            // ����ʩ��������
            foreach (GameObject point in pointList)
            {
                if (point != null)
                {
                    Rigidbody2D rb = point.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        // �����������ķ���ָ��ǰ���������
                        Vector2 direction = (transform.position - point.transform.position).normalized;

                        // ����ѡ�������������������
                        if (selectedAxis == Axis.x)
                        {
                            // ���� x �᷽����ʩ��������
                            direction = new Vector2(direction.x, 0);
                        }
                        else if (selectedAxis == Axis.y)
                        {
                            // ���� y �᷽����ʩ��������
                            direction = new Vector2(0, direction.y);
                        }

                        // �Ը���ʩ����
                        rb.AddForce(direction * attractionForce);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �����ײ���뿪��ǰ��ײ��������б����Ƴ���
        if (collision.CompareTag("point") && pointList.Contains(collision.gameObject))
        {
            pointList.Remove(collision.gameObject);
        }
    }
}