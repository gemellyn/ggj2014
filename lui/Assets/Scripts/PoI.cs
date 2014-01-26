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
            if (!used){
                IA luiIA = other.gameObject.GetComponent("IA") as IA;
                luiIA.addRumination(-20);
                used = true;
            }
			if (name.Equals("Elephant")) {

			} else if (name.Equals("Girafe")) {
				other.GetComponent<Pawn>().launchAnimEvent("Kite");
			} else if (name.Equals("Alpaga")) {

			}
		}
	}
}
