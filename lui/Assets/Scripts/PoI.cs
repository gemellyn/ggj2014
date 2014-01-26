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
            IA luiIA = other.gameObject.GetComponent("IA") as IA;

            collider.enabled = false;

			if (name.Equals("Elephant") ) {
                 luiIA.setRumination(20);
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("BackFlying");
			} 
            else if (name.Equals("Girafe")) {
                 luiIA.setRumination(20);
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Kite");

            }
            else if (name.Equals("Alpaga"))
            {
                 luiIA.setRumination(20);
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Umbrella");

            }
            else if (name.Equals("Shop") )
            {
                 luiIA.setRumination(20);
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Balloon");
			}
            else if (name.Equals("Sapin") )
            {
                 luiIA.setRumination(20);
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("BackJump");

            }
		}
	}

    
}
