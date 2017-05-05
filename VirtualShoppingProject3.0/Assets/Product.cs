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
        cart = GameObject.Find("CartListener").GetComponent<CartListener>();


        productText = GameObject.FindGameObjectWithTag("ProductText");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {

      


        //Product prodTest = new Product(1,1);


       




        if (col.gameObject.name == "inside_cart")
        {

           
            cart.addToCart(gameObject);


            // product.gameObject.attachedRigidbody.useGravity = false;
            //transform.gameObject.GetComponent<Product>();

            

            productText.GetComponent<UnityEngine.UI.Text>().text = "Product "+ this.productName + " was added to the cart !";

            Invoke("hideProductText", 3);

            transform.SetParent(GameObject.Find("CartListener").transform);

            GetComponent<InteractionBehaviour>().enabled = false;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

            Debug.Log(GetComponent<Rigidbody>());


            Destroy(GetComponent<InteractionBehaviour>());
            Destroy(GetComponent<Rigidbody>());
            Debug.Log(GetComponent<Rigidbody>().useGravity);
            Debug.Log(GetComponent<Rigidbody>().isKinematic);

            transform.position = new Vector3(0, 2, 0);

            

        }
    }



    void hideProductText() {

        productText.GetComponent<UnityEngine.UI.Text>().text = "";
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
