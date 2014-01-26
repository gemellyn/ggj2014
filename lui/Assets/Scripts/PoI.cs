using UnityEngine;
using System.Collections;

public class PoI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other) {
		if (other.tag.Equals("Lui")) {
			//TODO trigger event
		}
	}
}
