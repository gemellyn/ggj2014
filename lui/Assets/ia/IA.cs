﻿using UnityEngine;
using System.Collections.Generic;

public class IA : MonoBehaviour {

    private float timeStop;

    public Transform player = null;
    private Pawn pawn;

    //Rumination
    private float rumination = 0;
    const float ruminPerSecBase = 1;
    const float ruminPerSecPlayerNear = 1; 

    //Autiste
    private float autiste = 0;
    const float autistePerSecBase = 1;
    const float autistePerSecPlayerNear = 1; 
    
    //Etat
    private bool playerNear = false;

    //Zones
    private float zoneFrontSize = 10.0f;
    private float zoneBackSize = 10.0f;

    //Eval vitesse
    private Vector3 lastPlayerPos;
    float playerSpeed;

    


    //Etats
    private enum STATE_AI
    {
        STATE_BASE,
        STATE_FOLLOW,
        STATE_ROAM
    };
    private STATE_AI state = STATE_AI.STATE_FOLLOW;
    private STATE_AI lastState = STATE_AI.STATE_BASE;
    

	// Use this for initialization
	void Start () {
        pawn = GetComponent<Pawn>();
        player = GameObject.Find("player").transform;
    }

    //Il joue devant nous
    void stateFollow()
    {
        if (lastState != STATE_AI.STATE_FOLLOW)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            timeStop = 1;
            print("follow");
        }

        if (GetComponent<NavMeshAgent>().remainingDistance < 1 || playerSpeed > 0.5)
            timeStop -= Time.deltaTime;

        if (timeStop <= 0)
        {
            timeStop = Random.Range(2, 3);
            //Zone devant le joueur
            GetComponent<NavMeshAgent>().destination = player.transform.position + (player.transform.forward * (zoneFrontSize/1.5f)) +  new Vector3(Random.Range(-zoneFrontSize/2, zoneFrontSize/2), 0, Random.Range(-zoneFrontSize/2, zoneFrontSize/2));
            print("follow go " + playerSpeed);
        }
    }

    //Il joue derrière de nous
    void stateBase()
    {
        if (lastState != STATE_AI.STATE_BASE)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            timeStop = 3;
            print("base");

        }
        if (GetComponent<NavMeshAgent>().remainingDistance < 1 || playerSpeed > 0.5)
            timeStop -= Time.deltaTime;

        if (timeStop <= 0)
        {
            timeStop = Random.Range(4, 6);
            //Zone derriere le joueur
            GetComponent<NavMeshAgent>().destination = player.transform.position - (player.transform.forward * (zoneFrontSize / 1.5f)) - new Vector3(Random.Range(-zoneFrontSize / 2, zoneFrontSize / 2), 0, Random.Range(-zoneFrontSize / 2, zoneFrontSize / 2));
            print("base go " + GetComponent<NavMeshAgent>().destination.ToString());
            
        }
    }

    //Il part loin de nous
    void stateRoam()
    {
        if (lastState != STATE_AI.STATE_ROAM)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            print("roam");
        }

        if (GetComponent<NavMeshAgent>().remainingDistance < 5 )
        {
            //GetComponent<NavMeshAgent>().destinati//on = new Vector3(Random.Range(-200, 200), 0, Random.Range(-200, 200));
            Vector3 dest = player.transform.position - player.transform.forward * 200;
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

        playerSpeed = (lastPlayerPos - player.transform.position).magnitude / Time.deltaTime;

        //Updates jauges;
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
        if (autiste >= 50)
            state = STATE_AI.STATE_BASE;
        if (autiste >= 90)
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

        lastPlayerPos = player.transform.position;
	}

    public void OnGUI()
    {
        GUI.Label(new Rect (5,5,80,20), rumination.ToString());
        GUI.Label(new Rect(5, 30, 80, 20), timeStop.ToString());
    }
}
