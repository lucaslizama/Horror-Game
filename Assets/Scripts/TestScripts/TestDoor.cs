using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof(AudioSource))]

public class TestDoor : MonoBehaviour {
    
    private Animator animator;
    private Transform doorTransform;
    private AudioSource doorAS;

    public bool actionTrigger;
    private bool isMoving;
    private bool isOpen;

    #region MonoBehaviour Methods
    void Awake () {
        animator = GetComponent<Animator>();
        doorTransform = GetComponent<Transform>();
        doorAS = GetComponent<AudioSource>();
        actionTrigger = false;
        isMoving = false;
        isOpen = false;
	}
	
	void Update () {
        openDoor();
	}
    #endregion MonoBehaviour Methods

    #region Door Methods
    public void sendOpenCloseMessage()
    {
        if (actionTrigger.Equals(false))
            actionTrigger = true;
    }

    public void setDoorStill(int option)
    {
        if (option == 0)
        {
            isMoving = false;
            isOpen = true;
            actionTrigger = false;
        }
        else if (option == 1)
        {
            isMoving = false;
            isOpen = false;
            actionTrigger = false;
        }
    
    }

    private void openDoor()
    {
        if (actionTrigger.Equals(true))
        {
            if (isOpen.Equals(false) && isMoving.Equals(false))
            {
                isMoving = true;
                animator.SetTrigger("Open");
                Debug.Log("Door is Opening");
            }
            else if (isOpen.Equals(true) && isMoving.Equals(false))
            {
                isMoving = true;
                animator.SetTrigger("Close");
                Debug.Log("Door is Closing");
            }
        }
    }
    #endregion Door Methods
}
