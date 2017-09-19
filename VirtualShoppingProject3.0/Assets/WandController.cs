using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/**
	 * Basic implementation of how to use a Vive controller as an input device.
	 * Can only interact with items with InteractableBase component
	 */
public class WandController : MonoBehaviour
{
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private Valve.VR.EVRButtonId trackPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	//private Valve.VR.EVRButtonId downButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }

	private SteamVR_TrackedObject trackedObj;

	HashSet<InteractableBase> objectsHoveringOver = new HashSet<InteractableBase> ();

	private InteractableBase closestItem;
	private InteractableBase interactingItem;






	public GameObject menuGO;
	public TeleportVive teleportVive;

	public ActionListener actionListener; 

	groceryGame grocery;

	SphereCollider modelCollider;
	public UnityEngine.UI.Button[] buttons;


	public int activeMenuButton = 0;

	// Use this for initialization
	void Start ()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		grocery = GameObject.Find ("GroceryGame").GetComponent<groceryGame> ();

		buttons = menuGO.GetComponentsInChildren<UnityEngine.UI.Button> ();

		modelCollider = transform.Find ("Model").GetComponent<SphereCollider> ();

		actionListener = GameObject.Find ("Menu").GetComponent<ActionListener> ();
			
	}

	// Update is called once per frame
	void Update ()
	{
		if (controller == null) {
			Debug.Log ("Controller not initialized");
			return;
		}


		/*float minDistance = float.MaxValue;

		float distance;

		foreach (InteractableBase item in objectsHoveringOver)
		{
			distance = (item.transform.position - transform.position).sqrMagnitude;

			if (distance < minDistance)
			{
				minDistance = distance;
				closestItem = item;
			}
		}

		interactingItem = closestItem;
		closestItem = null;


		if (interactingItem != null) {

			interactingItem.GetComponent<Product> ().Highlight2();
		}
		*/








		if (controller.GetPressDown (gripButton) || controller.GetPressDown (triggerButton)) {
			Debug.Log ("TriggerDown");

			actionListener.triggerDown ();

			modelCollider.enabled = false;

			// Find the closest item to the hand in case there are multiple and interact with it
			float minDistance = float.MaxValue;

			float distance;
			foreach (InteractableBase item in objectsHoveringOver) {
				distance = (item.transform.position - transform.position).sqrMagnitude;

				if (distance < minDistance) {
					minDistance = distance;
					closestItem = item;
				}
			}

			interactingItem = closestItem;
			closestItem = null;



			if (interactingItem) {

	               

				// Begin Interaction should already end interaction from previous
				if (controller.GetPressDown (gripButton)) {
					if (interactingItem.GetComponent<Product> () != null) {
						interactingItem.GetComponent<Product> ().Highlight ();
					}
					interactingItem.OnGripPressDown (this);
				}
				if (controller.GetPressDown (triggerButton)) {
	                   

					if (interactingItem.GetComponent<Product> () != null) {
						interactingItem.GetComponent<Product> ().Highlight ();
					}
					interactingItem.OnTriggerPressDown (this);
				}
			}
		}

		if (controller.GetPressUp (gripButton) && interactingItem != null) {
			
			Debug.Log ("TRIGGERup");
			//interactingItem.EndInteraction(this);

			if (interactingItem.GetComponent<Product> () != null) {
				
				interactingItem.GetComponent<Product> ().removeHighlight ();
			}
			interactingItem.OnGripPressUp (this);

		}


		if (controller.GetPressUp (triggerButton) ){
			actionListener.triggerUp ();
			if (modelCollider != null) {
				modelCollider.enabled = true;
			}
		}

		if (controller.GetPressUp (triggerButton) && interactingItem != null) {

			Debug.Log ("TRIGGER INTERACTION");
			if (interactingItem.GetComponent<Product> () != null) {
				interactingItem.GetComponent<Product> ().removeHighlight ();
			}
			interactingItem.OnTriggerPressUp (this);
		}






		if (controller.GetPressUp (trackPad) && SteamVR_Controller.Input ((int)trackedObj.index).GetAxis ().y > 0.00f) {
			Debug.Log ("UP");

			if (activeMenuButton > 0) {
				activeMenuButton--;
				changeMenuSelection (activeMenuButton);
			}
		}


		if (controller.GetPressUp (trackPad) && SteamVR_Controller.Input ((int)trackedObj.index).GetAxis ().y < 0.00f) {
			//GetComponents<SteamVR_TrackedController> ();
			Debug.Log ("DOWN");

			if (activeMenuButton < (buttons.Length - 1)) {
				activeMenuButton++;
				changeMenuSelection (activeMenuButton);
			}

		}


		if (controller.GetPressUp (menuButton)) {

			actionListener.triggerMenu ();

			/*if (menuGO.activeSelf == true) {
				actionListener.menuClose ();
				//toggleMenu (false);
			} else {
				actionListener.menuOpen ();
				//toggleMenu (true);
			}*/

			Debug.Log ("menu" + menuGO.activeSelf + " " + menuGO.activeInHierarchy);

		}

		if (controller.GetPressUp (triggerButton) && (menuGO.activeSelf == true)) {

			Debug.Log ("ACTION START" + activeMenuButton);


			switch (activeMenuButton) {


			case 0:
				Debug.Log ("000000" + activeMenuButton);

				SceneManager.LoadScene ("StoreViveSmaller");

				break;
			case 1:
					

				SceneManager.LoadScene ("StoreViveBig");

				break;
			case 2:
					
					//Supermarket Game

				grocery.startGame ();
				toggleMenu (false);
					//SceneManager.LoadScene ("StoreGameThrow");

				break;
			case 3:
					

				Application.Quit ();

				break;
			
			}


		}








	}


	private void toggleMenu (bool show)
	{
		
		if (show == true) {

			menuGO.SetActive (true);
			teleportVive.enabled = false;
		} else {
			menuGO.SetActive (false);
			teleportVive.enabled = true;
		}
	}


	private void changeMenuSelection (int activeMenuButton)
	{
		int i = 0;
		foreach (UnityEngine.UI.Button button in buttons) {


			if (activeMenuButton == i) {
				button.image.color = Color.red;
			} else {
				button.image.color = Color.white;
			}
			i++;
		}
	}


	// Adds all colliding items to a HashSet for processing which is closest
	private void OnTriggerEnter (Collider collider)
	{
		InteractableBase collidedItem = collider.GetComponent<InteractableBase> ();
		if (collidedItem) {
			objectsHoveringOver.Add (collidedItem);
		}
	}

	// Remove all items no longer colliding with to avoid further processing
	private void OnTriggerExit (Collider collider)
	{
		InteractableBase collidedItem = collider.GetComponent<InteractableBase> ();
		if (collidedItem) {
			objectsHoveringOver.Remove (collidedItem);
		}
	}
}