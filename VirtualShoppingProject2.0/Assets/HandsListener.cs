using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Hand Listener");

        Debug.Log(other.gameObject.name);

        Debug.Log(other.gameObject.layer);

        if (other.gameObject.layer == 10)
        {
            other.gameObject.transform.SetParent(this.gameObject.transform);


            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;


            //other.gameObject.transform.localPosition = new Vector3(1.25f,0,0);
        }

        
    }


}
