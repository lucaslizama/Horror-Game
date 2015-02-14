using UnityEngine;
using System.Collections;

public class FPS_Controller : MonoBehaviour {

    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float minYLookRotation = -60f;
    public float maxYlookRotation = 60f;
	public float groundRayLength = 2f;
    public float walkSpeed;
	public float runSpeed;
    public float jumpForce;
    public bool canWalk;
    public bool isWalking;
	public bool isRunning;
    public AudioClip stepsSound;
    public LayerMask groundedMask;


    private Transform cameraT;
    private float verticalLookRotation;

    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;

    private AudioSource soundMaker;
    private bool grounded;
    private bool grassFloor;

	// Use this for initialization
	void Awake () {
        cameraT = Camera.main.transform;
        soundMaker = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Screen.lockCursor = true;

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.fixedDeltaTime * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, minYLookRotation, maxYlookRotation);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        if(canWalk){
			if(!isRunning){
	            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
	            Vector3 targetMoveAmount = moveDir * walkSpeed;
	            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);
			}else{
				Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
				Vector3 targetMoveAmount = moveDir * runSpeed;
				moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);
			}
        }

        if(Input.GetButtonDown("Jump")){
          if(grounded && canWalk)
            rigidbody.AddForce(transform.up * jumpForce);
        }

		detectGround();
        
        if((Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f) && grounded && canWalk){
            isWalking = true;
            if(grassFloor.Equals(true) && soundMaker.isPlaying.Equals(false)){
                soundMaker.loop = true;
                soundMaker.clip = stepsSound;
                soundMaker.Play();
            }
        }else{
            soundMaker.Stop();
            isWalking = false;
        }

		if(Input.GetKey(KeyCode.LeftShift) && isWalking){
			isRunning = true;
		}else{
			isRunning = false;
		}
	}

    void FixedUpdate(){
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

	void detectGround() {
		Ray ray = new Ray(transform.position, -transform.up);
		
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, groundRayLength, groundedMask)){
			grounded = true;
			
			if (hit.transform.CompareTag("grass"))
				grassFloor = true;
		}else{
			grounded = false;
		}
	}
}
