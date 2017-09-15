using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushButton : MonoBehaviour {

	Vector3 initialPosition;

	public GameObject button;

	void Start () {
		initialPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Button")
		{
			Debug.Log("Button was pushed !");

			transform.position = initialPosition;
			CreateOrder ();

			//Invoke ("desactivateButton", 0.2f);
			//Invoke ("activateButton", 1f);

		}
	}


	void activateButton (){
		button.GetComponent<BoxCollider> ().enabled = true;
	}

	void desactivateButton(){
		button.GetComponent<BoxCollider> ().enabled = false;
	}


	void CreateOrder(){
		GameObject.Find("CheckoutListener").GetComponent<checkoutListener> ().confirmOrder ();
	}


}
