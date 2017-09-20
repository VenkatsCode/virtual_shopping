using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class ControllerLaserSelector : MonoBehaviour {


	int VR_UI_LAYER_MASK = 1 << 10;
	public GameObject target;
	public Material selected;
	public Material unselected;
	Material[] mats;
	// Use this for initialization
	void Start () {
		
	}





	void Update () {

		RaycastHit raycastHit;
		GameObject gameObject = null;
		if(Physics.Raycast(transform.position, transform.forward, out raycastHit, Mathf.Infinity, VR_UI_LAYER_MASK))
		{
			gameObject = raycastHit.collider.gameObject;


			Debug.Log (gameObject.layer);



			if(target != null){
				unselectItem (target);
			}




				target = gameObject;

		
			if(target != null){
				selectItem (target);
			}

				


			Debug.Log ("Object was selected");


				Debug.Log (gameObject);


		}
	}


	void selectItem(GameObject item){
		mats = target.GetComponent<Renderer>().materials;
		mats[0] = selected;
		item.GetComponent<Renderer>().materials = mats;



		if (item.name.StartsWith("cartItem")) {

		

			Product prod = GameObject.Find ("Items").GetComponent<OrderPage> ().getProductAtPos (int.Parse(item.name.Substring(item.name.Length - 1,1)));

			if (prod != null) {
				GameObject.Find ("ItemDescription").GetComponent<TextMesh> ().text = ResolveTextSize(prod.productName + ": " + prod.productDescription.ToString(),30);
			}

		}




	}

	void unselectItem(GameObject item){
		mats = target.GetComponent<Renderer>().materials;
		mats[0] = unselected;
		item.GetComponent<Renderer>().materials = mats;
	}

	public GameObject Target {
		get {
			return this.target;
		}
		set {
			target = value;
		}
	}



	private string ResolveTextSize(string inputt, int lineLength){
		int currentCharLine = 0;
		StringBuilder input = new StringBuilder(inputt);
		for( int i = 0; i < input.Length; i++){
			currentCharLine++;
			if(currentCharLine>=lineLength){
				if(input[i]==' '){
					input[i]= Convert.ToChar("\n"); 
					currentCharLine = 0;
				}//end if
			}//end if
		}//end for 
		return input.ToString();
	}






}
