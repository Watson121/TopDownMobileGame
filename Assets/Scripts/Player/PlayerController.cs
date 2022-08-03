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


    public bool move = false;

    public float frustumHieght;
    public float frustumWidth;
    public float distance = 3.0f;
    


    public Vector3 screenBounds;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
        screenBounds = ray.origin;
        CalculateCamera();
    }

    private void CalculateCamera()
    {
        distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        frustumHieght = 2.0f * distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumHieght /= 2;


    }

    private void OnEnable()
    {
        playerControls.Player.Movement.performed += OnMovementAction;
        playerControls.Player.Movement.canceled += OnMovementAction;
   
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
        //playerPostion.x = Mathf.Clamp(playerPostion.x , screenBounds.y, screenBounds.y * -1);
        Debug.Log("Player y " + playerPostion.y);

        playerPostion.y = Mathf.Clamp(playerPostion.y, -frustumHieght, frustumHieght);
        //playerPostion.y = Mathf.Clamp(playerPostion.y, (screenBounds.y + positionOffset), (screenBounds.y + positionOffset) * -1);

        Debug.Log("Screen Bounds Y: " + screenBounds.y);

        // Setting Player Rotation
        playerRotation += new Vector3(0, inputVector.y * 10f * Time.deltaTime, inputVector.x * 10f * Time.deltaTime);
        playerRotation.z = Mathf.Clamp(playerRotation.z, -horitontalRotation, horitontalRotation);
        playerRotation.y = Mathf.Clamp(playerRotation.y, -verticalRotation, verticalRotation);

        transform.position = playerPostion;
        transform.rotation = Quaternion.Euler(playerRotation);

        
        
    }
}
