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
    public void addToCart(Product product) {

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




}




