using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Controller : MonoBehaviour {

    [SerializeField] private PlayerController player;

    [SerializeField] private Animator anim;
    //private float speed = 10.0f;
    //private float rotationSpeed = 100.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //float translation = Input.GetAxis("Vertical") * speed;
        //float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        //translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;
        //transform.Translate(0, 0, translation);
        //transform.Rotate(0, rotation, 0);

        if (player.bIsWalking == true)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
	}
}
