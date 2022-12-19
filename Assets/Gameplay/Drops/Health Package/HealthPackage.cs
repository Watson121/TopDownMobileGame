using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackage : Collectable
{
    public float HealthToGive { get { return healthToGive; } }
    private float healthToGive = 10.0f;

    public void SetHealthToGive()
    {
        healthToGive = Random.Range(10, 30);
    }


}
