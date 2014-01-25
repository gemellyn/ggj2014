using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Anim
        /*if (GetComponent<NavMeshAgent>().velocity.magnitude > 1.0f)
        {
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Moves", true);
        }
        else
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Moves", false);*/

        transform.FindChild("sprite").transform.LookAt(Camera.main.transform.position);
	}
}
