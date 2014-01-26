using UnityEngine;
using System.Collections;

public class AnimEventManager : MonoBehaviour {
    IA ia;
    Pawn pawn;
    Transform parent;
    Animator animator;
    

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
