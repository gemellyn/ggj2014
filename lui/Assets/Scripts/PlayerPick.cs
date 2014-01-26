using UnityEngine;
using System.Collections;

public class PlayerPick : MonoBehaviour {

	private GameObject hitObject ;
    private Transform transformToMove;
	private RaycastHit hit ;

    private Transform PP;
    private PickableObject po;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("e")) {

            //Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position+Camera.main.transform.forward * 10.0f, Color.red);
            if (hitObject != null){
                hitObject.collider.enabled = true;
                hitObject.transform.rigidbody.isKinematic = false;

                transformToMove.parent = null;
                BoxCollider bc = hitObject.transform.GetComponent("BoxCollider") as BoxCollider;
                transformToMove.position = new Vector3(transform.position.x + transform.forward.x, transform.position.y, transform.position.z + transform.forward.z);
                RaycastHit hit = new RaycastHit() ;
                if (Physics.Raycast(transformToMove.position, -transformToMove.up, out hit, Mathf.Infinity))
                {
                    transformToMove.position = new Vector3(transformToMove.position.x, hit.point.y, transformToMove.position.z);
                }
                
                hitObject = null;
                po.picked = false;
            }
            else{
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100))
                {
                    //Debug.Log(hit.collider.gameObject.name) ;
                    PP = hit.collider.gameObject.transform.Find("PickableProximity");
                    if (PP)
                    {
                        po = PP.GetComponent("PickableObject") as PickableObject;
                        if (hit.collider.gameObject.collider.tag == "Pick" && po.pickable)
                        {
                            hitObject = hit.collider.gameObject;
                            hitObject.transform.rigidbody.isKinematic = true;
                            hitObject.collider.enabled = false;
                            transformToMove = hitObject.transform.parent.transform ;
                            transformToMove.parent = transform;
                            transformToMove.position = new Vector3(transformToMove.position.x, transformToMove.position.y + 1.0f,transformToMove.transform.position.z);
                            po.picked = true;
                        }
                    }
                }
            }
		}
		/*
		if(Input.GetKey("f")&& hitObject != null){
            Transform PP = hitObject.transform.Find("PickableProximity");
            PickableObject po = PP.GetComponent("PickableObject") as PickableObject;
			hitObject.transform.parent.transform.parent = null;
            hitObject.transform.parent.transform.position = transform.position + transform.forward ;
			hitObject.transform.rigidbody.isKinematic = false;
            hitObject.collider.enabled = true;
			hitObject = null;
            po.picked = false;
		}*/

	}

    public Transform getObject(){
        if (hitObject != null){
            return hitObject.transform ;
        }
        else{
            return null ;
        }
    }

}
