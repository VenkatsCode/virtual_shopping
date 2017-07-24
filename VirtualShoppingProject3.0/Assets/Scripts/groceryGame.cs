using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groceryGame : MonoBehaviour {



	GameObject timerText;
	CartListener cart;
	bool counting = false;

	float counter = 0;


	float counterTime = 15f;

	// Use this for initialization
	void Start () {
		cart = GameObject.Find("CartListener").GetComponent<CartListener>();
		timerText = GameObject.FindGameObjectWithTag ("TimerText");
	}
	
	// Update is called once per frame
	void Update () {

		if (counting) {
		
			counter += Time.deltaTime;
			timerText.GetComponent<TextMesh> ().text = "Timer: " + Mathf.Round((counterTime - counter)*10)/10 + "seconds left";
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
	
	}

	void resetBasket(){
		cart.clearCart();
	}
}
