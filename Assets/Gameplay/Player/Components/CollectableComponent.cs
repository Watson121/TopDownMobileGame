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
