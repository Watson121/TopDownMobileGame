using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearComponent : MonoBehaviour
{

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void IncreaseGears()
    {
        gameManager.GearUpdateHandler(10);
    }

}
