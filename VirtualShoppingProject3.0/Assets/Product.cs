using Leap.Unity.Interaction;
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


    void OnTriggerEnter(Collider col)
    {
      /*  Debug.Log("Trigger happened"+ col.gameObject.name);
		productDetailText.GetComponent<TextMesh>().text = "Product " + this.productName + "\n Price:"+this.price+"$";
		Invoke("hideProductDetailText", 6);*/

    }


	public void Highlight(){
		productDetailText.GetComponent<TextMesh>().text = "Product " + this.productName + "\n Price:"+this.price+"$";
	}





	public void removeHighlight(){
		productDetailText.GetComponent<TextMesh>().text = "";
	}


    void OnCollisionEnter(Collision col)
    {


    

        //Product prodTest = new Product(1,1);



        if (col.gameObject.name == "inside_cart")
        {


            cart.addToCart(gameObject);


            // product.gameObject.attachedRigidbody.useGravity = false;
            //transform.gameObject.GetComponent<Product>();



			productText.GetComponent<TextMesh>().text = "Product " + this.productName + "\n was added to the cart !";

            Invoke("hideProductText", 6);

            transform.SetParent(GameObject.FindGameObjectWithTag("Cart").transform);

            transform.SetParent(GameObject.Find("CartListener").transform);

            // GetComponent<InteractionBehaviour>().enabled = false;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

           

            //transform.position = new Vector3(0, 2, 0);

           // Destroy(GetComponent<Rigidbody>());

           





        }




    }



    void hideProductText() {

        productText.GetComponent<TextMesh>().text = "";
    }

	void hideProductDetailText() {

		productDetailText.GetComponent<TextMesh>().text = "";
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
