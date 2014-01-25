using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "player") {
			Debug.Log("Player in trigger");
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "player") {
			Debug.Log("Player out of trigger");
		}
	}
}