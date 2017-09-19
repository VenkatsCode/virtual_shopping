using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionListener : MonoBehaviour {


	public ControllerLaserSelector laserSelector;


	public Transform menuPlayerPosition;

	Vector3 playerPositionInStore;

	Vector3 playerRotationInStore;


	public OrderPage orderPage;

	bool isMenuActive = false;

	int menuMode = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//controllerListener triggerup and highlighted IS confirm order






	public void triggerUp(){
		/*if (menuMode == 1 && ) {
			ControllerLaserSelector
		}*/
		if (isMenuActive && laserSelector.Target != null) {

			if (laserSelector.Target.tag == "ConfirmOrderBtn") {
				orderPage.confirmOrder ();
			}

			if (laserSelector.Target.tag == "ClearCartBtn") {
				orderPage.clearCart ();
			}

			if (laserSelector.Target.tag == "NextPageBtn") {
				orderPage.nextPage ();
			}

			if (laserSelector.Target.tag == "PreviousPageBtn") {
				orderPage.previousPage ();
			}

		}
	}

	public void triggerDown(){
	
	}


	public void triggerMenu(){
	
		if (isMenuActive) {
			GameObject.FindGameObjectWithTag ("Player").transform.position = playerPositionInStore;
			GameObject.FindGameObjectWithTag ("Player").transform.rotation  = Quaternion.Euler (playerRotationInStore);
			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<SteamVR_LaserPointer> ().thickness = 0f;
			isMenuActive = false;
		} else {
			playerPositionInStore = GameObject.FindGameObjectWithTag ("Player").transform.position;
			playerRotationInStore = GameObject.FindGameObjectWithTag ("Player").transform.rotation.eulerAngles;
			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<SteamVR_LaserPointer> ().thickness = 0.1f;
			GameObject.FindGameObjectWithTag ("Player").transform.position = menuPlayerPosition.position;
			GameObject.FindGameObjectWithTag ("Player").transform.rotation = Quaternion.Euler (new Vector3(0,90,0));

			isMenuActive = true;
		}
	
	}


}
