using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : BaseUserInterface
{

    [Header("Main Menu UI Elements")]
    [SerializeField] private TextMeshProUGUI currentPlayerProfileText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    /// <summary>
    /// Setup UI function that is setting up the funcaionality for the main menu
    /// </summary>
    protected override void SetupUI()
    {
        Debug.Log("Setup UI Function called in the Main Menu");

        // When a game saved make sure to update the player text
        saveManager.GameSaved.AddListener(UpdatePlayerProfileText);
        
        // When a normal setup is called make sure to update the player profile text
        saveManager.NormalStartup.AddListener(UpdatePlayerProfileText);
        
        // Setting up play button functionality
        playButton.onClick.AddListener(() => levelManager.PlayScene("MainScene"));

        // Setting up the quit game button functionality
        quitButton.onClick.AddListener(() => Application.Quit());
    }



    /// <summary>
    /// Update the Player Profile Text so the player knows which current profile they're on
    /// </summary>
    public void UpdatePlayerProfileText()
    {
        currentPlayerProfileText.text = "Player Name: " + saveManager.PlayerSave.playerName;
    }

    /// <summary>
    /// When this is destroyed make sure to remove these listeners so that they're are no errors
    /// </summary>
    private void OnDestroy()
    {
        saveManager.GameSaved.RemoveListener(UpdatePlayerProfileText);
        saveManager.NormalStartup.RemoveListener(UpdatePlayerProfileText);
    }
}
