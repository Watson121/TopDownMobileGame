using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUserInterface : MonoBehaviour
{

    [Header("Managers ")]
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected SaveManager saveManager;
    [SerializeField] protected LevelManager levelManager;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        GetGameManager();
        GetSaveManager();
        GetLevelManager();   
        SetupUI();
    }

    /// <summary>
    /// Get the Game Manager
    /// </summary>
    private void GetGameManager()
    {
        Debug.Log("Finding Game Manager");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Get the Save Manager
    private void GetSaveManager()
    {
        Debug.Log("Findng Save Manager");
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    /// <summary>
    /// Getting the Level Manager
    /// </summary>
    private void GetLevelManager()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    protected virtual void SetupUI()
    {
        Debug.Log("Base Setup UI method called");
    }

}
