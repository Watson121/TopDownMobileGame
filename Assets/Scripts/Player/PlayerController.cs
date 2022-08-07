using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamage
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

    // Weapons
    private Weapon currentEquipedWeapon;
    private Weapon baseWeapon;

    // Bullets
    [SerializeField] private List<Bullet> bullets;
    private static int index = 0;
    [SerializeField] Transform bulletSpawnPoint;

    // Player Health
    private const float MAX_HEALTH = 100.0f;
    
    public float Health
    {
        get { return health; }
    }

    [SerializeField] private float health;

    private void Awake()
    {
        CalculatingViewportBounds();
        ControlSetup();
        FindBullets();
        PlayerSetup();
        WeaponSetup();
    }

    // Setting up the controls
    private void ControlSetup()
    {
        playerInput = new PlayerInput();
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Player.Movement.performed += OnMovementAction;
        playerControls.Player.Movement.canceled += OnMovementAction;
        playerControls.Player.Fire.performed += WeaponFire;
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

    private void WeaponSetup()
    {
        baseWeapon = new Weapon(10.0f, 10.0f, true);
        currentEquipedWeapon = baseWeapon;
    }

    private void PlayerSetup()
    {
        health = MAX_HEALTH;
    }

    private void ApplyBulletDamage()
    {
        Debug.Log("Apply Damaging");
    }

    private void FindBullets()
    {
        GameObject[] tempBullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach(GameObject bullet in tempBullets)
        {
            bullets.Add(bullet.GetComponent<Bullet>());
        }

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
        
        if (obj.canceled)
        {
            move = false;
           
        }
    }

    private void WeaponFire(InputAction.CallbackContext obj)
    {
        Debug.Log("Test and index: " + index);
        Debug.Log("Weapon Damage: " + baseWeapon.Damage);


        if (!(bullets[index].BulletMoving))
        {
            StopCoroutine(bullets[index].BulletFire(currentEquipedWeapon.FiringSpeed, bulletSpawnPoint.position, currentEquipedWeapon.Damage));
            StartCoroutine(bullets[index].BulletFire(currentEquipedWeapon.FiringSpeed, bulletSpawnPoint.position, currentEquipedWeapon.Damage));
        }
        index++;

        if (index >= bullets.Count)
        {
            index = 0;
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
