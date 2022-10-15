using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Viewport;

public class PlayerController : MonoBehaviour, IDamage, ICollectable
{

    #region Player Health

    // Max Player Health 
    private const float MAX_HEALTH = 100.0f;

    [Header("Player Health")]
    [SerializeField] private float health = MAX_HEALTH;

    public float MaxHealth
    {
        get { return MAX_HEALTH; }
    }

    // Returng the current health for the player
    public float Health
    {
        get { return health; }
    }

    #endregion


    // Player Control Inputs
    private PlayerInput playerInput;
    private PlayerControls playerControls;

    // Player Position and Rotation, along with the offsets
    private Vector3 playerPostion;
    private Vector3 playerRotation;
    private bool move = false;
    private float veritcalScreenOffset = 4.0f;
    private float horizontalScreenOffset = 2.0f;

    [Header("Game Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

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
    private Weapon ketchupGun;
    private Weapon mustardGun;
    private Weapon mayoGun;

    // Bullets
    [Header("Bullet Settings")]
    [SerializeField] private List<Bullet> bullets;
    private static int index = 0;
    [SerializeField] Transform bulletSpawnPoint;

    // Targetting Ray
    private LineRenderer targettingRay;
    private RaycastHit hit;

    private void Start()
    {
        //CalculatingViewportBounds();
        ViewportBoundaries.CalculatingViewportBounds(transform);
        ControlSetup();
        FindManagers();
        FindBullets();
        PlayerSetup();
        WeaponSetup();

        targettingRay = GetComponent<LineRenderer>();
      
    }



    /// <summary>
    /// Setting up the controls
    /// </summary>
    private void ControlSetup()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();

        // Movement
        playerControls.Player.Movement.performed += OnMovementAction;
        playerControls.Player.Movement.canceled += OnMovementAction;
        
        // Weapons
        playerControls.Player.Fire.performed += WeaponFire;
        playerControls.Player.Ketchup.performed += SwitchWeapon;
        playerControls.Player.Mustard.performed += SwitchWeapon;
        playerControls.Player.Mayo.performed += SwitchWeapon;

        // Pausing Game
        playerControls.Player.Pause.performed += PausingGame;
    }

    private void OnDisable()
    {
        // Movement
        playerControls.Player.Movement.performed -= OnMovementAction;
        playerControls.Player.Movement.canceled -= OnMovementAction;

        // Weapons
        playerControls.Player.Fire.performed -= WeaponFire;
        playerControls.Player.Ketchup.performed -= SwitchWeapon;
        playerControls.Player.Mustard.performed -= SwitchWeapon;
        playerControls.Player.Mayo.performed -= SwitchWeapon;

        // Pausing Game
        playerControls.Player.Pause.performed -= PausingGame;
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
        ketchupGun = new Weapon(1.0f, 10.0f, true, SauceType.Ketchup);
        mustardGun = new Weapon(1.0f, 10.0f, false, SauceType.Musturd);
        mayoGun = new Weapon(1.0f, 10.0f, false, SauceType.Mayo);


        currentEquipedWeapon = ketchupGun;
        uiManager.UpdateCurrentWeapon(SauceType.Ketchup);
    }

    /// <summary>
    /// Setting up the player
    /// </summary>
    private void PlayerSetup()
    {
        health = MAX_HEALTH;
    }
    
    private void FindManagers()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    /// <summary>
    /// Finding all of the bullets from the pool of bullets
    /// </summary>
    private void FindBullets()
    {
        bullets = gameManager.PlayerBulletPool;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Movement();
        }

        int layerMask = 1 << 6;

        if (Physics.Raycast(transform.position, Vector3.forward * 20, 20, layerMask))
        {
            targettingRay.startColor = Color.green;
            targettingRay.endColor = Color.green;
        }
        else
        {
            targettingRay.startColor = Color.red;
            targettingRay.endColor = Color.red;
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
        Debug.Log("Weapon Damage: " + ketchupGun.Damage);
#endif

        // Getting the current bullet
        Bullet currentBullet = bullets[index];

        // If the bullet has not been preiously fired, then fire it. If it has been fired then move onto the bullet.
        if (!(bullets[index].BulletMoving))
        {
            StartCoroutine(currentBullet.BulletFire(bulletSpawnPoint.position, currentEquipedWeapon, Vector3.forward, 3.0f));
        }

        index++;

        if (index >= bullets.Count)
        {
            index = 0;
        }
    }

    private void SwitchWeapon(InputAction.CallbackContext obj)
    {

        string currentAction = obj.action.name;

        switch (currentAction)
        {
            case "Ketchup":
                Debug.Log("Ketchup Gun Equiped");
                currentEquipedWeapon = ketchupGun;
                uiManager.UpdateCurrentWeapon(SauceType.Ketchup);
                break;
            case "Mustard":
                Debug.Log("Mustard Gun Equiped");
                currentEquipedWeapon = mustardGun;
                uiManager.UpdateCurrentWeapon(SauceType.Musturd);
                break;
            case "Mayo":
                Debug.Log("Mayo Gun Equiped");
                currentEquipedWeapon = mayoGun;
                uiManager.UpdateCurrentWeapon(SauceType.Mayo);
                break;
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
        
        // Setting Targetting Ray Starting Location
        targettingRay.SetPosition(0, transform.position);
        targettingRay.SetPosition(1, transform.position + (Vector3.forward * 20));

    }

    private void PlayerDeath()
    {
        Debug.Log("Player Has Died");
        Time.timeScale = 0;
        gameManager.HighScoreUpdateHandler(gameManager.HighScore);
        uiManager.OpenDeathScreen();
        
    }

    // Interface - Applying Damage to the player
    public void ApplyDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, MAX_HEALTH);
        //uiManager.UpdatePlayerHealth_UI();

        uiManager.UpdatePlayerHealth_UI(health);

        if (health == 0)
        {
            PlayerDeath();
        }
        
    }

    public void ApplyDamageEnemy(float damage, SauceType bullet)
    {
        throw new System.NotImplementedException();
    }

    private void PausingGame(InputAction.CallbackContext obj)
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

     
        uiManager.OpenPauseMenu();
    }

    public void Collect(Collectable collectable)
    {
        CollectableType collectableType = collectable.TypeOfCollectable;

        switch (collectableType)
        {
            case CollectableType.EMoney:
                IncreaseGears(collectable as Gear);
                break;
            case CollectableType.EShield:
                ActivateShield();
                break;
            case CollectableType.EHealth:
                UseHealthKit();
                break;
        }

    }

    private void IncreaseGears(Gear moneyCoin)
    {
        Debug.Log("Have Collected Money Coin, now increasing monies");
        gameManager.GearUpdateHandler(moneyCoin.Value);
    }

    private void ActivateShield()
    {
        Debug.Log("Player has picked up Shield Pickup, Actiavated Shield");
    }

    private void UseHealthKit()
    {
        Debug.Log("Player has collected Health Kit, Using it to help player");
    }

}
