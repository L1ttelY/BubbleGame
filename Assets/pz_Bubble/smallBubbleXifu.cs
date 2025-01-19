using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class smallBubbleXifu : MonoBehaviour
{
    public float attractionForce = 5f; // ��������С
    public int maxPoints = 2;          // pointList �������

    public UnityEvent unityEvent;
    public List<GameObject> pointList = new List<GameObject>();

    // �����ӳ�ʱ��
    public float eventDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("smallBubble"))
        {
            if (!pointList.Contains(collision.gameObject) && pointList.Count < maxPoints)
            {
                pointList.Add(collision.gameObject);

                // ����һ���ӳٵ���Э��
                StartCoroutine(InvokeUnityEventWithDelay());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // �ж��Ƿ�Ϊ "smallBubble" ��ǩ����
        if (collision.CompareTag("smallBubble"))
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
        if (collision.CompareTag("smallBubble") && pointList.Contains(collision.gameObject))
        {
            pointList.Remove(collision.gameObject);
        }
    }

    // ����һ��Э�̣������ӳٵ��� UnityEvent
    private IEnumerator InvokeUnityEventWithDelay()
    {
        // �ȴ�ָ�����ӳ�ʱ��
        yield return new WaitForSeconds(eventDelay);

        // ���� UnityEvent
        unityEvent?.Invoke();
    }
}