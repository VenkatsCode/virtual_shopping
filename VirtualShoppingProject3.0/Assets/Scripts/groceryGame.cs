using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groceryGame : MonoBehaviour {



	GameObject timerText;
	GameObject OrderText;
	CartListener cart;
	bool counting = false;

	float counter = 0;


	float counterTime = 15f;

	// Use this for initialization
	void Start () {
		cart = GameObject.Find("CartListener").GetComponent<CartListener>();
		timerText = GameObject.FindGameObjectWithTag ("TimerText");
		OrderText = GameObject.FindGameObjectWithTag ("OrderText");
	}
	
	// Update is called once per frame
	void Update () {

		if (counting) {
			counter += Time.deltaTime;
			timerText.GetComponent<TextMesh> ().text = "Timer: " + Mathf.Round((counterTime - counter)*100)/100 + "seconds left";
			if (counter > counterTime) {
				counting = false;
				counter = 0;
				printScore ();
				resetBasket();
			}
		}


	}


	public void startGame(){
		cart.clearCart();
		counting = true;
	}


	void printScore(){
		timerText.GetComponent<TextMesh> ().text = OrderText.GetComponent<TextMesh> ().text; 
		Invoke ("hideScore", 8f);
	}

	void hideScore(){
	
		timerText.GetComponent<TextMesh> ().text = "";
	}

	void resetBasket(){
		cart.clearCart();
	}
}
