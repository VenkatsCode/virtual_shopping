using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartListener : MonoBehaviour {


	public GameObject orderText;


    public List<Product> productList;

    // Use this for initialization
    void Start () {
        productList = new List<Product>();
        orderText = GameObject.FindGameObjectWithTag("OrderText");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Product product
	public virtual void addToCart(GameObject productGO) {



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


            RefreshCartText();


        }

        


    }

	public virtual void RefreshCartText()
    {

        float price = 0;
        foreach (Product prod in productList)
        {
            price += prod.price;
        }
			
		orderText.GetComponent<TextMesh>().text = "Items in cart: "+ productList.Count+ " \n Total cost: "+price+"$";
    }

	public virtual void clearCart() {
        productList.Clear();
		orderText.GetComponent<TextMesh>().text = "Items in cart: 0 \n Total cost: 0$";
    }





}




