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
        if(transform.FindChild("sprite").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Suicide"))
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Suicide", false);
        if (transform.FindChild("sprite").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Come"))
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Come", false);
        if (transform.FindChild("sprite").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Leave"))
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", false);

        if (dead)
            return;

        transform.FindChild("sprite").transform.LookAt(Camera.main.transform.position);

        bool comeToPlayer = true;
        if (Vector3.Dot((transform.position - Camera.main.transform.position).normalized, transform.forward) > 0)
            comeToPlayer = false;

        
        if (fixeJoueur)
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Come", true);

        if (dosJoueur )
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", true);

        if (!fixeJoueur && !dosJoueur)
        {
            print("base");
            if(comeToPlayer || eyeCheckTimer >= 0)
                transform.FindChild("sprite").GetComponent<Animator>().SetBool("Come", true);
            else
                transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", true);
        }
            
        
        if (eyeCheckTimer >= 0)
            eyeCheckTimer -= Time.deltaTime;

        if (transform.FindChild("sprite").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Suicide"))
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Suicide", false);
        if (transform.FindChild("sprite").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Come"))
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Come", false);
        if (transform.FindChild("sprite").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Leave"))
            transform.FindChild("sprite").GetComponent<Animator>().SetBool("Leave", false);
	}

    public void suicide()
    {
        //if(! Gun1.isPlaying)
            //Gun1.Play();
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
