using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUserInterface : MonoBehaviour
{

    [Header("Managers ")]
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected SaveManager saveManager;

    // Start is called before the first frame update
    protected void Start()
    {
        GetGameManager();
        GetSaveManager();
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

    protected void SetupUI()
    {

    }

}
