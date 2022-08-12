using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Viewport;

public class PlayerController : MonoBehaviour, IDamage
{

    // Max Player Health 
    private const float MAX_HEALTH = 100.0f;

    // Returng the current health for the player
    public float Health
    {
        get { return health; }
    }

    private float health;

    // Player Control Inputs
    private PlayerInput playerInput;
    private PlayerControls playerControls;

    // Player Position and Rotation, along with the offsets
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

    // Weapons
    private Weapon currentEquipedWeapon;
    private Weapon baseWeapon;

    // Bullets
    [SerializeField] private List<Bullet> bullets;
    private static int index = 0;
    [SerializeField] Transform bulletSpawnPoint;

  


  

    private void Awake()
    {
        //CalculatingViewportBounds();
        ViewportBoundaries.CalculatingViewportBounds(transform);
        ControlSetup();
        FindBullets();
        PlayerSetup();
        WeaponSetup();
    }

    /// <summary>
    /// Setting up the controls
    /// </summary>
    private void ControlSetup()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Player.Movement.performed += OnMovementAction;
        playerControls.Player.Movement.canceled += OnMovementAction;
        playerControls.Player.Fire.performed += WeaponFire;
    }

    /// <summary>
    /// Getting the camera bounds so that player is clamp to stay within the viewport
    /// </summary>
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

    /// <summary>
    /// Setting up the weapons
    /// </summary>
    private void WeaponSetup()
    {
        baseWeapon = new Weapon(10.0f, 10.0f, true);
        currentEquipedWeapon = baseWeapon;
    }

    /// <summary>
    /// Setting up the player
    /// </summary>
    private void PlayerSetup()
    {
        health = MAX_HEALTH;
    }

    /// <summary>
    /// Finding all of the bullets from the pool of bullets
    /// </summary>
    private void FindBullets()
    {
        GameObject[] tempBullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach(GameObject bullet in tempBullets)
        {
            bullets.Add(bullet.GetComponent<Bullet>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Movement();
        }   
    }

    // Turning on or off the movement for the player
    private void OnMovementAction(InputAction.CallbackContext obj)
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
    /// Firing the Weapon that the player has currently equiped
    /// </summary>
    /// <param name="obj"></param>
    private void WeaponFire(InputAction.CallbackContext obj)
    {

#if UNITY_EDITOR
        Debug.Log("Test and index: " + index);
        Debug.Log("Weapon Damage: " + baseWeapon.Damage);
#endif

        // Getting the current bullet
        Bullet currentBullet = bullets[index];

        // If the bullet has not been preiously fired, then fire it. If it has been fired then move onto the bullet.
        if (!(bullets[index].BulletMoving))
        {
            StartCoroutine(currentBullet.BulletFire(bulletSpawnPoint.position, currentEquipedWeapon));
        }

        index++;

        if (index >= bullets.Count)
        {
            index = 0;
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
        playerPostion.x = Mathf.Clamp(playerPostion.x , -ViewportBoundaries.frustumWidth + horizontalScreenOffset, ViewportBoundaries.frustumWidth - horizontalScreenOffset);
        playerPostion.y = Mathf.Clamp(playerPostion.y, -ViewportBoundaries.frustumHeight + 1.0f, ViewportBoundaries.frustumHeight - veritcalScreenOffset);

        // Setting Player Rotation
        playerRotation += new Vector3(inputVector.y * playerRotationSpeed * Time.deltaTime, 0, inputVector.x * playerRotationSpeed * Time.deltaTime);
        playerRotation.z = Mathf.Clamp(playerRotation.z, -horitontalRotation, horitontalRotation);
        playerRotation.x = Mathf.Clamp(playerRotation.x, -verticalRotation, verticalRotation);                      

        // Applying the position and rotation to the player
        transform.position = playerPostion;
        transform.rotation = Quaternion.Euler(playerRotation);

    }

    // Interface - Applying Damage to the player
    public void ApplyDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, MAX_HEALTH);

        if(health == 0)
        {
            Debug.Log("Dead");
        }
        
    }
}
