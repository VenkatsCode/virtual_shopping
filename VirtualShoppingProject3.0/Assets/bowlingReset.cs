using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlingReset : MonoBehaviour {
	public gameScript game;
	// Use this for initialization

	public float startCounter = 0;

	public Vector3 initialPos;

	void Start () {
		startCounter = 0;
		initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {



		if (transform.localPosition.x < 89) {
		
			startCounter += Time.deltaTime;
			
		
		}

		if (startCounter > 5) {

			//Debug.Log (startCounter);
			Debug.Log (startCounter);


			game.calculateScore();



			game.resetPosition ();
			transform.position = initialPos;
			startCounter = 0f;
		}
		//if(startCounter - Time.deltaTime ()

	}



}
