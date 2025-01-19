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
			SoftBody softBody = collision.GetComponent<SoftBody>();
			if(softBody!=null) {
				// ���Ŵ��ƶ���
				//softBody.PlayPierceAnimation();

				// �ӳ����ٴ��ƶ���
				Destroy(softBody.gameObject);
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