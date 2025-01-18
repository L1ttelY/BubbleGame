using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController:MonoBehaviour {

	public static BubbleController instance;
	public bool propertyToughness;
	public bool propertyElasticness;
	public bool propertyDensity;
	public bool propertyWaterTight;

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
		foreach(var i in nodes) {
		}
	}

	void TryFixBubbleShape() {
		for(int i = 0;i<nodes.Length;i++) {
			int j = (i+1)%nodes.Length;
			Angle angle0 = new Angle(nodes[i].transform.position-transform.position);
			Angle angle1 = new Angle(nodes[j].transform.position-transform.position);
			if(angle1.IfBetween(angle0,angle0-Angle.Degree(90))) continue;
			Vector3 position0 = nodes[i].transform.position;
			nodes[i].transform.position=nodes[j].transform.position;
			nodes[j].transform.position=position0;

			Vector3 velocity0= nodes[i].GetComponent<Rigidbody2D>().velocity;
			nodes[i].GetComponent<Rigidbody2D>().velocity=nodes[j].GetComponent<Rigidbody2D>().velocity;
			nodes[j].GetComponent<Rigidbody2D>().velocity=velocity0;


			//Destroy(gameObject);
			break;
		}

	}


}
