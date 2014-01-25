using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

    public AudioSource Gun1 = null;
    private bool dead = false;

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

    public void suicide()
    {
        if(! Gun1.isPlaying)
            Gun1.Play();
        print("suicide done");
        dead = true;

      
        
    }
    public bool isDead()
    {
        return dead;
    }
}
