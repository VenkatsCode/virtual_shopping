using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class checkoutListener : MonoBehaviour
{

	public string url = "localhost:9090/vr/order";
	public CartListener cart;
	public GameObject productText;
	public OrderPage orderPage;

	// Use this for initialization
	void Start ()
	{
		cart = GameObject.Find("CartListener").GetComponent<CartListener>();
   

        productText = GameObject.FindGameObjectWithTag("ProductText");




		//HideOrderText ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}



	public void confirmOrder ()
	{
	

	

		var productsMap = new Dictionary<string, long> ();

		Debug.Log ("Checkout Activated");
		foreach (Product product in cart.productList) {

			productsMap [product.id] = product.qty;

		}




		string productsList = "{" +
		                      "\"owner\": \"" + "virtualshop@sap.com" + "\"," +
		                      "\"market\": {" +
		                      "\"marketId\": \"" + "A1\"" +
		                      "}," +
		                      "\"customer\": {" +
		                      "\"customerNumber\": \"" + "833950386\"" + "}," +
		                      "\"description\": \"" + "sample order\"," +
		                      "\"orderItems\":[{";


		//string productsList = "[{";
		foreach (KeyValuePair<string, long> entry in productsMap) {
			productsList += "\"itemType\":\"" + "subscriptionItem" + "\",";
			productsList += "\"product\":{" + "\"id\": \"" + entry.Key + "\"},";
			productsList += "\"quantity\":{" + "\"value\": \"" + entry.Value + "\"}";
			productsList += "},{";
		}
		productsList = productsList.Substring (0, productsList.Length - 2) + "]";
		productsList += "}";
		Debug.Log ("products is: " + productsList);

		WWWForm form = new WWWForm ();
		//form.AddField("", productsList);

		form.AddField ("owner", "virtualshop@sap.com");
		form.AddField ("market", "{\"marketId\": \"" + "A1\"}");
		form.AddField ("marketId", productsList);
		form.AddField ("productQuantities", productsList);

		//byte []bytes = System.Text.Encoding.UTF8.GetBytes(productsList);




		Debug.Log ("url:" + url);

		/*UnityWebRequest www = UnityWebRequest.Post("https://order-service-ngom-test.cfapps.us10.hana.ondemand.com/s2s/v1/v1/orders", JsonUtility.ToJson (productsList));
		www.SetRequestHeader("Content-Type", "application/json");
		www.SetRequestHeader("Authorization", "Basic TmpsVGsxSExoODFpTG5rNGJNZEo5Vmo1OjJrZG13cEZtNXhJRmVSMTNkb3NDOVo4cw==");
		www.SetRequestHeader("hybris-tenant", "revcdevcfint");
		www.SetRequestHeader("hybris-user", "revcdevcfint");*/
		// make a POST request with a retry policy for a 404
//		StartCoroutine(F(www));

		Dictionary<string, string> headers = new Dictionary<string, string> ();

		headers.Add ("Content-Type", "application/json");
		headers.Add ("Authorization", "Basic TmpsVGsxSExoODFpTG5rNGJNZEo5Vmo1OjJrZG13cEZtNXhJRmVSMTNkb3NDOVo4cw==");
		headers.Add ("hybris-tenant", "revcdevcfint");
		headers.Add ("hybris-user", "revcdevcfint");


		byte[] body = System.Text.Encoding.UTF8.GetBytes (productsList);
		UnityEngine.WWW www = new UnityEngine.WWW("https://order-service-ngom-test.cfapps.us10.hana.ondemand.com/s2s/v1/v1/orders", body, headers);
		StartCoroutine(WaitForRequest(www));
		Debug.Log ("ORDER PLACED !");
		Debug.Log (productsList);




	
		ShowOrderText ();


		Invoke ("HideOrderText", 2);


	}




	IEnumerator WaitForRequest (WWW www)
	{
		yield return www;

		// empty if http status cod 500
		Debug.Log ("WWW: " + www.text);

	}





	IEnumerator F (UnityWebRequest www)
	{
		www.downloadHandler = new DownloadHandlerBuffer ();
		www.Send ();

		while (!www.isDone) {
			Debug.Log (www.downloadProgress);
			yield return null;
		}

		if (www.isError) {
			Debug.Log (www.error + "," + www.responseCode);
		} else {
            

			if (www.downloadHandler.text != "") {
				Debug.Log ("Errors: " + www.downloadHandler.text);
			} else {
				Debug.Log ("Done");
			}

			/*
            if (www.downloadHandler.text.Contains("404"))
            {

			

				UnityWebRequest retryWww = UnityWebRequest.Post("https://order-service-ngom-test.cfapps.us10.hana.ondemand.com/s2s/v1/v1/orders", form);
				www.SetRequestHeader("Content-Type", "application/json");
				www.SetRequestHeader("Authorization", "Basic TmpsVGsxSExoODFpTG5rNGJNZEo5Vmo1OjJrZG13cEZtNXhJRmVSMTNkb3NDOVo4cw==");
				www.SetRequestHeader("hybris-tenant", "revcdevcfint");
				www.SetRequestHeader("hybris-user", "revcdevcfint");

                StartCoroutine(F(retryWww, form));
            }
           */

			// Or retrieve results as binary data
			byte[] results = www.downloadHandler.data;
		}

	}

	void ShowOrderText ()
	{
		productText.GetComponent<TextMesh> ().text = "Congratulation, the order has been placed successfully !";
	}


	void HideOrderText ()
	{
		productText.GetComponent<TextMesh> ().text = "";
	}




}
