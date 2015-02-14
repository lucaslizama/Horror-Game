using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public float interactionDistance;
	public GameObject puntero;
	public GameObject descripcion;
	public GameObject GM;
	public LayerMask whatIsInteractable;

	private Image punto;
	private Text collectDescription;
	private GM gameManager;


	// Use this for initialization
	void Awake () {
		punto = puntero.GetComponent<Image>();
		collectDescription = descripcion.GetComponent<Text>();
		gameManager = GM.GetComponent<GM>();
	}
	
	// Update is called once per frame
	void Update () {
		changeCursorToInteract ();
		interactWith ();
		showMessageWhenLookingAt();
	}

	#region Class Methods
    /// <summary>
    /// En este metodo especifico cada accion a realizar dependiendo
    /// del objeto con el cual esté interactuando el jugador.
    /// </summary>
	void interactWith(){
		if(Input.GetMouseButtonDown(0)){

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,interactionDistance,whatIsInteractable)){
				if(hit.transform.CompareTag("door")){
					Door puerta = hit.transform.GetComponentInParent<Door>();
					if(!puerta.doorIsOpen){
						puerta.openTheDoor = true;
					}else if(puerta.doorIsOpen){
						puerta.closeTheDoor = true;
					}
					Debug.DrawRay(ray.origin,ray.direction * interactionDistance,Color.yellow);
				}

				if(hit.transform.CompareTag("collectable")){
					gameManager.sumCollectable();
					GameObject collectable = hit.transform.gameObject;
					Destroy (collectable);
				}
			}
		}
	}
    /// <summary>
    /// Este metodo cambia el color del cursor cuando estamos
    /// frente a un objeto interactuable.
    /// </summary>
	void changeCursorToInteract(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,interactionDistance,whatIsInteractable)){
			punto.color = Color.green;
		}else{
			punto.color = Color.white;
		}
	}
    /// <summary>
    /// Muestra un mensaje en pantalla segun el objeto
    /// al que estemos mirando.
    /// </summary>
	void showMessageWhenLookingAt () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,interactionDistance,whatIsInteractable)){
			if(hit.transform.CompareTag("collectable")){
				string texto = "Esto es un coleccionable\nTomalo aweonao";
				collectDescription.text = texto;
				descripcion.SetActive(true);
			}
		}else{
			descripcion.SetActive(false);
		}
	}
	#endregion Class Methods
}
