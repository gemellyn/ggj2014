using UnityEngine;
using System.Collections;

public class PlayerPick : MonoBehaviour {

	private GameObject hitObject ;
    private Transform transformToMove;
	private RaycastHit hit ;
    private RaycastHit hit2;

    private Transform PP;
    private PickableObject po;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = new Vector3(transform.position.x + transform.forward.x * 3.0f, transform.position.y + 10.0f, transform.position.z + transform.forward.z * 3.0f);
        //Debug.DrawLine(pos, pos-transform.up*100.0f, Color.red);
        
        if(Input.GetKeyDown("e")) {

            //Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position+Camera.main.transform.forward * 10.0f, Color.red);
            if (hitObject != null){
               
                //transformToMove.position = new Vector3(transform.position.x + transform.forward.x, transform.position.y, transform.position.z + transform.forward.z);
                RaycastHit hit = new RaycastHit() ;

                //Vector3 pos = new Vector3(transform.position.x + transform.forward.x * 2.0f, transform.position.y + 10.0f, transform.position.z + transform.forward.z * 2.0f);
        
                if (Physics.Raycast(pos, -hitObject.transform.up, out hit, Mathf.Infinity))
                {
                    //Debug.DrawRay(pos, -hitObject.transform.up * 100.0f, Color.blue, 10.0f);
                    hitObject.collider.enabled = true;
                    hitObject.transform.rigidbody.isKinematic = true;
                    
                    transformToMove.parent = null;
                    BoxCollider bc = hitObject.transform.GetComponent("BoxCollider") as BoxCollider;
                    //Debug.DrawLine(pos, hit.point, Color.black,10.0f);
                    transformToMove.position = new Vector3(hit.point.x, hit.point.y+bc.size.y/2, hit.point.z);
                    
                    if (Physics.Raycast(hit.point, transformToMove.position-hit.point , out hit2, Mathf.Infinity)){
                        transformToMove.position = new Vector3(transformToMove.position.x, transformToMove.position.y-hit2.distance, transformToMove.position.z);
                        //Debug.DrawRay(hit.point, transformToMove.position - hit.point, Color.blue, 10.0f);
                    }
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
