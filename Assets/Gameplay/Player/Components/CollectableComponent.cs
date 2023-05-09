using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// This component is responisible for dealing with when the player interacts with a collectable
/// </summary>
public class CollectableComponent : MonoBehaviour, ICollectable
{
    [Header("Health Kit Collection Events")]
    public UnityEvent m_HealthKitCollected;

    [Header("Shield Collection Events")]
    public UnityEvent m_ShieldCollected;

    [Header("Gear Collection Events")]
    public UnityEvent m_GearCollected;

    private HUDManager hudManager;
    private GameManager gameManager;
    private HealthComponent healthComponent;

    private void Start()
    {
        FindManagers();
        UnityEventSetup();
    }

    private void FindManagers()
    {
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthComponent = GetComponent<HealthComponent>();
    }

    private void UnityEventSetup()
    {
        m_GearCollected = new UnityEvent();
        m_GearCollected.AddListener(() => gameManager.NumberOfGearsCollected += 10);
        m_GearCollected.AddListener(() => hudManager.UpdateGearCollection(gameManager.NumberOfGearsCollected));
    }



    /// <summary>
    /// Reacting to the different collectables
    /// </summary>
    /// <param name="collectable">The Collectable currently collected </param>
    public void Collect(Collectable collectable)
    {
        CollectableType collectableType = collectable.TypeOfCollectable;

        switch (collectableType)
        {
            case CollectableType.EMoney:
                m_GearCollected.Invoke();
                break;
            case CollectableType.EShield:
                m_ShieldCollected.Invoke();
                break;
            case CollectableType.EHealth:
                m_HealthKitCollected.Invoke();
                break;
        }
    }
}
