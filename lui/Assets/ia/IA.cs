using UnityEngine;
using System.Collections.Generic;

public class IA : MonoBehaviour {

    private float timeStop;

    public Transform player = null;
    private Pawn pawn;

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
        
    }

    void stateRoam()
    {
        timeStop -= Time.deltaTime;
        if (timeStop <= 0)
        {
            GetComponent<NavMeshAgent>().destination = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            timeStop = Random.Range(5, 6);
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

        drawDebugStuff();

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
