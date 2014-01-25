using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	private GameObject objectToPick ;

    public float delight ;

	[HideInInspector]
	public Material oldMat ;
	[HideInInspector]
	public bool pickable = false ;


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "player") {
			objectToPick = transform.parent.gameObject ;
			if(oldMat == null){
				oldMat = objectToPick.renderer.materials[0] ; 
			}
			objectToPick.renderer.material =  Resources.Load("Pickable") as Material ;
			this.pickable = true ;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "player") {
			objectToPick.renderer.material = oldMat ;
			this.pickable = false ;
		}
	}
}