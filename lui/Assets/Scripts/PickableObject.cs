using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	private GameObject objectToPick ;
	public Material shineMat ;
	public Material oldMat ;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "player") {
			Debug.Log("Player in trigger");
			objectToPick = this.transform.parent.gameObject ;
			if(oldMat == null){
				oldMat = objectToPick.renderer.materials[0] ;
			}
			objectToPick.renderer.material = shineMat ;
			Debug.Log(objectToPick.renderer.materials[0]) ;
			Debug.Log(this.transform.parent.gameObject.name) ;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "player") {
			Debug.Log("Player out of trigger");
			objectToPick.renderer.material = oldMat ;
		}
	}
}