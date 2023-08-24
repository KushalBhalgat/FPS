using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;

public class PlayerMovementController : NetworkBehaviour
{

    [SerializeField]
    private Camera playerCam;
    //private Camera mainCam;
    private CharacterController controller;
    [SerializeField]
    private Transform groundCheck;
    private Vector3 storeGroundCheckerPosition;
    public bool isGrounded;
    [SerializeField]
    private bool onRamp;
    public LayerMask groundMask;
    public LayerMask rampMask;
    private float gravity = -9.81f;
    [SerializeField]
    private float gravityScale = 5f;
    private float keyX, keyZ;
    private Vector3 moveVector;
    private bool walk;
    

    //Initialize Mouse Inputs
    private float MouseX;
    private float MouseY;
    [SerializeField]
    private float mouseSensitivity;
    private float verticalRotator;
    [SerializeField]


    private float storePlayerSpeed;
    private float playerSpeed;
    [SerializeField]
    private Vector3 playerYvel;

    //Jump Mechanics
    [SerializeField]
    private float jumpHeight;

    private void Start()
    {
        if (!IsOwner) {
            playerCam.gameObject.GetComponent<Camera>().enabled = false;
            playerCam.gameObject.GetComponent<AudioListener>().enabled = false;
            return;}
        if(Camera.main != null){Camera.main.gameObject.SetActive(false);}
        /*
        if (Camera.main) {Camera.main.gameObject.SetActive(false);}
        foreach (Camera c in Camera.allCameras)
        {
            if (c.tag == "playerCam") { playerCam = c;playerCam.gameObject.SetActive(true); }
        }
        */
        onRamp = false;
        jumpHeight = 7f;
        storePlayerSpeed = 30f;
        playerSpeed = 200f;
        walk = false;
        verticalRotator = 90f;
        mouseSensitivity = 60f;
        isGrounded = false;
        //groundCheck = GameObject.FindWithTag("groundChecker").GetComponent<Transform>();
        gravity = -9.81f*gravityScale;
        controller = this.gameObject.GetComponent<CharacterController>();
        //if (GameObject.FindWithTag("crosshair") == null) { Destroy(GameObject.FindWithTag("NetworkUI").gameObject); }

        if (GameObject.FindWithTag("NetworkUI") != null) { Destroy(GameObject.FindWithTag("NetworkUI").gameObject); }
        //GameObject.FindWithTag("NetworkUI").GetComponent<NetworkObject>().Spawn(false);
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update() {
        if (!IsOwner) { this.gameObject.GetComponent<PlayerMovementController>().enabled = false; return; }
        //Query Ground Distance
        /*
        storeGroundCheckerPosition = transform.position;
        storeGroundCheckerPosition.y -= 1f*this.transform.localScale.y;
        groundCheck.position=storeGroundCheckerPosition;
        */
        isGrounded = Physics.CheckSphere(groundCheck.position,0.2f,groundMask);
        onRamp = Physics.CheckSphere(groundCheck.position, 0.4f, rampMask);

        //GetMovementInput
        keyX = Input.GetAxis("Horizontal");
        keyZ = Input.GetAxis("Vertical");
        walk = Input.GetKey(KeyCode.LeftShift);
        if (walk) { playerSpeed = storePlayerSpeed / 2f; } else { playerSpeed = storePlayerSpeed; }
        moveVector = (keyX * transform.right + keyZ * transform.forward) * playerSpeed * Time.deltaTime;
        controller.Move(moveVector);

        //GetMouseInput
        MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Set Player and Camera rotations
        this.transform.Rotate(MouseX * Vector3.up);
        verticalRotator -= MouseY;
        verticalRotator = Mathf.Clamp(verticalRotator, -90f, 90f);
        playerCam.transform.localRotation = Quaternion.Euler(verticalRotator * Vector3.right);

        //Jump Mechanism
        //Apply gravity manually
        if (Input.GetButtonDown("Jump") && ((isGrounded)||(onRamp))){
            Debug.Log("jumped");
            playerYvel.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
        else if(isGrounded || onRamp){
            playerYvel.y = -2f;
        }
        else if (!isGrounded) { 
            playerYvel.x = 0; playerYvel.z = 0;
            playerYvel.y+=gravity * Time.deltaTime;
        }
        controller.Move(playerYvel * Time.deltaTime );
    }
    private void FixedUpdate()
    {

    }
}
