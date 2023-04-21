using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Viewport;

/// <summary>
/// Controls the player movement, including positon and rotation
/// </summary>
[RequireComponent(typeof(PlayerInputComponent))]
public class MovementComponent : MonoBehaviour, IComponent
{

    public PlayerControls PlayerControls
    {
        set { playerControls = value; }
    }
    private PlayerControls playerControls;


    // Player Position and Rotation, along with the offsets
    private Vector3 playerPostion;
    private Vector3 playerRotation;
    private Vector3 playerStartingPostion = new Vector3(0, 0, -2.8f);
    private bool move = false;
    private float veritcalScreenOffset = 4.0f;
    private float horizontalScreenOffset = 2.0f;

    /// <summary>
    /// Allows for changing speed in editor
    /// </summary>
    [Header("Movement Settings")]
    [SerializeField] private float playerSpeed = 9.0f;
    [SerializeField] private float playerRotationSpeed = 3.0f;
    [SerializeField] private float horitontalRotation = 15.0f;
    [SerializeField] private float verticalRotation = 15.0f;


    // Start is called before the first frame update
    void Awake()
    {
        ViewportBoundaries.CalculatingViewportBounds(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Movement();
        }
    }

    /// <summary>
    /// Setting the player movement to true or false
    /// </summary>
    public void OnMovementAction(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            move = true;

        }

        if (obj.canceled)
        {
            move = false;
        }
    }

    /// <summary>
    /// Moving the player around the level
    /// </summary>
    private void Movement()
    {


        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();

        // Setting Player Position
        playerPostion = transform.position;
        playerPostion += new Vector3(inputVector.x * playerSpeed * Time.deltaTime, inputVector.y * playerSpeed * Time.deltaTime, 0);
        playerPostion.x = Mathf.Clamp(playerPostion.x, -ViewportBoundaries.frustumWidth + horizontalScreenOffset, ViewportBoundaries.frustumWidth - horizontalScreenOffset);
        playerPostion.y = Mathf.Clamp(playerPostion.y, -ViewportBoundaries.frustumHeight + 1.0f, ViewportBoundaries.frustumHeight - veritcalScreenOffset);

        // Setting Player Rotation
        playerRotation += new Vector3(inputVector.y * playerRotationSpeed * Time.deltaTime, 0, inputVector.x * playerRotationSpeed * Time.deltaTime);
        playerRotation.z = Mathf.Clamp(playerRotation.z, -horitontalRotation, horitontalRotation);
        playerRotation.x = Mathf.Clamp(playerRotation.x, -verticalRotation, verticalRotation);

        // Applying the position and rotation to the player
        transform.position = playerPostion;
        transform.rotation = Quaternion.Euler(playerRotation);

      
    }

    /// <summary>
    /// Reseting the Player to their starting position
    /// </summary>
    public void ResetComponent()
    {
        transform.position = playerStartingPostion;
    }
}
