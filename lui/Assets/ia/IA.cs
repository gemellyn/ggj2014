using UnityEngine;
using System.Collections.Generic;

public class IA : MonoBehaviour {

    private float timeStop;

    public Transform player = null;
    private Pawn pawn;

    //Rumination
    private float rumination = 0;
    const float ruminPerSecBase = 5;
    const float ruminPerSecPlayerNear = 1; 

    //Autiste
    private float autiste = 0;
    const float autistePerSecBase = 5;
    const float autistePerSecPlayerNear = 1; 
    
    //Etat
    private bool playerNear = false;

    


    //Etats
    private enum STATE_AI
    {
        STATE_BASE,
        STATE_FOLLOW,
        STATE_ROAM
    };
    private STATE_AI state = STATE_AI.STATE_FOLLOW;
    private STATE_AI lastState = STATE_AI.STATE_BASE;

    //timers
    float elapsedFollow = 0;
    

	// Use this for initialization
	void Start () {
        pawn = GetComponent<Pawn>();
        player = GameObject.Find("player").transform;
    }

    void stateFollow()
    {
        if (lastState != STATE_AI.STATE_FOLLOW)
            print("follow");

        if ((player.transform.position - transform.position).magnitude < 3.0)
        {
            GetComponent<NavMeshAgent>().ResetPath();
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
        }
    }

    void stateBase()
    {
        if (lastState != STATE_AI.STATE_BASE)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            print("base");

        }
    }

    void stateRoam()
    {
        if (lastState != STATE_AI.STATE_ROAM)
            print("roam");

        if (GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
        {
            //GetComponent<NavMeshAgent>().destinati//on = new Vector3(Random.Range(-200, 200), 0, Random.Range(-200, 200));
            Vector3 dest = player.transform.position + player.transform.forward * 200;
            GetComponent<NavMeshAgent>().destination = dest;
           //GetComponent<NavMeshAgent>().destination = new Vector3(Random.Range(-200, 200), 0, Random.Range(-200, 200));
        }
    }

    void stateAction()
    {
     
    }

    void drawDebugStuff()
    {
        if (state == STATE_AI.STATE_FOLLOW)
            Debug.DrawLine(transform.position + transform.up, player.position, Color.magenta);
        else
            Debug.DrawLine(transform.position + transform.up, player.position, Color.blue);
    }
	
	// Update is called once per frame
	void Update () {

        if (pawn.isDead())
        {
            transform.FindChild("sprite").renderer.enabled = false;
            return;
        }

        drawDebugStuff();

        //Update etat
        playerNear = false;
        if ((player.transform.position - transform.position).magnitude < 3.0)
            playerNear = true;

        //Updates jauges
        float ruminPerSec = ruminPerSecBase;
        if (playerNear)
            ruminPerSec = ruminPerSecPlayerNear;

        rumination += Time.deltaTime * ruminPerSec;

        float autistePerSec = autistePerSecBase;
        if (playerNear)
            autistePerSec = autistePerSecPlayerNear;

        autiste += Time.deltaTime * autistePerSec;

        //Test jauges
        if (rumination >= 100)
        {
            pawn.suicide();
        }

        //Test jauges
        state = STATE_AI.STATE_FOLLOW;
        if (autiste >= 20)
            state = STATE_AI.STATE_BASE;
        if (autiste >= 50)
            state = STATE_AI.STATE_ROAM;



        switch (state)
        {
            case STATE_AI.STATE_BASE:
                stateBase();
                lastState = STATE_AI.STATE_BASE;
                break;
             case STATE_AI.STATE_FOLLOW:
                stateFollow();
                lastState = STATE_AI.STATE_FOLLOW;
                break;
             case STATE_AI.STATE_ROAM:
                stateRoam();
                lastState = STATE_AI.STATE_ROAM;
                break;
        }
	}
}
