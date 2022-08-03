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
    private float positionOffset = 2.0f;    


    private float playerSpeed = 5f;
    private float playerRotationSpeed = 10.0f;
    private float horitontalRotation = 15.0f;
    private float verticalRotation = 15.0f;


    private bool move = false;

    


    private Vector2 screenBounds;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void OnEnable()
    {
        playerControls.Player.Movement.performed += OnMovementAction;
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
        if (obj.performed)
        {
            move = true;
        }
        else if (obj.canceled)
        {
            move = false;
        }
    }

    private void Movement()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();

        // Setting Player Position
        playerPostion += new Vector3(inputVector.x * playerSpeed * Time.deltaTime, inputVector.y * playerSpeed * Time.deltaTime, 0);
        playerPostion.x = Mathf.Clamp(playerPostion.x , (screenBounds.x + positionOffset), (screenBounds.x + positionOffset) * -1);
        
        // Setting Player Rotation
        playerRotation += new Vector3(0, inputVector.y * 10f * Time.deltaTime, inputVector.x * 10f * Time.deltaTime);
        

        transform.position = playerPostion;
        transform.rotation = Quaternion.Euler(playerRotation);

        
        
    }
}
