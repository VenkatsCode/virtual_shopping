using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour {


    public CartListener cart;


    GameObject productText;

	GameObject productDetailText;




	Material highlightMat;

    public string id = "2";

    public string productName;

	public string productDescription;

    public float price;

    public long qty = 1;

    public Product(string id, long qty)
    {

        this.id = id;
        this.qty = qty;

    }


    // Use this for initialization
    void Start () {
        cart = GameObject.Find("CartListener").GetComponent<CartListener>();

		highlightMat = Resources.Load("Material/outlined.mat", typeof(Material)) as Material;

        productText = GameObject.FindGameObjectWithTag("ProductText");
		productDetailText = GameObject.FindGameObjectWithTag("ProductDetailText");


	

    }
	
	// Update is called once per frame
	void Update () {
		
	}





	public void Highlight(){
		productDetailText.GetComponent<TextMesh>().text = "Product " + this.productName + "\n Price:"+this.price+"$";
	}





	public void removeHighlight(){
		productDetailText.GetComponent<TextMesh>().text = "";
	}


    void OnCollisionEnter(Collision col)
    {


  


        if (col.gameObject.name == "inside_cart")
        {


            cart.addToCart(gameObject);



			productText.GetComponent<TextMesh>().text = "Product " + this.productName + "\n was added to the cart !";
			cart.productDetailPanel.SetActive (true);


            Invoke("hideProductText", 1);

            transform.SetParent(GameObject.FindGameObjectWithTag("Cart").transform);

            transform.SetParent(GameObject.Find("CartListener").transform);



            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

           

			transform.position = new Vector3(0, 2, 0);

      

           





        }




    }



    void hideProductText() {

        productText.GetComponent<TextMesh>().text = "";
		cart.productDetailPanel.SetActive (false);
    }

	void hideProductDetailText() {

		productDetailText.GetComponent<TextMesh>().text = "";
	}

  


}
