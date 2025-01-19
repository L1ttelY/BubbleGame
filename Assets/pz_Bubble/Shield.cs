using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield:MonoBehaviour {
	public GameObject circle; // �������ɵ�Բ������
	public Quaternion spawnRotation = Quaternion.identity; // ��ʼ���ɵ���ת�Ƕ�

	private void OnTriggerEnter2D(Collider2D collision) {
		Vector3 spawnOffset = transform.position; // ��ȡ��ǰ�����λ��

		if(collision.CompareTag("Points")) // ʹ�� CompareTag ����ֱ�ӱȽ��ַ���������Ч��
		{
			// ��ȡ SoftBody �ű�����
			BubbleController bubble = collision.GetComponent<BubbleController>();
			if(bubble!=null&&!bubble.propertyToughness) {
				Destroy(bubble.gameObject);
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision) {
		Vector3 spawnOffset = transform.position; // ��ȡ��ǰ�����λ��
		BubbleController bubble = collision.collider.GetComponentInParent<BubbleController>();

		if(bubble&&bubble.gameObject.CompareTag("Points")) // ʹ�� CompareTag ����ֱ�ӱȽ��ַ���������Ч��
		{
			// ��ȡ SoftBody �ű�����
			if(bubble!=null&&!bubble.propertyToughness) {
				Destroy(bubble.gameObject);
			}
		}
	}


	// ����Э���������ӳ������߼�
	private IEnumerator DestroyAfterDelay(Collider2D collision,float delay) {
		yield return new WaitForSeconds(delay); // �ȴ������������
		if(collision!=null) // ���Ŀ������Ƿ���Ȼ����
		{
			Destroy(collision.gameObject); // ����Ŀ��
		}
	}
}