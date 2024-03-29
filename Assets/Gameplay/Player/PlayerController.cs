using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Viewport;

public class PlayerController : MonoBehaviour, IDamage, ICollectable
{

    #region Player Health

    // Max Player Health 
    [SerializeField] private float MAX_HEALTH;

    [Header("Player Health")]
    [SerializeField] private float health;

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

    #region Managers

    [Header("Game Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    #endregion

    #region Player Movement

    // Player Control Inputs
    private PlayerInput playerInput;
    private PlayerControls playerControls;

    // Player Position and Rotation, along with the offsets
    private Vector3 playerPostion;
    private Vector3 playerRotation;
    private Vector3 playerStartingPostion = new Vector3(0, 0, -2.8f);
    private bool move = false;
    private float veritcalScreenOffset = 4.0f;
    private float horizontalScreenOffset = 2.0f;

    [Header("Player Settings")]
    [SerializeField] private float playerSpeed = 9.0f;
    [SerializeField] private float playerRotationSpeed = 3.0f;
    [SerializeField] private float horitontalRotation = 15.0f;
    [SerializeField] private float verticalRotation = 15.0f;

    #endregion

    #region Player Camera 

    // Player Camera & Viewport Bounds
    private Camera playerCamera;
/*    private float frustumHeight;
    private float frustumWidth;*/
/*    private float cameraDistance = 3.0f;*/

    #endregion

    #region Weapons

    // Weapons
    [Header("Weapons")]
    private Weapon currentEquipedWeapon;
    private Weapon ketchupGun;
    private Weapon mustardGun;
    private Weapon mayoGun;
    [SerializeField] private float weaponDamage;

    // Bullets
    [Header("Bullet Settings")]
    [SerializeField] private List<Bullet> bullets;
    private static int index = 0;
    [SerializeField] Transform bulletSpawnPoint;

    // Targetting Ray
    private LineRenderer targettingRay;
    private RaycastHit hit;

    #endregion

    #region Shields

    [Header("Shields")]
    [SerializeField] GameObject shieldMesh;
    [SerializeField] float shieldLongiviety;
    bool shieldActive;

    #endregion

    #region Debugging

    [Header("Debugging")]
    [SerializeField] private bool turnShieldsOn = false;
    [SerializeField] private bool invisible = false;

    #endregion



    private void Awake()
    {
        ViewportBoundaries.CalculatingViewportBounds(transform);
        targettingRay = GetComponent<LineRenderer>();

        playerCamera = Camera.main;

        FindManagers();
        FindBullets();
        PlayerSetup();
        WeaponSetup();
        ShieldSetup();
        ControlSetup();

        
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
    /// Setting up the weapons
    /// </summary>
    private void WeaponSetup()
    {
        int weaponLevel = gameManager.WeaponLevel;

        switch (weaponLevel)
        {
            case 1:
                weaponDamage = 2.0f;
                break;
            case 2:
                weaponDamage = 3.0f;
                break;
            case 0:
                weaponDamage = 1.0f;
                break;
        }


        ketchupGun = new Weapon(weaponDamage, 10.0f, true, SauceType.Ketchup);
        mustardGun = new Weapon(weaponDamage, 10.0f, false, SauceType.Musturd);
        mayoGun = new Weapon(weaponDamage, 10.0f, false, SauceType.Mayo);


        currentEquipedWeapon = ketchupGun;
        uiManager.UpdateCurrentWeapon(SauceType.Ketchup);
    }

    private void ShieldSetup()
    {
        int shieldLevel = gameManager.ShieldLevel;

        switch (shieldLevel)
        {
            case 1:
                shieldLongiviety = 4.0f;
                break;
            case 2:
                shieldLongiviety = 5.0f;
                break;
            case 3:
                shieldLongiviety = 6.0f;
                break;
            case 0:
                shieldLongiviety = 3.0f;
                break;
        }

        
#if UNITY_EDITOR

        // If this debugging option is turned on, then it will force shields to always be active
        if (turnShieldsOn)
        {
            shieldMesh.SetActive(true);
        }

#endif


    }

    /// <summary>
    /// Setting up the player
    /// </summary>
    private void PlayerSetup()
    {
        int healthLevel = gameManager.HealthLevel;

        // Checking what the health level is in the game manager, and setting the MAX HEALTH to the corrct health amount
        switch(healthLevel){
            case 1:
                MAX_HEALTH = 125;
                break;
            case 2:
                MAX_HEALTH = 150;
                break;
            case 3:
                MAX_HEALTH = 175;
                break;
            case 4:
                MAX_HEALTH = 200;
                break;
            case 0:
                MAX_HEALTH = 100;
                break;
        }


        health = MAX_HEALTH;
    }
    
    /// <summary>
    /// Finding the game managers
    /// </summary>
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

        TargettingRay();
    }

    /// <summary>
    /// Updates the targetting ray that is attached to the player
    /// </summary>
    private void TargettingRay()
    {
        
        // Making so that it should only detect objects on the enemy layer
        int layerMask = 1 << 6;

        // Updating the colour of the ray
        // Red - No Target in Range
        // Green - Target in Range
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


    /// <summary>
    /// Turning on or off the movement for the player
    /// </summary>
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
            gameManager.AddActiveBullet(currentBullet);
            StartCoroutine(currentBullet.BulletFire(bulletSpawnPoint.position, currentEquipedWeapon, Vector3.forward, 3.0f, gameManager));
        }

        index++;

        if (index >= bullets.Count)
        {
            index = 0;
        }
    }

    /// <summary>
    /// Player Controls - Switching between the different weapons
    /// </summary>
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

        // Setting Player Camera
        playerCamera.gameObject.transform.position = new Vector3(playerPostion.x, playerPostion.y + 3.0f, -8);

    }

