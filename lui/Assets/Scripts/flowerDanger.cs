using UnityEngine;
using System.Collections;

public class flowerDanger : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Lui")) {
            IA luiIA = other.gameObject.GetComponent("IA") as IA;
            luiIA.setRuminationSpeed(10.0f);
		}
	}
    
    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.tag.Equals("Lui"))
        {
            IA luiIA = other.gameObject.GetComponent("IA") as IA;
            //luiIA.addRumination(0.5f);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Lui"))
        {
            IA luiIA = other.gameObject.GetComponent("IA") as IA;
            luiIA.resetRuminationSpeed();
        }
    }
}
