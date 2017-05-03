using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkoutListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CartListener cart = GameObject.Find("CartListener").GetComponent<CartListener>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {


        Debug.Log("Something collided in checkout zone Checkout listener");


        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Checkout Activated");

        }
    }



    void OnTriggerExit(Collider other)
    {

        Debug.Log("Something Left the checkout zone Checkout listener");



        CartListener cart = GameObject.Find("CartListener").GetComponent<CartListener>();



        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Object removed");


            cart.clearCart();


        }
    }

}
