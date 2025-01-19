using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController:MonoBehaviour {

	public static BubbleController instance;
	public bool propertyToughness;
	public bool propertyElasticness;
	public bool propertyDensity;
	public bool propertyWaterThrough;
	public bool propertyCanSplit;

	public BubbleNodeController[] nodes;

	private void Start() {
		instance=this;
		nodes=GetComponentsInChildren<BubbleNodeController>();
	}
	private void OnDestroy() {
		if(instance==this) instance=null;
	}

	private void FixedUpdate() {
		TryFixBubbleShape();
	}

	public void UpdateStat() {
		nodes=GetComponentsInChildren<BubbleNodeController>();
		foreach(var node in nodes) {
			if(propertyToughness) {
				foreach(var j in node.GetComponentsInChildren<SpringJoint2D>()) j.frequency*=1.5f;
			}
			if(propertyElasticness) {
				gameObject.layer=LayerMask.NameToLayer("BubbleElastic");
				node.gameObject.layer=LayerMask.NameToLayer("BubbleElastic");
			}
			if(propertyDensity){
				node.GetComponent<Rigidbody2D>().mass*=1.5f;
				node.GetComponent<Rigidbody2D>().gravityScale*=3f;
			}
		}
	}

	void TryFixBubbleShape() {
		int brokeCount = 0;
		for(int i = 0;i<nodes.Length;i++) {
			int j = (i+1)%nodes.Length;
			Angle angle0 = new Angle(nodes[i].transform.position-transform.position);
			Angle angle1 = new Angle(nodes[j].transform.position-transform.position);
			if(angle1.IfBetween(angle0,angle0-Angle.Degree(90))) continue;
			Vector3 position0 = nodes[i].transform.position;
			nodes[i].transform.position=nodes[j].transform.position;
			nodes[j].transform.position=position0;

			Vector3 velocity0 = nodes[i].GetComponent<Rigidbody2D>().velocity;
			nodes[i].GetComponent<Rigidbody2D>().velocity=nodes[j].GetComponent<Rigidbody2D>().velocity;
			nodes[j].GetComponent<Rigidbody2D>().velocity=velocity0;

			brokeCount++;

			//break;
		}
		if(brokeCount>2) Destroy(gameObject);
	}


}
