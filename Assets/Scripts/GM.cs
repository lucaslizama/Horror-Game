using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {

	public GameObject collectibleCounter;

	private Text counterText;
	private int collectablesNumber;

	// Use this for initialization
	void Awake () {
		counterText = collectibleCounter.GetComponent<Text>();
		collectablesNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
		setCounterText();
		winGame();
	}

	private void setCounterText() {
		counterText.text = "Coleccionables encontrados: " + collectablesNumber;
	}

	public void sumCollectable() {
		collectablesNumber++;
	}

	public void loseGame() {
		Application.LoadLevel(2);
	}

	public void winGame() {
		if(collectablesNumber.Equals(8))
			Application.LoadLevel(1);
	}

	public void closeApplication() {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}
