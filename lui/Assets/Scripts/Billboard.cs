using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	public bool noBending;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newVec = Camera.main.transform.position;
		if (noBending) {
			newVec.y = transform.position.y;
		}
		transform.LookAt(newVec);
	}
}
