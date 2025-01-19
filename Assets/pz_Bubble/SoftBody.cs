using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class SoftBody:MonoBehaviour {
	public SpriteShapeController spriteShape;
	public Transform[] points; // ���еĿ��Ƶ�
	public const float splineOffset = 0.5f;
	public GameObject xiaopaopao; // С����Ԥ����
	public Quaternion spawnRotation = Quaternion.identity; // ��ʼ���ɵ���ת�Ƕ�

	public GameObject animationObject; // ���ڲ��Ŷ����Ķ���
	public Animator animator; // ����������
	private bool isAnimationPlaying = false; // �Ƿ����ڲ��Ŷ���

	public float bubbleForce = 5f; // С�����׳��ĳ�ʼ����
	private Collider2D softBodyCollider; // SoftBody ����ײ��


	private void Awake() {
		softBodyCollider=GetComponent<Collider2D>(); // ��ȡ��ǰ�������ײ��
		if(softBodyCollider==null) {
			Debug.LogWarning("SoftBody ����δ���� Collider2D �����");
		}
		UpdateVerticies();
	}

	private void Update() {
		// ÿ֡������״
		UpdateVerticies();

		// ���� B ��ʱ��ʼ���Ŷ���������С����
		if(Input.GetKeyDown(KeyCode.B)) {
			if(!isAnimationPlaying) {
				StartCoroutine(PlayAnimation()); // ����������
				CreateSmallBubble();
			}
		}
	}

	private IEnumerator PlayAnimation() {
		isAnimationPlaying=true; // ��Ƕ������ڲ���
		animationObject.SetActive(true); // ��ʾ��������

		// ȷ��������������Ϊ�ղ���������
		if(animator!=null) {
			animator.SetTrigger("Play");
		} else {
			Debug.LogWarning("����������δ���ӣ�");
		}

		// �ȴ������������
		yield return new WaitForSeconds(GetAnimationLength());

		animationObject.SetActive(false); // ���ض�������
		isAnimationPlaying=false; // ��Ƕ���ֹͣ����
	}

	private float GetAnimationLength() {
		if(animator!=null&&animator.runtimeAnimatorController!=null) {
			AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
			if(clips.Length>0) {
				return clips[0].length; // ���趯��������ֻ��1������Ƭ��
			}
		}
		return 1f; // ����޷���ȡ�������ȣ�Ĭ�Ϸ���1��
	}

	public void CreateSmallBubble() {
		// ��ȡ��������λ��
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z=0; // ȷ�� z ֵΪ 0

		// �������Ƿ��� SoftBody ����ײ����
		if(softBodyCollider!=null&&softBodyCollider.OverlapPoint(mousePosition)) {
			Debug.Log("����� SoftBody ����ײ�巶Χ�ڣ��޷�����С���ݣ�");
			return; // ����������ײ�巶Χ�ڣ���ֹͣ����С����
		}

		// �ҵ����������ĵ�
		Transform closestPoint = null;
		float minDistance = Mathf.Infinity;

		foreach(Transform point in points) {
			float distance = Vector2.Distance(mousePosition,point.position);
			if(distance<minDistance) {
				minDistance=distance;
				closestPoint=point;
			}
		}

		if(closestPoint==null) {
			Debug.LogWarning("û���ҵ�����ĵ㣡");
			return;
		}

		// ��������λ������С����
		Vector3 spawnOffset = closestPoint.position; // ����λ��Ϊ������λ��
		GameObject bubble = Instantiate(xiaopaopao,spawnOffset,spawnRotation);

		// ���㷽�򣺴�����λ��ָ�����λ��
		Vector2 direction = (mousePosition-spawnOffset).normalized;

		// ������ʩ����
		Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
		if(rb!=null) {
			rb.AddForce(direction*bubbleForce,ForceMode2D.Impulse);
		} else {
			Debug.LogWarning("С����Ԥ����δ���� Rigidbody2D �����");
		}

		// ����С���ݵĽ���Ч��
		StartCoroutine(BubbleLifeCycle(bubble));
	}
	public float lifeTime = 2f; // С���ݴ��ʱ��
	public float expandSpeed = 1f; // ������ٶȱ���
	private IEnumerator BubbleLifeCycle(GameObject bubble) {
		SpriteShapeRenderer spriteRenderer = bubble.GetComponent<SpriteShapeRenderer>();

		if(spriteRenderer==null) {
			Debug.LogWarning("С����δ���� SpriteShapeRenderer �����");
			yield break;
		}

		// ѭ������͸���Ⱥ�����
		float elapsedTime = 0f;
		while(elapsedTime<lifeTime) {
			elapsedTime+=Time.deltaTime;

			// ������
			float scale = Mathf.Lerp(0.26f,0.5f,elapsedTime/lifeTime);
			bubble.transform.localScale=new Vector3(scale,scale,1f);

			// ���黯
			Color color = spriteRenderer.color;
			color.a=Mathf.Lerp(1f,0f,elapsedTime/lifeTime);
			spriteRenderer.color=color;

			yield return null; // �ȴ���һ֡
		}

		// �������ѣ�����
		Destroy(bubble);
	}

	public void UpdateVerticies() {
		for(int i = 0;i<points.Length-1;i++) {
			Vector2 _vertex = points[i].localPosition;
			Vector2 _towardsCenter = (Vector2.zero-_vertex).normalized;
			float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;

			try {
				spriteShape.spline.SetPosition(i,(_vertex));
			} catch {
				Debug.Log("<color=red>�������</color>");
				spriteShape.spline.SetPosition(i,(_vertex-_towardsCenter*(_colliderRadius+splineOffset)));
			}

			Vector2 LT = spriteShape.spline.GetLeftTangent(i);
			Vector2 newRT = Vector2.Perpendicular(_towardsCenter)*LT.magnitude;
			Vector2 newLT = Vector2.zero-(newRT);

			spriteShape.spline.SetRightTangent(i,newRT);
			spriteShape.spline.SetLeftTangent(i,newLT);
		}
	}
	public Animator pierceAnimator; // ���ƶ����Ŀ�����
	public GameObject pierceAnimationObject; // ���Ŵ��ƶ����Ķ���
	private bool isPierceAnimationPlaying = false; // �Ƿ����ڲ��Ŵ��ƶ���

	private void OnDestroy() {
		pierceAnimationObject.SetActive(true); // ���������
		pierceAnimationObject.transform.parent=null;

		// ȷ����������������
		if(pierceAnimator!=null) {
			pierceAnimator.SetTrigger("Play"); // �������ƶ���
		} else {
			Debug.LogWarning("δ���Ӵ��ƶ�����������");
		}

	}


	private float GetPierceAnimationLength() {
		// ��ȡ���ƶ����ĳ��ȣ������ȡʧ�ܣ��򷵻�Ĭ��ֵ 0.3 ��
		if(pierceAnimator!=null&&pierceAnimator.runtimeAnimatorController!=null) {
			AnimationClip[] clips = pierceAnimator.runtimeAnimatorController.animationClips;
			if(clips.Length>0) {
				return clips[0].length; // ������ƶ���������ֻ��һ������Ƭ��
			}
		}
		return 0.3f;
	}
}
