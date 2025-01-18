using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleNodeController:MonoBehaviour {

	List<Transform> points;
	int thisIndex;
	[SerializeField] float pressureFactor;

	Rigidbody2D rigidBody;

	void Start() {
		rigidBody=GetComponent<Rigidbody2D>();
		points=new List<Transform>(GetComponentInParent<SoftBody>().points);
		points.Remove(points[points.Count-1]);
		foreach(var i in points) Debug.Log(i);
		for(int i = 0;i<points.Count;i++) if(points[i]==transform) thisIndex=i;
	}

	void FixedUpdate() {
		Vector2 forceTotal = Vector2.zero;
		foreach(var testFlowingArea in FlowingArea.instances) {
			if(!testFlowingArea.collider.OverlapPoint(transform.position)) continue;
			RaycastHit2D hit = Physics2D.Raycast(
				testFlowingArea.transform.position,
				transform.position-testFlowingArea.transform.position,
				(transform.position-testFlowingArea.transform.position).magnitude,
				LayerMask.GetMask("Collider","ColliderSticky","Spike","Liquid"),
				int.MinValue
			);
			if(hit.collider) {
				continue;
			}
			forceTotal+=GetForce(testFlowingArea.velocity);

		}
		if(forceTotal==Vector2.zero) forceTotal+=GetForce(Vector2.zero);
		rigidBody.AddForce(forceTotal);
	}

	Vector2 GetForce(Vector2 localAirVelocity) {
		Vector2 relativeVelocity = localAirVelocity-rigidBody.velocity;

		Vector2 previousPosition = points[(thisIndex+points.Count-1)%points.Count].position;
		Vector2 nextPosition = points[(thisIndex+points.Count+1)%points.Count].position;
		Vector2 thisPosition = transform.position;

		Vector2 normal = Utils.Prudoct((nextPosition-previousPosition).normalized,Vector2.up);

		previousPosition=(previousPosition+thisPosition)/2f;
		nextPosition=(nextPosition+thisPosition)/2f;

		float factor1 = Utils.CrossAbs((previousPosition-thisPosition).normalized,relativeVelocity.normalized);
		float factor2 = Utils.CrossAbs((nextPosition-thisPosition).normalized,relativeVelocity.normalized);
		float factor = factor1+factor2;
		if(Vector2.Dot(normal,relativeVelocity)>0) factor=0;

		GetComponent<SpriteRenderer>().color=new Color(factor,factor,factor);
		return factor*relativeVelocity*pressureFactor;
	}

	static ContactFilter2D airBlowFilter = new ContactFilter2D();
	static BubbleNodeController() {
		airBlowFilter.useLayerMask=true;
		airBlowFilter.SetLayerMask(LayerMask.GetMask());
	}

	private void OnDrawGizmos() {
		Vector2 previousPosition = points[(thisIndex+points.Count-1)%points.Count].position;
		Vector2 nextPosition = points[(thisIndex+points.Count+1)%points.Count].position;
		Vector2 normal = Utils.Prudoct((nextPosition-previousPosition).normalized,Vector2.up);
		Gizmos.DrawLine(transform.position,transform.position+(Vector3)normal);
	}

}
