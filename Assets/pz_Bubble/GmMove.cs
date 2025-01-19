using UnityEngine;

public class GmMove : MonoBehaviour
{
    public Transform target;  // Ŀ���� Transform
    public float moveDuration = 2f; // �ӵ�ǰλ�õ�Ŀ�������ʱ�䣨�룩

    private Vector3 startPoint; // ��ʼʱ��λ��
    private float elapsedTime;  // �Ѿ�����ʱ��
    private bool isMoving = false;

    void Update()
    {
     
        // ��������ƶ������в�ֵ
        if (isMoving)
        {
            elapsedTime += Time.deltaTime; // �����ѹ�ʱ��

            float t = Mathf.Clamp01(elapsedTime / moveDuration); // ����ʱ���������Χ0��1��

            // ʹ�� Lerp ������� startPoint �ƶ��� target.position
            transform.position = Vector3.Lerp(startPoint, target.position, t);

            // �������Ŀ��㣬ֹͣ�ƶ�
            if (t >= 1)
            {
                isMoving = false; // ֹͣ�ƶ�
            }
        }
    }

    // ��ʼ�ƶ�
   public void StartMove()
    {
        startPoint = transform.position; // ��¼��ǰλ��
        elapsedTime = 0f;                // �����ѹ�ʱ��
        isMoving = true;                 // ��ʼ�ƶ�
    }
}