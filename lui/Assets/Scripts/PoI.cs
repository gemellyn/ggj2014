using UnityEngine;
using System.Collections;

public class PoI : MonoBehaviour {

    private bool used = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Lui")) {
            collider.enabled = false;

            if (!used){
                IA luiIA = other.gameObject.GetComponent("IA") as IA;
                luiIA.addRumination(-20);
                used = true;
            }
			if (name.Equals("Elephant")) {

                other.gameObject.GetComponent<Pawn>().launchAnimEvent("BackFlying");
			} else if (name.Equals("Girafe")) {
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Kite");

			} else if (name.Equals("Alpaga")) {
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Umbrella");

			} else if (name.Equals("Shop")) {
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Balloon");

			}
		}
	}
}
