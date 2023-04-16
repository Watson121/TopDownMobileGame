using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls aspects of the shield, that the player can activate
/// </summary>
public class ShieldComponent : MonoBehaviour
{

    public GameObject ShieldMesh
    {
        get { return shieldMesh; }
    }

    [Header("Shield Settings")]
    [SerializeField] private GameObject shieldMesh;
    [SerializeField] private float shieldLongiviety;
    private bool shieldActive;

    [Header("Shield Debugging")]
    [SerializeField] private bool turnShieldsOn = false;

    void Start()
    {
        ShieldSetup();
    }

    /// <summary>
    /// Setting up the player shields parameters
    /// </summary>
    private void ShieldSetup()
    {
        int shieldLevel = GameObject.Find("GameManager").GetComponent<GameManager>().ShieldLevel;

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
    /// Activate the shield after picking up a shield pickup
    /// </summary>
    public void ActivateShield()
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
}
