using UnityEngine;
using System.Collections.Generic;

public class IA : MonoBehaviour {

    private float timeStop;

    public Transform player = null;
    private Pawn pawn;

    //Rumination
    private float rumination = 0;
    const float ruminPerSecBase =2;
    const float ruminPerSecPlayerNear = 1; 

    //Autiste
    private float autiste = 0;
    const float autistePerSecBase = 2;
    const float autistePerSecPlayerNear = 1; 

    //Eye check
    float timerEyeCheck = 0;
    const float periodEyeCheck = 2;
    
    //Etat
    private bool playerNear = false;

    //Zones
    private float zoneJoyeux = 10.0f;
    private float zoneTranquille = 8.0f;
    private float distForwardJoyeux = 10.0f;

    //Eval vitesse joueur
    private Vector3 lastPlayerPos;
    float playerSpeed;

    //Vitesse IA
    const float speedJoyeux = 3.5f;
    const float speedTranquille = 2.0f;
    const float speedProstre = 1.0f;
    const float speedSeBarre = 2.0f;

    //Etats
    private enum STATE_AI
    {
        STATE_JOYEUX,
        STATE_TRANQUILLE,
        STATE_PROSTRE,
        STATE_SE_BARRE
    };
    private STATE_AI state = STATE_AI.STATE_JOYEUX;
    private STATE_AI lastState = STATE_AI.STATE_TRANQUILLE;
    

	// Use this for initialization
	void Start () {
        pawn = GetComponent<Pawn>();
        player = GameObject.Find("player").transform;
    }

    //Il joue devant nous
    void stateJoyeux()
    {
        if (lastState != STATE_AI.STATE_JOYEUX)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            timeStop = 1;
            timerEyeCheck = Random.Range((int) (periodEyeCheck * 0.3),(int) (periodEyeCheck*1.3));
            pawn.dosAuJoueur(false);
            pawn.fixeLeJoueur(false);
            GetComponent<NavMeshAgent>().speed = speedJoyeux;
            print("joyeux");
        }

        if(player.GetComponent<PlayerPick>().getObject() != null)
            pawn.fixeLeJoueur(true);
        else
            pawn.fixeLeJoueur(false);


        timerEyeCheck -= Time.deltaTime;
        if (timerEyeCheck < 0)
        {
            print("eye check");
            pawn.eyeCheck();
            timerEyeCheck =  Random.Range((int) (periodEyeCheck),(int) (periodEyeCheck * 3));
        }

        if (GetComponent<NavMeshAgent>().remainingDistance < 1 || playerSpeed > 0.5)
            timeStop -= Time.deltaTime;

        if (timeStop <= 0)
        {
            timeStop = Random.Range(2, 3);
            //Zone devant le joueur
            GetComponent<NavMeshAgent>().destination = player.transform.position + (player.transform.forward * distForwardJoyeux) +  new Vector3(Random.Range(-zoneJoyeux/2, zoneJoyeux/2), 0, Random.Range(-zoneJoyeux/2, zoneJoyeux/2));
            print("follow go " + playerSpeed);
        }
    }

    //Il joue dans son coin
    void stateTranquille()
    {
        if (lastState != STATE_AI.STATE_TRANQUILLE)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            timeStop = 3;
            print("tranquille");
            pawn.dosAuJoueur(true);
            pawn.fixeLeJoueur(false);
            GetComponent<NavMeshAgent>().speed = speedTranquille;
        }
        if (GetComponent<NavMeshAgent>().remainingDistance < 1)
            timeStop -= Time.deltaTime;

        if (timeStop <= 0)
        {
            timeStop = Random.Range(4, 6);
            //Autour de lui
            GetComponent<NavMeshAgent>().destination = transform.position + new Vector3(Random.Range(-zoneTranquille / 2, zoneTranquille / 2), 0, Random.Range(-zoneTranquille / 2, zoneTranquille / 2));
            print("tranquille go " + GetComponent<NavMeshAgent>().destination.ToString());
            
        }
    }

    //Il joue dans son coin
    void stateProstre()
    {
        if (lastState != STATE_AI.STATE_PROSTRE)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            timeStop = 3;
            print("prostre");
            pawn.dosAuJoueur(true);
            pawn.fixeLeJoueur(false);
            GetComponent<NavMeshAgent>().speed = speedProstre;
        }
    }

    //Il part loin de nous
    void stateSeBarre()
    {
        if (lastState != STATE_AI.STATE_SE_BARRE)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            print("se barre");
            pawn.dosAuJoueur(true);
            pawn.fixeLeJoueur(false);
            GetComponent<NavMeshAgent>().speed = speedSeBarre;
            Vector3 dest = transform.position + transform.forward * 200;
            GetComponent<NavMeshAgent>().destination = dest;
        }
    }

    void stateAction()
    {
     
    }

    void drawDebugStuff()
    {
        if (state == STATE_AI.STATE_JOYEUX)
            Debug.DrawLine(transform.position + transform.up, player.position, Color.magenta);
        else
            Debug.DrawLine(transform.position + transform.up, player.position, Color.blue);
    }
	
	// Update is called once per frame
	void Update () {

        if (pawn.isDead())
        {
            //transform.FindChild("sprite").renderer.enabled = false;
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
            GetComponent<NavMeshAgent>().ResetPath();
            pawn.suicide();
        }

        //Test jauges
        state = STATE_AI.STATE_JOYEUX;
        if (autiste >= 40)
            state = STATE_AI.STATE_TRANQUILLE;
        if (autiste >= 70)
            state = STATE_AI.STATE_PROSTRE;
        if (autiste >= 90)
            state = STATE_AI.STATE_SE_BARRE;



        switch (state)
        {
            case STATE_AI.STATE_TRANQUILLE:
                stateTranquille();
                lastState = STATE_AI.STATE_TRANQUILLE;
                break;
             case STATE_AI.STATE_JOYEUX:
                stateJoyeux();
                lastState = STATE_AI.STATE_JOYEUX;
                break;
             case STATE_AI.STATE_PROSTRE:
                stateProstre();
                lastState = STATE_AI.STATE_PROSTRE;
                break;
             case STATE_AI.STATE_SE_BARRE:
                stateSeBarre();
                lastState = STATE_AI.STATE_SE_BARRE;
                break;
        }

        lastPlayerPos = player.transform.position;
	}

    public void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 180, 20), "rumination: " + rumination.ToString());
        GUI.Label(new Rect(5, 25, 180, 20), "timeStop: " + timeStop.ToString());
        GUI.Label(new Rect(5, 45, 180, 20), "timerEyeCheck: " + timerEyeCheck.ToString());
        switch (state)
        {
            case STATE_AI.STATE_TRANQUILLE:
                GUI.Label(new Rect(5, 65, 180, 20), "STATE_TRANQUILLE");
                break;
            case STATE_AI.STATE_JOYEUX:
                GUI.Label(new Rect(5, 65, 180, 20), "STATE_JOYEUX");
                break;
            case STATE_AI.STATE_PROSTRE:
                GUI.Label(new Rect(5, 65, 180, 20), "STATE_PROSTRE");
                break;
            case STATE_AI.STATE_SE_BARRE:
                GUI.Label(new Rect(5, 65, 180, 20), "STATE_SE_BARRE");
                break;
        }
    }
}
