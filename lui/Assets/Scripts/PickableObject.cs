using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void function OnTriggerEnter (other : Collider) {
		Debug.Log ("Enter", other.gameObject);
	}
}
