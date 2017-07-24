using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScript : MonoBehaviour {

	// Use this for initialization
	public GameObject[] gameobjectArray;
	private Vector3[] gameobjectInitialPositionArray = new Vector3[100];
	private int score = 0;

	GameObject ProductText;

	void Start () {
		initialPosition ();


		ProductText = GameObject.FindGameObjectWithTag ("ProductText");

		Debug.Log("gameobjectArray: "+gameobjectArray.Length);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			calculateScore ();
			resetPosition ();
		}
			
	}

	public void initialPosition(){

		for(int i=0; i<gameobjectArray.Length; i++)
		{
			Debug.Log(i);
			Debug.Log("gameobjectArray: "+gameobjectArray [i].transform.position);
			gameobjectInitialPositionArray [i] = gameobjectArray [i].transform.position;
		}

	}

	public void resetPosition(){

		for(int i=0; i<gameobjectArray.Length; i++)
		{
			gameobjectArray [i].transform.position = gameobjectInitialPositionArray [i];
			gameobjectArray [i].transform.rotation = Quaternion.identity;
			//Vector3.zero;
		}

	}

	public void calculateScore(){

		for(int i=0; i<gameobjectArray.Length; i++)
		{
			//Debug.Log(gameobjectArray [i].transform.rotation.eulerAngles);

			if ((gameobjectArray [i].transform.position.x != gameobjectInitialPositionArray [i].x && 
				gameobjectArray [i].transform.position.z != gameobjectInitialPositionArray [i].z && 
				gameobjectArray [i].transform.position.y != gameobjectInitialPositionArray [i].y) &&
				(gameobjectArray [i].transform.rotation.eulerAngles.x >= 2f ||
				gameobjectArray [i].transform.rotation.eulerAngles.y >= 2f ||
					gameobjectArray [i].transform.rotation.eulerAngles.z >= 2f) 

			)
			{
				//Debug.Log("in if");
				score = score + 1;
			}
		}
		Debug.Log("score is: "+score);

		Invoke ("showScore",0f);
		Invoke ("hideScore",4f);

	}


	void showScore(){
		ProductText.GetComponent<TextMesh> ().text = "Your score is: " + score;
	}

	void hideScore(){
		ProductText.GetComponent<TextMesh> ().text = "";
		score = 0;
	}
}