    /// <summary>
    /// Killing the player, updating the high score and opening the death screen
    /// </summary>
    private void PlayerDeath()
    {
        Debug.Log("Player Has Died");
        Time.timeScale = 0;
        gameManager.HighScoreUpdateHandler(gameManager.HighScore);
        uiManager.OpenDeathScreen();
        
    }

    /// <summary>
    /// Reseting the player, including their health and position
    /// </summary>
    public void PlayerReset()
    {
        Debug.Log("The Player has been reset!");
        
        // Setting Health Back to MAX_HEALTH, and updating the UI
        health = MAX_HEALTH;
        uiManager.UpdatePlayerHealth_UI(health);

        // Setting the player position back to start
        transform.position = playerStartingPostion;

        // Setting Current Weapon back to ketchup gun
        currentEquipedWeapon = ketchupGun;

        // Reseting the Player Camera
        playerCamera.gameObject.transform.position = new Vector3(playerPostion.x, playerPostion.y + 3.0f, -8);
    }

    /// <summary>
    /// Updating Player Health when either taking damage or picking up a health package
    /// </summary>
    /// <param name="healthChange"></param>
    private void UpdatePlayerHealth(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, MAX_HEALTH);
        uiManager.UpdatePlayerHealth_UI(health);
    }

    #region Damage 

    // Interface - Applying Damage to the player
    public void ApplyDamage(float damage)
    {

        // If the shield mesh is active, no damage should be taken
        if (!shieldMesh.activeSelf)
        {

            UpdatePlayerHealth(-damage);

            /*health -= damage;
            health = Mathf.Clamp(health, 0, MAX_HEALTH);
            uiManager.UpdatePlayerHealth_UI(health);*/

        }

#if UNITY_EDITOR

        // Debugging - If set invisible set to true, then the player cannot be killed
        if (invisible)
        {
            health = MAX_HEALTH;
        }

#endif

        if (health == 0)
        {
            PlayerDeath();
        }
        
        
    }

    public void ApplyDamageEnemy(float damage, SauceType bullet)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    /// <summary>
    /// Open up the pause menu, when hitting the pause button
    /// </summary>
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

    #region Collectables

    // Interface - Reacting to the different collectables
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
                UseHealthKit(collectable as HealthPackage);
                break;
        }

    }

    /// <summary>
    /// Increase Number of Gears that you have collected
    /// </summary>
    /// <param name="gear">The Gear Drop that the player has picked up</param>
    private void IncreaseGears(Gear gear)
    {
        Debug.Log("Have Collected Money Coin, now increasing monies");
        //gameManager.GearUpdateHandler(gear.Value);
    }

    /// <summary>
    /// Activate the shield after picking up a shield pickup
    /// </summary>
    private void ActivateShield()
    {
        
        StartCoroutine(ShieldCountdown());


        Debug.Log("Player has picked up Shield Pickup, Actiavated Shield");
    }

    /// <summary>
    /// The Shield is only active for a short amount of time
    /// </summary>
    private IEnumerator ShieldCountdown()
    {
        float elaspedTime = 0;


        shieldMesh.SetActive(true);
        while ((elaspedTime < shieldLongiviety))
        {
            elaspedTime += Time.deltaTime;
            yield return null;
        }
        shieldMesh.SetActive(false);

        // Cleanup
        StopCoroutine(ShieldCountdown());


    }

    /// <summary>
    /// Increase the player health after they have picked up a health package
    /// </summary>
    /// <param name="healthPackage">Health Package that the player has picked up</param>
    private void UseHealthKit(HealthPackage healthPackage)
    {

        if(health != MAX_HEALTH)
        {
            UpdatePlayerHealth(healthPackage.HealthToGive);
        }

#if UNITY_EDITOR

        Debug.Log("Health Pack picked up");

#endif
    }

    #endregion

}
