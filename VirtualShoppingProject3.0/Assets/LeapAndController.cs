using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapAndController : MonoBehaviour {

    GameObject player;

    GameObject mainCamera;

    GameObject LMHeadMount;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("FPSController");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        LMHeadMount = GameObject.FindGameObjectWithTag("LMHeadMount");

    }
	
	// Update is called once per frame
	void Update () {
        player.transform.rotation = mainCamera.transform.rotation;
        LMHeadMount.transform.position = player.transform.position;
    }
}
