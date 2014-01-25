using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	private GameObject objectToPick ;

    public float delight ;

	[HideInInspector]
	public bool pickable = false ;
    [HideInInspector]
    public bool picked = false;
    [HideInInspector]
    public bool used = false ;

    private string luiStatus = "out";
    private Transform PParticle ;
    ParticleSystem PPS ;
    private Transform PParticle2;
    ParticleSystem PPS2;

    void Start(){
        objectToPick = transform.parent.gameObject;
        PParticle = objectToPick.transform.Find("PickableParticle") as Transform;
        PPS = PParticle.GetComponent("ParticleSystem") as ParticleSystem;

        PParticle2 = objectToPick.transform.Find("PickableParticle2") as Transform;
        PPS2 = PParticle2.GetComponent("ParticleSystem") as ParticleSystem;
    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "player") {
            if (!this.picked){
                PPS.Play(false);
                this.pickable = true ;
            }
		}else if (!this.used && other.gameObject.name == "lui"){
            luiStatus = "IN";
            PPS2.Play(true) ;
            this.used = true ;
            IA luiIA = other.gameObject.GetComponent("IA") as IA ;
            luiIA.addRumination(-this.delight) ;
        }
	}

    void Update(){
        if (this.picked){
            PPS.Stop(false);
        }
    }

	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "player") {
            PPS.Stop(false);
			this.pickable = false ;
        } else if (other.gameObject.name == "lui"){
            luiStatus = "OUT";
        }
	}


    public void OnGUI(){
        GUI.Label(new Rect(600, 5, 180, 20), "Lui in trigger: " + luiStatus.ToString());
    }
}