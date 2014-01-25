using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

    public AudioSource Gun1 = null;
    private bool dead = false;
    private float eyeCheckTimer = 0;

    public Sprite face;
    public Sprite back;

    bool fixeJoueur = false;
    bool dosJoueur = false;

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

        //transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", true);

        if (!transform.FindChild("sprite").GetComponent<Animation>().animation.isPlaying)
        {

            if (Vector3.Dot(transform.forward, Camera.main.transform.forward) > 0 && eyeCheckTimer <= 0)
                transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", true);//transform.FindChild("sprite").GetComponent<SpriteRenderer>().sprite = back;
            else
                transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", false);

            if (fixeJoueur)
                transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", false);

            if (dosJoueur)
                transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", true);
        }
        if (eyeCheckTimer >= 0)
            eyeCheckTimer -= Time.deltaTime;
	}

    public void suicide()
    {
        if(! Gun1.isPlaying)
            Gun1.Play();
        transform.FindChild("sprite").GetComponent<Animator>().SetBool("Suicide", true);
        print("suicide done");
        dead = true;   
    }
    public bool isDead()
    {
        return dead;
    }

    public void eyeCheck()
    {
        eyeCheckTimer = 0.4f;
    }

    public void fixeLeJoueur(bool fixe)
    {
        fixeJoueur = fixe;
    }

    public void dosAuJoueur(bool dos)
    {
        dosJoueur = dos;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(200, 5, 180, 20), eyeCheckTimer.ToString());
    }
}
