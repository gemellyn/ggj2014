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
                    launchAnim("StandLeave");
                else
                    launchAnim("StandCome");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

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
}
