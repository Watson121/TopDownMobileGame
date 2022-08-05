using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // Player Control Inputs
    private PlayerInput playerInput;
    private PlayerControls playerControls;

    private Vector3 playerPostion;
    private Vector3 playerRotation;
    private bool move = false;
    private float veritcalScreenOffset = 4.0f;
    private float horizontalScreenOffset = 2.0f;

    [Header("Player Settings")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerRotationSpeed = 3.0f;
    [SerializeField] private float horitontalRotation = 15.0f;
    [SerializeField] private float verticalRotation = 15.0f;

    // Player Camera & Viewport Bounds
    private Camera playerCamera;
    private float frustumHeight;
    private float frustumWidth;
    private float cameraDistance = 3.0f;
   

    private void Awake()
    {
        CalculatingViewportBounds();
        ControlSetup();
    }

    // Setting up the controls
    private void ControlSetup()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Player.Movement.performed += OnMovementAction;
        playerControls.Player.Movement.canceled += OnMovementAction;
    }


    private void CalculatingViewportBounds()
    {
        playerCamera = Camera.main;

        // Setting the Viewport Bounds
        cameraDistance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        frustumHeight = (2.0f * cameraDistance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad)) / 2;
        frustumWidth = (frustumHeight * Camera.main.aspect);

        // Setting Camera Location
        playerCamera.transform.position = new Vector3(0, frustumHeight / 2, playerCamera.transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Movement();
        }   
    }

    private void OnMovementAction(InputAction.CallbackContext obj)
    {

        Debug.Log(obj.phase);

        if (obj.performed)
        {
            move = true;
        }
        
        if (obj.canceled)
        {
            move = false;
           
        }
    }

    private void Movement()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();

        // Setting Player Position
        playerPostion = transform.position;
        playerPostion += new Vector3(inputVector.x * playerSpeed * Time.deltaTime, inputVector.y * playerSpeed * Time.deltaTime, 0);
        playerPostion.x = Mathf.Clamp(playerPostion.x , -frustumWidth + horizontalScreenOffset, frustumWidth - horizontalScreenOffset);
        playerPostion.y = Mathf.Clamp(playerPostion.y, -frustumHeight + 1.0f, frustumHeight - veritcalScreenOffset);

        // Setting Player Rotation
        playerRotation += new Vector3(inputVector.y * 10f * Time.deltaTime, 0, inputVector.x * 10f * Time.deltaTime);
        playerRotation.z = Mathf.Clamp(playerRotation.z, -horitontalRotation, horitontalRotation);
        playerRotation.x = Mathf.Clamp(playerRotation.x, -verticalRotation, verticalRotation);

        transform.position = playerPostion;
        transform.rotation = Quaternion.Euler(playerRotation);

    }
}
