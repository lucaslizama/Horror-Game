using UnityEngine;
using System.Collections;

public class Player_Animations : MonoBehaviour {

	private Animator anim;
	private FPS_Controller fpsController;

	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
		fpsController = GetComponent<FPS_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		if(fpsController.isRunning){
			anim.SetBool("isRunning",true);
		}else{
			anim.SetBool("isRunning",false);
		}
	}
}
