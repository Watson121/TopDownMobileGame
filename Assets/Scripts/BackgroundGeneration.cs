using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGeneration : MonoBehaviour
{

    public GameObject backgroundMesh;
    public float speed = 1.0f; 

    private List<Transform> backgroundTransforms = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= 4; i++)
        {
            GenerateNewBackgroundMesh();
        }
    }

    private void GenerateNewBackgroundMesh()
    {
        int lastIndex = 0;
        GameObject newBackground;

        if (backgroundTransforms.Count != 0)
        {
            lastIndex = backgroundTransforms.Count - 1;
            newBackground = Instantiate(backgroundMesh, new Vector3(0.0f, -5.0f, backgroundTransforms[lastIndex].position.z + 30.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        }
        else
        {
            newBackground = Instantiate(backgroundMesh, new Vector3(0.0f, -5.0f, 0.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        }

        backgroundTransforms.Add(newBackground.transform);
    }

    private void RemoveBackgroundMesh(Transform oldBuilding)
    {
        backgroundTransforms.Remove(oldBuilding);
        Destroy(oldBuilding.gameObject);
        GenerateNewBackgroundMesh();
    }

    private void MovingBackground()
    {
        for(int i = 0; i < backgroundTransforms.Count; i++)
        {
            Transform background = backgroundTransforms[i];
            background.position -= new Vector3(0, 0, speed * Time.deltaTime);
        
            if(background.position.z < -35.0f)
            {
                RemoveBackgroundMesh(background);
            }
        
        }
    }


    // Update is called once per frame
    void Update()
    {
        MovingBackground();
    }

}
