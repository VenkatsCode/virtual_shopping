using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {


    public CartListener cart;


    GameObject productText;

    public long id = 2;

    public string productName;

    public float price;

    public long qty = 1;

    public Product(long id, long qty)
    {

        this.id = id;
        this.qty = qty;

    }


    // Use this for initialization
    void Start () {
        cart = GameObject.Find("CartListener").GetComponent<CartListenerThrowing>();


        productText = GameObject.FindGameObjectWithTag("ProductText");
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger happened"+ col.gameObject.name);



    }

    void OnCollisionEnter(Collision col)
    {


        Debug.Log("Collision happened"+ col.gameObject.name);

        //Product prodTest = new Product(1,1);



        if (col.gameObject.name == "inside_cart")
        {


            cart.addToCart(gameObject);


            // product.gameObject.attachedRigidbody.useGravity = false;
            //transform.gameObject.GetComponent<Product>();



            productText.GetComponent<TextMesh>().text = "Product " + this.productName + " was added to the cart !";

            Invoke("hideProductText", 3);

            transform.SetParent(GameObject.FindGameObjectWithTag("Cart").transform);

            transform.SetParent(GameObject.Find("CartListener").transform);

            // GetComponent<InteractionBehaviour>().enabled = false;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

            Debug.Log(GetComponent<Rigidbody>());

            transform.position = new Vector3(0, 2, 0);

            Destroy(GetComponent<Rigidbody>());

            Debug.Log(GetComponent<Rigidbody>().isKinematic);





        }




    }



    void hideProductText() {

        productText.GetComponent<TextMesh>().text = "";
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
