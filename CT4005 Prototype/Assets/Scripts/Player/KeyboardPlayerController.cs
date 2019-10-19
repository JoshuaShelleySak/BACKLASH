using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Same as gunController but for keyboard and mouse. Outdated and now unused. Was previously used for testing.
/// </summary>
public class KeyboardPlayerController : MonoBehaviour {

    [SerializeField] private float fPlayerSpeed;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 lookDirection;

    // Dash Variables
    const float dashCooldown = 0.5f;
    private const float maxDashTime = 1.0f;
    private float dashDistance = 0.2f;
    private float dashStoppingSpeed = 0.1f;
    private float currentDashCooldown = dashCooldown;
    float currentDashTime = maxDashTime;


    CharacterController controller;

    [SerializeField]
    private KeyboardGunController theGun;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        moveInput = new Vector3();
        currentDashCooldown -= Time.deltaTime;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            lookDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookDirection);
        }

        if (Input.GetKey("w"))
        {
            moveInput += Vector3.forward;
        }
        if (Input.GetKey("a"))
        {
            moveInput += Vector3.left;
        }
        if (Input.GetKey("d"))
        {
            moveInput += Vector3.right;
        }
        if (Input.GetKey("s"))
        {
            moveInput += Vector3.back;
        }
        moveVelocity = moveInput * fPlayerSpeed;


        if (Input.GetKeyDown("space"))
        {
            theGun.bIsFiring = true;
        }
        if (Input.GetKeyUp("space"))
        {
            theGun.bIsFiring = false;
        }

        if (Input.GetMouseButtonDown(0) && currentDashCooldown < 0)
        {
            //print("Button Down");
            currentDashTime = 0;
            currentDashCooldown = dashCooldown;
        }
        if (currentDashTime < maxDashTime)
        {
            //print("Dash Time is Less");
            this.transform.position += transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
    }

    // FixedUpdate is called always at the same time - consistent.
    private void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }
}
