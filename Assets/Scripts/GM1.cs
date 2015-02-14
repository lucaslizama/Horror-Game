using UnityEngine;
using System.Collections;

public class GM1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("resetGame", 2f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void resetGame(){
		Application.LoadLevel(0);
	}
}
