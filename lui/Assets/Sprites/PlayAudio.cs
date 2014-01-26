using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

    public AudioSource audio;
	// Use this for initialization
    void OnEnable()
    {
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
