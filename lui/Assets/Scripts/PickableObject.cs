using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	private GameObject objectToPick ;
	public Material shineMat ;
	public Material oldMat ;
	private bool picked = false ;
	private Camera mainCam ;
	public bool pickable = false ;

	void Start(){
		mainCam = Camera.main;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.gameObject.name);
		if (other.gameObject.name == "player") {
			Debug.Log("Player in trigger");
			objectToPick = transform.parent.gameObject ;
			if(oldMat == null){
				oldMat = objectToPick.renderer.materials[0] ; 
			}
			objectToPick.renderer.material = shineMat ;
			this.pickable = true ;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "player") {
			Debug.Log("Player out of trigger");
			objectToPick.renderer.material = oldMat ;
			this.pickable = false ;
		}
	}
}