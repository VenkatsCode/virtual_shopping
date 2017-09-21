﻿using UnityEngine;
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


	public Material highlightMat;



	//public GameObject menuGO;



	public TeleportVive teleportVive;

	public ActionListener actionListener; 

	groceryGame grocery;


	public UnityEngine.UI.Button[] buttons;


	public int activeMenuButton = 0;

	// Use this for initialization
	void Start ()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		grocery = GameObject.Find ("GroceryGame").GetComponent<groceryGame> ();

	//	buttons = menuGO.GetComponentsInChildren<UnityEngine.UI.Button> ();


		//actionListener = GameObject.Find ("Menu").GetComponent<ActionListener> ();
			
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

			if (closestItem != null) {
				
			
			
			} else {
			
			}

			if (interactingItem) {
				RumbleController (0.05f, 10);
				//controller.TriggerHapticPulse highlightMat

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
		}

		if (controller.GetPressUp (triggerButton) && interactingItem != null) {
			RumbleController (0.05f, 10);
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
		}


	}


	void highlightController(){
		GetComponentInChildren
		Material[] mats = GetComponent<Renderer> ().materials;
		mats [0] = highlightMat;
		GetComponent<Renderer> ().materials = mats;
	
	}

	void RumbleController( float duration, float strength )
	{
		StartCoroutine( RumbleControllerRoutine( duration, strength ) );
	}

	IEnumerator RumbleControllerRoutine( float duration, float strength )
	{
		strength = Mathf.Clamp01( strength );
		float startTime = Time.realtimeSinceStartup;

		while( Time.realtimeSinceStartup - startTime <= duration )
		{
			int valveStrength = Mathf.RoundToInt( Mathf.Lerp( 0, 3999, strength ) );

			controller.TriggerHapticPulse( (ushort)valveStrength );

			yield return null;
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