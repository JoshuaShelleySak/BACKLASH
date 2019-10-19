using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player movement and direction
/// </summary>
public class PlayerController : MonoBehaviour {

    //Speed of the player
    [SerializeField] private float fPlayerSpeed;
    [SerializeField] private int controllerPrefix = 0;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    //If they're currently moving, used for animations
    public bool bIsWalking = false;

    // Dash Variables
    const float dashCooldown = 0.5f;
    private const float maxDashTime = 1.0f;
    private float dashDistance = 0.2f;
    private float dashStoppingSpeed = 0.1f;
    private float currentDashCooldown = dashCooldown;
    float currentDashTime = maxDashTime;

    [SerializeField]
    private GunController theGun;

	// Use this for initialization
	void Start () {
        //Gets rigidbody of the player
        myRigidbody = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
        //Gets input from the controller relative to the player
        moveInput = new Vector3(Input.GetAxis("P" + controllerPrefix + "Horizontal"), 0f, Input.GetAxis("P" + controllerPrefix + "Vertical"));
        moveVelocity = moveInput * fPlayerSpeed;
        currentDashCooldown -= Time.deltaTime;
        
        // Rotate with controller
        Vector3 playerDirection = Vector3.right * Input.GetAxis("P" + controllerPrefix + "RightHorizontal") + Vector3.forward * Input.GetAxis("P" + controllerPrefix + "RightVertical"); // - because .forward is actually down.
        //Changes the look direction based on rotation
        if(playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }
        //If the fire button (Right trigger) for this controller is pressed, set the guncontroller variable "isFiring" to true
        if (Input.GetButtonDown("P" + controllerPrefix + "Fire1"))
        {
            theGun.bIsFiring = true;
        }
        if (Input.GetButtonUp("P" + controllerPrefix + "Fire1"))
        {
            theGun.bIsFiring = false;
        }

        //If the dodge/dash button (left trigger) for this controller is pressed and the dashcooldown is less than 0
        if (Input.GetButtonDown("P" + controllerPrefix + "Dodge") && currentDashCooldown < 0)
        {
            //Set dash time to 0 
            currentDashTime = 0;
            //Starts cooldown until next dash
            currentDashCooldown = dashCooldown;
        }
        //While dashTime is less than max, move the player in the current look direction at the dashing speed
        if (currentDashTime < maxDashTime)
        {
            this.transform.position += transform.forward * dashDistance;
            //Add the stopping speed to the time
            currentDashTime += dashStoppingSpeed;
        }

        //If the velocity = 0, the player is no longer walking
        if (moveVelocity != Vector3.zero)
        {
            bIsWalking = true;
        }
        else
        {
            bIsWalking = false;
        }

    }

    // FixedUpdate is called always at the same time - consistent.
    private void FixedUpdate()
    {
        //Changes the player velocity to the current move
        myRigidbody.velocity = moveVelocity;
    }

    //Gets the current playerControllerPrefix
    public int GetPlayerControllerPrefix()
    {
        return controllerPrefix;
    }
}
