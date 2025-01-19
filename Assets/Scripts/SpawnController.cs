using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class SpawnController:MonoBehaviour {

	[SerializeField] GameObject prefab;
	[SerializeField] Transform targetPosition;
	public static SpawnController instance;
	private void Start() {
		instance=this;
	}

	private void Update() {
		//Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//if(GetComponent<Collider2D>().OverlapPoint(mousePosition)) TrySpawn();
	}

	public void TrySpawn() {
		if(BubbleController.instance) return;
		BubbleController newBubble = Instantiate(prefab,targetPosition.position,Quaternion.identity).GetComponent<BubbleController>();
		newBubble.propertyDensity=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Heavy);
		newBubble.propertyElasticness=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Elastic);
		newBubble.propertyToughness=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Toughness);
		newBubble.propertyWaterThrough=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Submerge);
		newBubble.propertyCanSplit=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Split);
		newBubble.UpdateStat();
	}

}
