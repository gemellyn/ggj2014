using UnityEngine;
using System.Collections;

public class PlayerPick : MonoBehaviour {

	private GameObject hitObject ;
	private RaycastHit hit ;
	private bool show = false ;
	private float initialX ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("e")) {
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50)){
				Transform PP = hit.collider.gameObject.transform.Find("PickableProximity") ;
				if(PP){
					PickableObject po = PP.GetComponent("PickableObject") as PickableObject ;
					if(hit.collider.gameObject.collider.tag == "Pick" && po.pickable){
						hitObject = hit.collider.gameObject ;
						hitObject.transform.rigidbody.isKinematic = true;
					}
				}
			}
		}

		if (hitObject != null) {
			hitObject.transform.parent.transform.parent = gameObject.transform ;
			float y = 0.3f ;
			/*if(show){
				y -= 0.15f ;
			}*/
			hitObject.transform.position = new Vector3(hitObject.transform.position.x, y, hitObject.transform.position.z) ;
		}
        /*
		if (Input.GetMouseButtonDown (0) && hitObject != null) {
            hitObject.transform.position = new Vector3(hitObject.transform.position.x + (transform.forward.x * 0.5f), 0.15f, hitObject.transform.position.z);
			show = true ;

		}

		if (show && Input.GetMouseButtonUp (0) && hitObject != null) {
            hitObject.transform.position = new Vector3(hitObject.transform.position.x - (transform.forward.x * 0.5f), 0.3f, hitObject.transform.position.z);
			show = false ;
		}*/
		
		if(!Input.GetMouseButtonDown (0) && Input.GetKey("f")&& hitObject != null){
			hitObject.transform.parent.transform.parent = null;
			hitObject.transform.parent.transform.position = transform.position + transform.forward   ;
			hitObject.transform.rigidbody.isKinematic = false;
			hitObject = null;
		}
	}
}
