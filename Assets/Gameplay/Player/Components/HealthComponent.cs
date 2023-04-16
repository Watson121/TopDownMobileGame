using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This Componenet is responsible for the management of player health
/// </summary>
/// 
[RequireComponent(typeof(ShieldComponent))]
public class HealthComponent : MonoBehaviour, IDamage, IComponent
{


    
    public float MaxHealth
    {
        get { return maxHealth; }
    }
    [Header("Health Settings")]
    [SerializeField] private float maxHealth;

    public float Health
    {
        get { return health; }
    }
    [SerializeField] private float health;

    private ShieldComponent playerShield;

    // Managers
    private GameManager gameManager;
    private UIManager uiManager;

    [Header("Debugging")]
    [SerializeField] private bool invicible = false;

    // This event is called when the player death happens
    public UnityEvent m_PlayerDeath;

    // This event is called when the player's health needs updating
    public UnityEvent m_PlayerHealthUpdate;


    // Start is called before the first frame update
    void Awake()
    {
        FindManagers();
        UnityEventSetup();
        FindShield();
        HealthSetup();
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
    /// Setting up the damage and death events
    /// </summary>
    private void UnityEventSetup()
    {
        // Setting up Player Damage Events
        m_PlayerHealthUpdate = new UnityEvent();
        m_PlayerHealthUpdate.AddListener(delegate { uiManager.UpdatePlayerHealth_UI(health); });

        // Setting up Player Death Events
        m_PlayerDeath.AddListener(delegate { gameManager.HighScoreUpdateHandler(gameManager.HighScore); });
        m_PlayerDeath.AddListener(uiManager.OpenDeathScreen);
    }

    /// <summary>
    /// Finding the player's shield
    /// </summary>
    private void FindShield()
    {
        playerShield = GetComponent<ShieldComponent>();
    }

    /// <summary>
    /// Setting up the Health for the players
    /// </summary>
    private void HealthSetup()
    {
        int healthLevel = gameManager.HealthLevel;

        // Checking what the health level is in the game manager, and setting the MAX HEALTH to the corrct health amount
        switch (healthLevel)
        {
            case 1:
                maxHealth = 125;
                break;
            case 2:
                maxHealth = 150;
                break;
            case 3:
                maxHealth = 175;
                break;
            case 4:
                maxHealth = 200;
                break;
            case 0:
                maxHealth = 100;
                break;
        }

        health = maxHealth;
    }

    /// <summary>
    /// Updating the Player Health, and updating the UI
    /// </summary>
    /// <param name="healthChange">Amount of Health to change by</param>
    private void UpdatePlayerHealth(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, maxHealth);
        m_PlayerHealthUpdate.Invoke();
        //uiManager.UpdatePlayerHealth_UI(health);
    }

    /// <summary>
    /// Killing the player, updating the high score and opening the death screen
    /// </summary>
    private void PlayerDeath()
    {
        Debug.Log("Player Has Died");
        Time.timeScale = 0;
        m_PlayerDeath.Invoke();
    }

    /// <summary>
    /// Applying Damage to the Player
    /// </summary>
    /// <param name="damage">Amount of Damage Player has taken</param>
    public void ApplyDamage(float damage)
    {


        // If the shield mesh is active, no damage should be taken
        if (!playerShield.ShieldMesh.activeSelf)
        {
            UpdatePlayerHealth(-damage);
        }

#if UNITY_EDITOR

        // Debugging - If set invisible set to true, then the player cannot be killed
        if (invicible)
        {
            health = maxHealth;
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


    /// <summary>
    /// Reseting the player health back to it's max health 
    /// </summary>
    public void ResetComponent()
    {

        Debug.Log("HEALTH COMPONENT RESET");
        health = maxHealth;
        m_PlayerHealthUpdate.Invoke();

    }

    /// <summary>
    /// Increase the player health after they have picked up a health package
    /// </summary>
    public void UseHealthKit()
    {

        if (health != maxHealth)
        {
            UpdatePlayerHealth(10.0f);
        }

#if UNITY_EDITOR

        Debug.Log("Health Pack picked up");

#endif
    }
}
