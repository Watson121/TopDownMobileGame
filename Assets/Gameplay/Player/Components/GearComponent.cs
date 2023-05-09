using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearComponent : MonoBehaviour
{

    private GameManager gameManager;
    private HUDManager hudManager;

    private UnityEvent m_GearCollected;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();
    }



    public void IncreaseGears()
    {
        gameManager.GearUpdateHandler(10);
    }

}
