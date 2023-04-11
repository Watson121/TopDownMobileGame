using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using UnityEditorInternal;
using UnityEngine;

public enum BuildingType
{
    type1,
    type2,
    type3
}


[ExecuteAlways]
public class BackgroundGeneration : MonoBehaviour
{

    [Range(1, 10)]
    public int NumberOfBuildings = 3;
    [SerializeField] private int numberBuildings = 3;

    [SerializeField] private List<GameObject> leftBackground;
    [SerializeField] private List<GameObject> rightBackground;

    

    public GameObject buildingOne;
    public GameObject buildingTwo;
    public GameObject buildingThree;

    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;

    private void Start()
    {
        OnVariableChange += UpdateBackground;
    }

    private void Update()
    {
       
        if(NumberOfBuildings != numberBuildings)
        {
            UpdateBackground();
            numberBuildings = NumberOfBuildings;
        }

        MoveBuildings();



    }

    private void UpdateBackground()
    {
        ClearBackgrounds();

        for (int i = 1; i <= NumberOfBuildings; i++)
        {
            leftBackground.Add(AddBuilding(-13, i));
            rightBackground.Add(AddBuilding(13, i));
        }

      
    }

    private GameObject AddBuilding(float x, float i)
    {
        
        GameObject newBuilding = Instantiate(RandomBuildingMesh(), new Vector3(x, Random.Range(-3, 3f), i * 8), transform.rotation);
        return newBuilding;
    }

    private GameObject RandomBuildingMesh()
    {
        BuildingType buildingType = (BuildingType)Random.Range(0, 3);
        GameObject building = buildingOne;

        switch (buildingType)
        {
            case BuildingType.type1:
                building = buildingOne;
                break;
            case BuildingType.type2:
                building = buildingTwo;
                break;
            case BuildingType.type3:
                building = buildingThree;
                break;
            default:
                building = buildingOne;
                break;
        }

        return building;
    }


    private void ClearBackgrounds()
    {
        for(int i = 0; i < leftBackground.Count; i++)
        {
            DestroyImmediate(leftBackground[i]);
            DestroyImmediate(rightBackground[i]);
        }

        leftBackground.Clear();
        rightBackground.Clear();
    }

    private void MoveBuildings()
    {
        for (int i = 0; i < leftBackground.Count; i++)
        {
            leftBackground[i].transform.position -= new Vector3(0, 0, 3) * 0.5f * Time.deltaTime;
            rightBackground[i].transform.position -= new Vector3(0, 0, 3) * 0.5f * Time.deltaTime;
        }
    }


}
