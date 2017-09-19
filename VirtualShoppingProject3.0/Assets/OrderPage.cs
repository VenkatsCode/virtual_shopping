using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPage : MonoBehaviour {

	public List<Transform> orderItems;
	public CartListener cart;

	int pageCount = 0;

	int productIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void previousPage(){
		if (pageCount > 0) {
			pageCount--;
			refreshList ();
		}
	}

	public void nextPage(){
		

		if ((pageCount+1)*6 < cart.productList.Count) {
			pageCount++;
			refreshList ();
		}

	}


	public void clearCart(){
		pageCount = 0;
		resetProductPosition ();
		cart.clearCart ();

		GameObject.Find ("ItemsInCart").GetComponent<TextMesh> ().text = "Items in Cart: " + cart.productList.Count;
		GameObject.Find("OrderTotal").GetComponent<TextMesh>().text = "Order Total: 0 $";
	}

	public void confirmOrder(){
		GameObject.Find("CheckoutListener").GetComponent<checkoutListener> ().confirmOrder ();
	}

	void resetProductPosition(){
	
		foreach (Product product in cart.productList)
		{
			product.transform.position = Vector3.zero;   
		}

		for (int i = 0; i < 6; i++) {
			orderItems [i].GetComponentInChildren<TextMesh> ().text = "";
		}
	
	}



	public void refreshList(){

		resetProductPosition ();


		float price = 0;
		foreach (Product prod in cart.productList)
		{
			price += prod.price;
		}
	

		GameObject.Find ("ItemsInCart").GetComponent<TextMesh> ().text = "Items in Cart: " + cart.productList.Count;
		GameObject.Find("OrderTotal").GetComponent<TextMesh>().text = "Order Total: "+price+" $";

		Debug.Log ("RefreshList");
		productIndex = pageCount * 6;
		Debug.Log ("pageCount" + pageCount);
		Debug.Log ("productIndex" + productIndex);
		//Debug.Log ("Item 0 "+cart.productList[0]);
		//Debug.Log ("Item 1 "+cart.productList[1]);
		int orderItemsIndex = 0;
		for (int i = productIndex; i <= (productIndex + 5); i++) {
			orderItems [orderItemsIndex].GetComponentInChildren<TextMesh> ().text = "";
			if (cart.productList.Count > i) {

				cart.productList [i].transform.position = orderItems [orderItemsIndex].position;
				orderItems [orderItemsIndex].GetComponentInChildren<TextMesh> ().text = cart.productList [i].productName;

				cart.productList [i].transform.localRotation = Quaternion.Euler (new Vector3 (0, -90, 0)); //Quaternion.identity; // = Quaternion.AngleVector3.zero;
				orderItemsIndex++;
			}


				//cart.productList [i].gameObject.AddComponent<BasketDetail>();
		}


	}
}
