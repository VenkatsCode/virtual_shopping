using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class checkoutListener : MonoBehaviour {

    public string url = "localhost:9090/vr/order";
    public CartListener cart;
    public GameObject orderText;

    public GameObject productText;

    // Use this for initialization
    void Start () {
        cart = GameObject.Find("CartListener").GetComponent<CartListener>();
        orderText = GameObject.FindGameObjectWithTag("OrderText");

        productText = GameObject.FindGameObjectWithTag("ProductText");

        HideOrderText();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {


        Debug.Log("Something collided in checkout zone Checkout listener"+other.gameObject.tag);

        var productsMap = new Dictionary<long, long>();
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Checkout Activated");
            foreach(Product product in cart.productList){

                productsMap[product.id] = product.qty;

            }

            string productsList = "[{";
            foreach (KeyValuePair<long, long> entry in productsMap)
            {
                productsList += "\"id\":\"" + entry.Key + "\",";
                productsList += "\"value\":\"" + entry.Value + "\"";
                productsList += "},{";
            }
            productsList = productsList.Substring(0, productsList.Length - 2) + "]";

            Debug.Log("products is: " + productsList);

            WWWForm form = new WWWForm();
            form.AddField("productQuantities", productsList);

            Debug.Log("url:" + url);
            UnityWebRequest www = UnityWebRequest.Post("https://api.eu.yaas.io/vdkom/vrservicetrial/vrservicetrial/order", form);
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            // make a POST request with a retry policy for a 404
            StartCoroutine(F(www, form));

            foreach (Product product in cart.productList)
            {
               // Destroy(product.gameObject);
            }


			//Debug.Log (productsList.Length);
			//if (productsList.Length > 0) {
            	ShowOrderText();
			//}

            Invoke("HideOrderText", 2);

        }
    }

    IEnumerator F(UnityWebRequest www, WWWForm form)
    {
        www.downloadHandler = new DownloadHandlerBuffer();
        www.Send();

        while (!www.isDone)
        {
            Debug.Log(www.downloadProgress);
            yield return null;
        }

        if (www.isError)
        {
            Debug.Log(www.error + "," + www.responseCode);
        }
        else
        {
            

            if (www.downloadHandler.text != "") {
                Debug.Log("Errors: " + www.downloadHandler.text);
            }
            else { Debug.Log("Done"); }


            if (www.downloadHandler.text.Contains("404"))
            {


                UnityWebRequest retryWww = UnityWebRequest.Post("https://api.eu.yaas.io/vdkom/vrservicetrial/vrservicetrial/order", form);
                retryWww.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

                StartCoroutine(F(retryWww, form));
            }
           

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }

    }

    void ShowOrderText()
    {
         productText.GetComponent<TextMesh>().text = "Congratulation, the order has been placed successfully !";
    }


    void HideOrderText() {
        productText.GetComponent<TextMesh>().text = "";
    }


    void OnTriggerExit(Collider other)
    {

        Debug.Log("Something Left the checkout zone Checkout listener");

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Object removed");
            cart.clearCart();
        }
    }

}
