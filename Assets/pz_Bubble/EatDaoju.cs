using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EatDaoju:MonoBehaviour {

	[SerializeField] BubbleMaterial material;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag=="Points") {
			if(!BubbleCraftingController.materialsUnlocked.Contains(material))
				BubbleCraftingController.materialsUnlocked.Add(material);
			Debug.Log("<color=yellow>³Ôµ½µÀ¾ß");
            Destroy(gameObject);
		}
	}

}
