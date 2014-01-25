using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

    public AudioSource Gun1 = null;
    private bool dead = false;
    private float eyeCheckTimer = 0;
    public bool isCalm = false;

    public Sprite face;
    public Sprite back;

    bool fixeJoueur = false;
    bool dosJoueur = false;

	// Use this for initialization
	void Start () {
	
	}

    void launchAnim(string anim)
    {
        if(transform.FindChild("sprite").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(anim) == false)
            transform.FindChild("sprite").GetComponent<Animator>().SetBool(anim, true);
    }

    void setAnimWalk(bool leave)
    {
        if (isCalm && leave)
            launchAnim("LeaveCalm");
        if (isCalm && !leave)
            launchAnim("ComeCalm");
        if (!isCalm && leave)
            launchAnim("Leave");
        if (!isCalm && !leave)
            launchAnim("Come");
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
