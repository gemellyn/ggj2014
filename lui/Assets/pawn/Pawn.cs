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

    string actualAnim;

    private IA ia;

    public bool blockAnims;

	// Use this for initialization
	void Start () {
        ia = GetComponent<IA>();
	}

    void launchAnim(string anim)
    {
        

        if (blockAnims)
            return;
        if (!audio.isPlaying)
            audio.Play();

        if(actualAnim != anim)   
            transform.FindChild("sprite").GetComponent<Animator>().SetBool(anim, true);
        actualAnim = anim;
    }

    void setAnimWalk(bool leave)
    {
        switch (ia.getState())
        {
            case IA.STATE_AI.STATE_JOYEUX :
                if(leave)
                    launchAnim("Leave");
                else
                    launchAnim("Come");
                break;
            case IA.STATE_AI.STATE_TRANQUILLE :
            case IA.STATE_AI.STATE_SE_BARRE:
                if (leave)
                    launchAnim("LeaveCalm");
                else
                    launchAnim("ComeCalm");
                break;

            case IA.STATE_AI.STATE_PROSTRE:
                if (leave)
                    launchAnim("LeaveStand");
                else
                    launchAnim("ComeStand");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.F1)) 
            launchAnimEvent("Kite");
        if (Input.GetKey(KeyCode.F2))
            launchAnimEvent("BackFlying");
        if (Input.GetKey(KeyCode.F3))
            launchAnimEvent("Umbrella");
        if (Input.GetKey(KeyCode.F4))
            launchAnimEvent("Balloon");
        if (Input.GetKey(KeyCode.F5))
            launchAnimEvent("BackJump");

        if (dead)
            return;

        transform.FindChild("sprite").transform.LookAt(Camera.main.transform.position);

        bool comeToPlayer = true;
        if (Vector3.Dot((transform.position - Camera.main.transform.position).normalized, transform.forward) > 0)
            comeToPlayer = false;

        if (fixeJoueur)
            setAnimWalk(false);

        if (dosJoueur )
            setAnimWalk(true);

        if (!fixeJoueur && !dosJoueur)
        {
            if(comeToPlayer || eyeCheckTimer >= 0)
                setAnimWalk(false);
            else
                setAnimWalk(true);
        }         
        
        if (eyeCheckTimer >= 0)
            eyeCheckTimer -= Time.deltaTime;

    }

    public void suicide()
    {
        transform.FindChild("sprite").GetComponent<Animator>().SetBool("Suicide", true);
        dead = true;
    }

    public void launchAnimEvent(string animName)
    {
        print("launchAnimEvent " + animName);
        transform.FindChild("sprite").GetComponent<Animator>().SetBool(animName, true);
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

    
               
			/*} 
            else if (name.Equals("Girafe")|| Input.GetKey(KeyCode.F2)) {
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("BackJump");

            }
            else if (name.Equals("Alpaga") || Input.GetKey(KeyCode.F3))
            {
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Umbrella");

            }
            else if (name.Equals("Shop") || Input.GetKey(KeyCode.F4))
            {
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("Balloon");
			}
            else if (name.Equals("Sapin") || Input.GetKey(KeyCode.F5))
            {
                other.gameObject.GetComponent<Pawn>().launchAnimEvent("BackJump");

            }*/
}
