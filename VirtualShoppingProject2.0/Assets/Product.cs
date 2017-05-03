using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {
   
    public long id = 2;
    public long qty = 1;

    public Product(long id, long qty)
    {

        this.id = id;
        this.qty = qty;

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {

        Debug.Log("Something collided");


        //Product prodTest = new Product(1,1);


        CartListener cart = GameObject.Find("CartListener").GetComponent<CartListener>();

        cart.addToCart(gameObject);


        // product.gameObject.attachedRigidbody.useGravity = false;
        //transform.gameObject.GetComponent<Product>();

        transform.SetParent(GameObject.Find("CartListener").transform);

        GetComponent<Rigidbody>().isKinematic = true;

        if (col.gameObject.layer == 10)
        {

            Debug.Log("Object Added to cart");

         
        }
    }

    /*
    void OnCollisionStay(Collision col)
    {

        Debug.Log("Something collided");


        Destroy(transform.gameObject);

        if (col.gameObject.layer == 10)
        {

            Debug.Log("Object Added to cart");

          
        }
    }*/


}
