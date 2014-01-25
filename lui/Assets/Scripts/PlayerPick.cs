using UnityEngine;
using System.Collections;

public class PlayerPick : MonoBehaviour {

	private GameObject hitObject ;
	private RaycastHit hit ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("e")) {
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500)){
				Transform t = hit.collider.gameObject.transform.Find("PickableProximity") ;
				if(t){
					Debug.Log(hit.collider.gameObject.name) ;
					PickableObject po = t.GetComponent("PickableObject") as PickableObject ;
					if(hit.collider.gameObject.collider.tag == "Pick" && po.pickable){
						Debug.Log("Pick it please "+hit.collider.gameObject.name) ;
						hitObject = hit.collider.gameObject ;
					}
				}
			}
		}

		if (hitObject != null) {
			hitObject.transform.parent.transform.parent = gameObject.transform ;
			hitObject.transform.position = new Vector3(hitObject.transform.position.x, 0.3f, hitObject.transform.position.z) ;
		}
		
		if(Input.GetKey("f")&& hitObject != null){
			hitObject.transform.parent.transform.parent = null;
			hitObject.transform.parent.transform.position = gameObject.transform.position ;
			hitObject = null;
		}
	}
}
