using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionListener : MonoBehaviour {


	public ControllerLaserSelector laserSelector;

	int menuMode = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//controllerListener triggerup and highlighted IS confirm order


	void ConfirmOrder(){
		GameObject.Find("CheckoutListener").GetComponent<checkoutListener> ().confirmOrder ();
	}




	public void triggerUp(){
		/*if (menuMode == 1 && ) {
			ControllerLaserSelector
		}*/


		if (laserSelector.Target.tag == "ConfirmOrderBtn") {
			ConfirmOrder ();
		}
	}

	public void triggerDown(){
	
	}
}
