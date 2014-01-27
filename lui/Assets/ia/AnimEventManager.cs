using UnityEngine;
using System.Collections;

public class AnimEventManager : MonoBehaviour {
    IA ia;
    Pawn pawn;
    Transform parent;
    Animator animator;
    bool showEscape = false;

    public void showEscapeGUI()
    {
        showEscape = true;
    }
    public void youCanMove()
    {
        print("youCanMove");
        ia.stopMoving = false;
    }

    public void stopMoving()
    {
        print("stopMoving");
        ia.stopMoving = true;
    }

    public void blockOtherAnims()
    {
        print("blockOtherAnims");
        pawn.blockAnims = true;
    }

    public void allowOtherAnims()
    {
        print("blockOtherAnims");
        pawn.blockAnims = false;
        pawn.actualAnim = "";
    }

    public void OnGUI()
    {
        if (showEscape)
        {
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.fontSize = 50;
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 45, 300, 200), "Press ESC to retry !", centeredStyle);

        }
        // GUI.Label(new Rect(200, 5, 180, 20), eyeCheckTimer.ToString());
    }

     

    // Use this for initialization
	void Start () {
        parent = transform.parent;
        ia = transform.parent.GetComponent<IA>();
        pawn = transform.parent.GetComponent<Pawn>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
