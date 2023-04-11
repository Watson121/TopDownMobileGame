using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class BackgroundBuilding : MonoBehaviour
{
    [SerializeField] private GameObject building1;
    [SerializeField] private GameObject building2;
    [SerializeField] private GameObject building3;
    private Vector3 StartPosition;

    private void Start()
    {
        StartPosition = transform.position;
        SetBuilding((BuildingType)Random.Range(0, 3));
    }

    public void SetBuilding(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.type1:
                building1.SetActive(true);
                building2.SetActive(false);
                building3.SetActive(false);
                break;
            case BuildingType.type2:
                building1.SetActive(false);
                building2.SetActive(true);
                building3.SetActive(false);
                break;
            case BuildingType.type3:
                building1.SetActive(false);
                building2.SetActive(false);
                building3.SetActive(true);
                break;
        }

        transform.position = StartPosition;

    }


}
