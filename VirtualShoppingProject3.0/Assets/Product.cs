using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {


    public CartListener cart;

    public long id = 2;
    public long qty = 1;

    public Product(long id, long qty)
    {

        this.id = id;
        this.qty = qty;

    }


    // Use this for initialization
    void Start () {
        cart = GameObject.Find("CartListener").GetComponent<CartListener>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {

        Debug.Log("Something collided");


        //Product prodTest = new Product(1,1);


       



        Debug.Log(col.gameObject.name);

        Debug.Log(col.gameObject.layer);

        if (col.gameObject.name == "inside_cart")
        {

            Debug.Log("Object Added to cart");
            cart.addToCart(gameObject);


            // product.gameObject.attachedRigidbody.useGravity = false;
            //transform.gameObject.GetComponent<Product>();

            transform.SetParent(GameObject.Find("CartListener").transform);

            GetComponent<Rigidbody>().isKinematic = true;

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
