using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepgrounded : MonoBehaviour {
    private bool isGrounded;
    private CharacterController controller;
    private int verticalVelosity;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {

            isGrounded = controller.isGrounded ;

            if (isGrounded)
            {
                verticalVelosity -= 0;
            }
            else
            {
                verticalVelosity -= 1;
            }

        Vector3 moveVector = new Vector3(0, verticalVelosity, 0);
        controller.Move(moveVector);
    }
}
