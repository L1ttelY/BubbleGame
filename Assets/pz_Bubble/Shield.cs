using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject circle; // �������ɵ�Բ������
    public Quaternion spawnRotation = Quaternion.identity; // ��ʼ���ɵ���ת�Ƕ�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 spawnOffset = transform.position; // ��ȡ��ǰ�����λ��
        if (collision.CompareTag("Points")) // ʹ�� CompareTag ����ֱ�ӱȽ��ַ���������Ч��
        {
            // �����¶���
            Instantiate(circle, spawnOffset, spawnRotation);

            // �ӳ����ٶ���ʹ��Э�̴���
            StartCoroutine(DestroyAfterDelay(collision, 0.15f));
        }
    }

    // ����Э���������ӳ������߼�
    private IEnumerator DestroyAfterDelay(Collider2D collision, float delay)
    {
        yield return new WaitForSeconds(delay); // �ȴ�
        if (collision != null) // ���Ŀ������Ƿ���Ȼ����
        {
            Destroy(collision.gameObject); // ����Ŀ��
        }
    }
}