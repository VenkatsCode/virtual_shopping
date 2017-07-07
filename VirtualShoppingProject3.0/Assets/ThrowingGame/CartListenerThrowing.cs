using System;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartListenerThrowing : CartListener {







	//Product product
	public override void addToCart(GameObject productGO) {



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

	public override void RefreshCartText()
	{

		float price = 0;
		foreach (Product prod in productList)
		{
			price += prod.price;
		}
		orderText.GetComponent<TextMesh>().text = "Score: "+ productList.Count;
	}

	public override void clearCart() {
		productList.Clear();
		orderText.GetComponent<TextMesh>().text = "Score: 0";
	}





}





