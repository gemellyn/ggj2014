using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	public bool noBending;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 newVec;
		if (noBending) {
			//newVec = 
			//transform.position.y = Camera.main.transform.position.y;
		}
		transform.LookAt(Camera.main.transform.position);
	}
}
