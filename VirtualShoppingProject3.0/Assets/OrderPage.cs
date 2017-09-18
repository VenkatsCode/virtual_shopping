using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPage : MonoBehaviour {

	public List<Transform> orderItems;
	public CartListener cart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void refreshList(){
		Debug.Log ("RefreshList");

		Debug.Log ("Cart Count" + cart.productList.Count);

		//Debug.Log ("Item 0 "+cart.productList[0]);
		//Debug.Log ("Item 1 "+cart.productList[1]);

		for (int i = 0; i < cart.productList.Count; i++) {
			cart.productList [i].transform.position = orderItems [i].position;


			orderItems [i].GetComponentInChildren<TextMesh> ().text = cart.productList [i].productName;

			Debug.Log ("i: "+i);
			Debug.Log ("item: "+cart.productList [i]);
			Debug.Log (cart.productList [i].transform.position);


			cart.productList [i].transform.localRotation = Quaternion.Euler (new Vector3(0, -90, 0)); //Quaternion.identity; // = Quaternion.AngleVector3.zero;
			//cart.productList [i].gameObject.AddComponent<BasketDetail>();
		}


	}
}
