using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartListener : MonoBehaviour {



    public List<Product> productList;

    // Use this for initialization
    void Start () {
        productList = new List<Product>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Product product
    public void addToCart(GameObject productGO) {



        Product product = productGO.GetComponent<Product>();

        Debug.Log("Adding a product");


        Debug.Log("Adding a product"+product.id);


        if (productList.Contains(product))
        {
            Debug.Log("Product already exist");

        }
        else {
            Debug.Log("Adding a product");
            productList.Add(product);
        }

        


    }

    public void clearCart() {
        productList.Clear();
    }




    void OnCollisionEnter(Collision col)
    {

        Debug.Log("Cart collided in checkout zone Cart Listener");


        //Product prodTest = new Product(1,1);


        // CartListener cart = GameObject.Find("CartListener").GetComponent<CartListener>();

        // cart.addToCart(this);

        //transform.gameObject.GetComponent<Product>();

        /* if (col.gameObject.layer == 10)
         {

             Debug.Log("Object Added to cart");


         }*/
    }


}




