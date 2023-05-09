using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the weapony including firing and changing weapons
/// </summary>
public class WeaponsComponent : MonoBehaviour, IComponent
{
    
    // Weapon Settings
    private Weapon currentEquipedWeapon;
    private Weapon ketchupGun;
    private Weapon mustardGun;
    private Weapon mayoGun;
    private float weaponDamage;

    // Bullet Settings
    private List<Bullet> bullets;
    private static int bulletIndex = 0;
    [SerializeField] private Transform bulletSpawnPoint;

    // Game Manager - holds important gameplay information
    private GameManager gameManager;
    private HUDManager hudManager;

    // Start is called before the first frame update
    void Awake()
    {
        FindManagers();
        FindPlayerBullets();
        WeaponSetup();
    }

    /// <summary>
    /// Finding the game managers
    /// </summary>
    private void FindManagers()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();
    }

    /// <summary>
    /// Getting the pool of Player Bullets
    /// </summary>
    private void FindPlayerBullets()
    {
        bullets = gameManager.PlayerBulletPool;
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
        hudManager.UpdateCurrentWeapon(SauceType.Ketchup);
    }

    /// <summary>
    /// Firing the Weapon that the player has currently equiped
    /// </summary>
    /// <param name="obj"></param>
    public void WeaponFire(InputAction.CallbackContext obj)
    {

        // Getting the current bullet
        Bullet currentBullet = bullets[bulletIndex];

        // If the bullet has not been preiously fired, then fire it. If it has been fired then move onto the bullet.
        if (!(bullets[bulletIndex].BulletMoving))
        {
            gameManager.AddActiveBullet(currentBullet);
            StartCoroutine(currentBullet.BulletFire(bulletSpawnPoint.position, currentEquipedWeapon, Vector3.forward, 3.0f, gameManager));
        }
        bulletIndex++;

        if (bulletIndex >= bullets.Count)
        {
            bulletIndex = 0;
        }
    }

    /// <summary>
    /// Switching between the different weapons
    /// </summary>
    public void SwitchWeapon(InputAction.CallbackContext obj)
    {

        string currentAction = obj.action.name;

        switch (currentAction)
        {
            case "Ketchup":
                Debug.Log("Ketchup Gun Equiped");
                currentEquipedWeapon = ketchupGun;
                hudManager.UpdateCurrentWeapon(SauceType.Ketchup);
                break;
            case "Mustard":
                Debug.Log("Mustard Gun Equiped");
                currentEquipedWeapon = mustardGun;
                hudManager.UpdateCurrentWeapon(SauceType.Musturd);
                break;
            case "Mayo":
                Debug.Log("Mayo Gun Equiped");
                currentEquipedWeapon = mayoGun;
                hudManager.UpdateCurrentWeapon(SauceType.Mayo);
                break;
        }


    }

    /// <summary>
    /// Pausing the game
    /// </summary>
    /// <param name="obj"></param>
    public void PausingGame(InputAction.CallbackContext obj)
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


        hudManager.TogglePauseMenu();
    }

    /// <summary>
    /// Reseting the Weapons  back to the starting weapon
    /// </summary>
    public void ResetComponent()
    {
        currentEquipedWeapon = ketchupGun;
    }
}
