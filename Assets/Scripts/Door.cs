using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public float openCloseSpeed;
	public bool doorIsOpen;
	public bool openTheDoor;
	public bool closeTheDoor;
	public AudioClip doorOpenSound;
	public AudioClip doorClosedSound;

	private const float OPEN_ROTATION = 270f;
	private AudioSource audioSource;
	
	void Awake () {
		transform.rotation = Quaternion.identity;
		doorIsOpen = false;
		openTheDoor = false;
		closeTheDoor = false;
		audioSource = GetComponent<AudioSource> ();
	}

	void Update () {
		if(openTheDoor.Equals(true) && !doorIsOpen){
			openDoor(openCloseSpeed);
		}

		if(closeTheDoor.Equals(true) && doorIsOpen){
			closeDoor(openCloseSpeed);
		}
	}

	/// <summary>
	/// Opens the door.
	/// </summary>
	/// <param name="speed">Speed.</param>
	private void openDoor(float speed){
		if(!doorIsOpen){
			if(!audioSource.isPlaying){
				audioSource.clip = doorOpenSound;
				audioSource.Play();
			}

			Vector3 targetRotation = new Vector3 (0f, openCloseSpeed * Time.deltaTime, 0f);
			transform.Rotate (-targetRotation);

			if(transform.eulerAngles.y <= Door.OPEN_ROTATION){
				transform.eulerAngles = new Vector3(0f,270f,0f);
				doorIsOpen = true;
				openTheDoor = false;
				audioSource.Stop();
			}
		}
	}

	/// <summary>
	/// Closes the door.
	/// </summary>
	/// <param name="speed">Speed.</param>
	private void closeDoor(float speed){
		if(doorIsOpen){
			if(!audioSource.isPlaying){
				audioSource.clip = doorClosedSound;
				audioSource.Play();
			}

			Vector3 targetRotation = new Vector3 (0f, openCloseSpeed * Time.deltaTime, 0f);
			transform.Rotate (targetRotation);
			
			if(transform.eulerAngles.y < 270f){
				transform.eulerAngles = Vector3.zero;
				doorIsOpen = false;
				closeTheDoor = false;
				audioSource.Stop();
			}
		}
	}


}
