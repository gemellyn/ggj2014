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
            //Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position+Camera.main.transform.forward * 10.0f, Color.red);
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100)){
                //Debug.Log(hit.collider.gameObject.name) ;
                Transform PP = hit.collider.gameObject.transform.Find("PickableProximity") ;
				if(PP){
					PickableObject po = PP.GetComponent("PickableObject") as PickableObject ;
					if(hit.collider.gameObject.collider.tag == "Pick" && po.pickable){
						hitObject = hit.collider.gameObject ;
						hitObject.transform.rigidbody.isKinematic = true;
                        hitObject.transform.parent.transform.parent = transform;
                        hitObject.transform.parent.transform.position = new Vector3(hitObject.transform.parent.transform.position.x, transform.position.y + 1.0f, hitObject.transform.parent.transform.position.z);
                        po.picked = true ;
                    }
				}
			}
		}
		
		if(Input.GetKey("f")&& hitObject != null){
            Transform PP = hitObject.transform.Find("PickableProximity");
            PickableObject po = PP.GetComponent("PickableObject") as PickableObject;
			hitObject.transform.parent.transform.parent = null;
            hitObject.transform.parent.transform.position = transform.position + transform.forward ;
			hitObject.transform.rigidbody.isKinematic = false;
			hitObject = null;
            po.picked = false;
		}

	}

}
