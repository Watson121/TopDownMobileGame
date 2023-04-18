using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is responsible for targetting ray, and reacting when an enemy comes into range 
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class TargettingComponent : MonoBehaviour
{
    private LineRenderer targettingRay;
    private RaycastHit hit;

    // Making it so that the enemies are the only objects reacted to 
    private int enemyLayer; 

    // Start is called before the first frame update
    void Start()
    {
        ComponentSetup();
    }

    /// <summary>
    /// Setting up the Targetting Ray
    /// </summary>
    private void ComponentSetup()
    {
        targettingRay = GetComponent<LineRenderer>();
        enemyLayer = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatingTargetingRayColour();
        UpdatingTargetingRayPostion();
    }

    /// <summary>
    /// Updating the Colour of the targetting ray
    /// </summary>
    private void UpdatingTargetingRayColour()
    {
        // Updating the colour of the ray
        // Red - No Target in Range
        // Green - Target in Range
        if (Physics.Raycast(transform.position, Vector3.forward * 20, 20, enemyLayer))
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
    /// Updating the positon of the targeting ray
    /// </summary>
    private void UpdatingTargetingRayPostion()
    {
        targettingRay.SetPosition(0, transform.position);
        targettingRay.SetPosition(1, transform.position + (Vector3.forward * 20));
    }
}
