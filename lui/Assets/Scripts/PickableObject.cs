using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	private GameObject objectToPick ;

    public float delight ;

	[HideInInspector]
	public Material oldMat ;
	[HideInInspector]
	public bool pickable = false ;
    [HideInInspector]
    public bool picked = false;
    [HideInInspector]
    public bool active = true ;
    [HideInInspector]
    public string luiStatus = "out";

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "player") {
			objectToPick = transform.parent.gameObject ;
            if (!this.picked){
                objectToPick.renderer.enabled = true;
                this.pickable = true;
            }
		}else if (other.gameObject.name == "lui"){
            luiStatus = "IN";
        }
	}

    void Update(){
        if (this.picked){
            objectToPick.renderer.enabled = false;
        }
    }

	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "player") {
			//objectToPick.renderer.material = oldMat ;
            objectToPick.renderer.enabled = false;
			this.pickable = false ;
        } else if (other.gameObject.name == "lui"){
            luiStatus = "OUT";
        }
	}


    public void OnGUI(){
        GUI.Label(new Rect(600, 5, 180, 20), "Lui in trigger: " + luiStatus.ToString());
    }
}