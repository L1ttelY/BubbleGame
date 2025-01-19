using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield:MonoBehaviour {
	public GameObject circle; // 用于生成的圆环对象
	public Quaternion spawnRotation = Quaternion.identity; // 初始生成的旋转角度

	private void OnTriggerEnter2D(Collider2D collision) {
		Vector3 spawnOffset = transform.position; // 获取当前对象的位置

		if(collision.CompareTag("Points")) // 使用 CompareTag 代替直接比较字符串（更高效）
		{
			// 获取 SoftBody 脚本引用
			BubbleController bubble = collision.GetComponent<BubbleController>();
			if(bubble!=null&&!bubble.propertyToughness) {
				Destroy(bubble.gameObject);
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision) {
		Vector3 spawnOffset = transform.position; // 获取当前对象的位置
		BubbleController bubble = collision.collider.GetComponentInParent<BubbleController>();

		if(bubble&&bubble.gameObject.CompareTag("Points")) // 使用 CompareTag 代替直接比较字符串（更高效）
		{
			// 获取 SoftBody 脚本引用
			if(bubble!=null&&!bubble.propertyToughness) {
				Destroy(bubble.gameObject);
			}
		}
	}


	// 定义协程来处理延迟销毁逻辑
	private IEnumerator DestroyAfterDelay(Collider2D collision,float delay) {
		yield return new WaitForSeconds(delay); // 等待动画播放完成
		if(collision!=null) // 检查目标对象是否依然存在
		{
			Destroy(collision.gameObject); // 销毁目标
		}
	}
}