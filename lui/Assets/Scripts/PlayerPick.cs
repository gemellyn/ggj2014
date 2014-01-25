using UnityEngine;
using System.Collections;

public class PlayerPick : MonoBehaviour {

	private GameObject hitObject ;
	private RaycastHit hit ;
	private bool show = false ;
	private float initialX ;
    private string cast = "" ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("e")) {
            Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position+Camera.main.transform.forward * 10.0f, Color.red);
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100)){
                Debug.Log(hit.collider.gameObject.name) ;
                Transform PP = hit.collider.gameObject.transform.Find("PickableProximity") ;
				if(PP){
					PickableObject po = PP.GetComponent("PickableObject") as PickableObject ;
					if(hit.collider.gameObject.collider.tag == "Pick" && po.pickable){
						hitObject = hit.collider.gameObject ;
						hitObject.transform.rigidbody.isKinematic = true;
                        hitObject.transform.parent.transform.parent = gameObject.transform;
                        hitObject.transform.parent.transform.LookAt(gameObject.transform.position + gameObject.transform.forward * 10.0f);
                        hitObject.transform.parent.transform.localPosition = new Vector3(1.0f, 2.0f, 1.0f) ;
					}
				}
			}
		}
        
		if (hitObject != null) {
            if (show){
                Debug.DrawLine(hitObject.transform.position, hitObject.transform.position + hitObject.transform.forward * 10.0f, Color.red);
                if (Physics.Raycast(hitObject.transform.position, hitObject.transform.forward, out hit, 100))
                {
                   cast = hit.collider.gameObject.name ;
                }
            }
		}
        
		if (Input.GetMouseButtonDown (0) && hitObject != null) {
            //hitObject.transform.position = new Vector3(hitObject.transform.position.x + (transform.forward.x * 0.5f), 0.15f, hitObject.transform.position.z);
			show = true ;
            hitObject.transform.parent.transform.localPosition = new Vector3(1.0f, 1.3f, 1.0f);
		}

		if (Input.GetMouseButtonUp (0)) {
            //hitObject.transform.position = new Vector3(hitObject.transform.position.x - (transform.forward.x * 0.5f), 0.3f, hitObject.transform.position.z);
			show = false ;
            hitObject.transform.parent.transform.localPosition = new Vector3(1.0f, 2.0f, 1.0f);
		}
		
		if(!Input.GetMouseButtonDown (0) && Input.GetKey("f")&& hitObject != null){
			hitObject.transform.parent.transform.parent = null;
            hitObject.transform.parent.transform.position = transform.position + transform.forward ;
			hitObject.transform.rigidbody.isKinematic = false;
			hitObject = null;
		}
	}

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 80, 20), cast.ToString());
    }
}
