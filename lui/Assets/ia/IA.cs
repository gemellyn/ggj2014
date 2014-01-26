using UnityEngine;
using System.Collections.Generic;

public class IA : MonoBehaviour {

    private float timeStop;

    public Transform player = null;
	public AudioSource ambiance;
    public AudioSource happySound;

    private Pawn pawn;

    //Rumination
    private float rumination = 0;

    private const float ruminPerSecBase =1.5f;
    private float ruminPerSec = ruminPerSecBase; 

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
    /*const float speedJoyeux = 5f;
    const float speedTranquille = 3.0f;
    const float speedProstre = 1.0f;
    const float speedSeBarre = 2.0f;*/
    const float speedJoyeux = 7.5f;
    const float speedTranquille = 4.5f;
    const float speedProstre = 1.0f;
    const float speedSeBarre = 2.0f;

    public bool stopMoving = false;

    //Etats
    public enum STATE_AI
    {
        STATE_JOYEUX,
        STATE_TRANQUILLE,
        STATE_PROSTRE,
        STATE_SE_BARRE
    };
    private STATE_AI state = STATE_AI.STATE_JOYEUX;
    private STATE_AI lastState = STATE_AI.STATE_TRANQUILLE;

    public void addRumination(float value)
    {
        rumination += value;
    }

    public void setRumination(float value)
    {
        rumination = value;
    }

    public void setRuminationSpeed(float value)
    {
        ruminPerSec = value;
    }

    public void resetRuminationSpeed()
    {
        ruminPerSec = ruminPerSecBase;
    }

   


    public STATE_AI getState()
    {
        return state;
    }


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

        Debug.DrawLine(player.transform.position + (player.transform.forward * distForwardJoyeux) + new Vector3(-zoneJoyeux / 2,0, -zoneJoyeux / 2), player.transform.position + (player.transform.forward * distForwardJoyeux) + new Vector3(zoneJoyeux / 2,0, zoneJoyeux / 2), Color.magenta);
        Debug.DrawLine(player.transform.position + (player.transform.forward * distForwardJoyeux) + new Vector3(-zoneJoyeux / 2,0, zoneJoyeux / 2), player.transform.position + (player.transform.forward * distForwardJoyeux) + new Vector3(zoneJoyeux / 2,0, -zoneJoyeux / 2), Color.magenta);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (pawn.isDead())
        {
            //transform.FindChild("sprite").renderer.enabled = false;
            return;
        }

        if(stopMoving)
            GetComponent<NavMeshAgent>().ResetPath();

        drawDebugStuff();

        //Update etat
        playerNear = false;
        if ((player.transform.position - transform.position).magnitude < 3.0)
            playerNear = true;

        playerSpeed = (lastPlayerPos - player.transform.position).magnitude / Time.deltaTime;

        //Updates jauges;

        rumination += Time.deltaTime * ruminPerSec;

		float pitch = rumination / 100.0f;
		pitch = 1 - pitch;
		float minPitch = 0.5f;
		float maxPitch = 1.0f;
		ambiance.pitch = (pitch * (maxPitch - minPitch)) + minPitch;
        happySound.pitch = (pitch * (maxPitch - minPitch)) + minPitch;
		print("PITCH BITCH: " + ambiance.pitch);


        //Test jauges
        if (rumination >= 100)
        {
            GetComponent<NavMeshAgent>().ResetPath();
            pawn.suicide();
        }

        //Etat
        state = STATE_AI.STATE_JOYEUX;
        if (rumination >= 70)
            state = STATE_AI.STATE_TRANQUILLE;
        if (rumination >= 80)
            state = STATE_AI.STATE_PROSTRE;
        if (rumination >= 90)
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
       /* GUI.Label(new Rect(5, 5, 180, 20), "rumination: " + rumination.ToString());
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
        }*/
    }
}
