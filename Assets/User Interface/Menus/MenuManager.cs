using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Manager is responsible for the menus within the main menu
/// </summary>
public class MenuManager : MonoBehaviour
{

    [Header("Managers")]
    [SerializeField] private SaveManager saveManager;

    [Header("Menus")]
    [SerializeField] private GameObject profileCreator;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private GameObject upgradeScreen;


    // Start is called before the first frame update
    void Start()
    {
        FindSaveManager();
    }

    /// <summary>
    /// Finding the Save Manager and setting up the events
    /// </summary>
    private void FindSaveManager()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        
        // Event setup   
        saveManager.NewSave.AddListener(NewGame);
        saveManager.NormalStartup.AddListener(NormalStartup); 
    }

    /// <summary>
    /// On Scene Change Then Listeners should be removed to stop any errors
    /// </summary>
    private void OnDestroy()
    {
        saveManager.NewSave.RemoveListener(NewGame);
        saveManager.NormalStartup.RemoveListener(NormalStartup);
    }

    /// <summary>
    /// Toggling a menu on and off
    /// </summary>
    /// <param name="menuToToggle"> The menu to toggle on and off </param>
    public void ToggleMenu(GameObject menuToToggle)
    {
        Debug.Log("Menu Toggled: " + menuToToggle.name);
        menuToToggle.SetActive(!menuToToggle.activeSelf);
    }

    /// <summary>
    /// When a new game is started, the player won't have a profile so this will load up the profile
    /// </summary>
    private void NewGame()
    {
        Debug.Log("New Game Function Called in Menu Manager");

        profileCreator.SetActive(true);
        mainMenu.SetActive(false);
        levelSelection.SetActive(false);
    }

    /// <summary>
    /// When a profile is found and the game has just started this 
    /// </summary>
    private void NormalStartup()
    {
        profileCreator.SetActive(false);
        mainMenu.SetActive(true);
        levelSelection.SetActive(false);
    }

}
