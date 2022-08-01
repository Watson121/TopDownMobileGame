using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerControls playerControls;

    private Vector3 playerPostion;
    private float playerSpeed = 10.0f;
    private bool move = false;

    private Vector2 screenBounds;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Screen.width));
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

        playerPostion += new Vector3(inputVector.x * playerSpeed * Time.deltaTime, 0, inputVector.y * playerSpeed * Time.deltaTime);
        playerPostion.x = Mathf.Clamp(playerPostion.x , screenBounds.x, screenBounds.x * -1);
        Debug.Log(Screen.width);
        
        transform.position = playerPostion;
    }
}
