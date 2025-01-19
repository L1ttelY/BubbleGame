using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleNodeController:MonoBehaviour {

	List<Transform> points;
	int thisIndex;
	[SerializeField] float pressureFactor;
	[SerializeField] float clampForce = 1;
	[SerializeField] float relativeConstraintsThreshold = 0.5f;
	[SerializeField] float backSideForceMult = 0.1f;

	RelativeJoint2D relativeConstraints;

	Rigidbody2D rigidBody;
	Vector2 force;
	void Start() {
		rigidBody=GetComponent<Rigidbody2D>();
		relativeConstraints=GetComponent<RelativeJoint2D>();
		if(GetComponentInParent<SoftBody>()) {
			points=new List<Transform>(GetComponentInParent<SoftBody>().points);
			points.Remove(points[points.Count-1]);
		}
		//foreach(var i in points) if(i != null) Debug.Log(i);
		for(int i = 0;i<points.Count;i++) if(points[i]==transform) thisIndex=i;
	}

	void FixedUpdate() {
		force=Vector2.zero;
		Vector2 forceTotal = Vector2.zero;
		foreach(var testFlowingArea in FlowingArea.instances) {
			if(!testFlowingArea.collider.OverlapPoint(transform.position)) continue;
			RaycastHit2D hit = Physics2D.Raycast(
				testFlowingArea.transform.position,
				transform.position-testFlowingArea.transform.position,
				(transform.position-testFlowingArea.transform.position).magnitude,
				LayerMask.GetMask("Collider","ColliderSticky","Spike","Liquid","Default"),
				int.MinValue
			);
			if(hit.collider) {
				continue;
			}
			forceTotal+=GetForce(testFlowingArea.velocity);

		}
		if(forceTotal==Vector2.zero) forceTotal+=GetForce(Vector2.zero);
		rigidBody.AddForce(forceTotal);

		if(relativeConstraints) {
			float currentDifferenceToTarget = (relativeConstraints.target-(Vector2)transform.position).magnitude;
			float acceptedDifferenceToTarget = relativeConstraints.linearOffset.magnitude*relativeConstraintsThreshold;
			if(currentDifferenceToTarget>acceptedDifferenceToTarget) relativeConstraints.enabled=true;
			else relativeConstraints.enabled=false;
		}

	}


	Vector2 GetForce(Vector2 localAirVelocity) {
		Vector2 relativeVelocity = localAirVelocity-rigidBody.velocity;

		float factor = 1;
		if(points!=null) {
			Vector2 previousPosition = points[(thisIndex+points.Count-1)%points.Count].position;
			Vector2 nextPosition = points[(thisIndex+points.Count+1)%points.Count].position;
			Vector2 thisPosition = transform.position;

			Vector2 normal = Utils.Prudoct((nextPosition-previousPosition).normalized,Vector2.up);

			previousPosition=(previousPosition+thisPosition)/2f;
			nextPosition=(nextPosition+thisPosition)/2f;

			float factor1 = Utils.CrossAbs(previousPosition-thisPosition,relativeVelocity.normalized);
			float factor2 = Utils.CrossAbs(nextPosition-thisPosition,relativeVelocity.normalized);
			factor=factor1+factor2;
			if(Vector2.Dot(normal,relativeVelocity)>0) factor*=backSideForceMult;
		}

		return factor*Vector2.ClampMagnitude(relativeVelocity,clampForce)*pressureFactor;
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
		Gizmos.DrawLine(transform.position,transform.position+(Vector3)force);
	}

}
