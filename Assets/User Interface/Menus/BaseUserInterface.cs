using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseUserInterface : MonoBehaviour
{

    protected UIManager uiManager;
    protected GameManager gameManager;

    // Start is called before the first frame update
    protected void Start()
    {
        GetManagers();
    }

    private void GetManagers()
    {
        Debug.Log("Get Managers");

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    protected void SetupUI()
    {
        Debug.Log("Setup Menu Function Called");
    }



}
