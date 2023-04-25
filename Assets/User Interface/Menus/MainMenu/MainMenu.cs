using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : BaseUserInterface
{

    [Header("Main Menu UI Elements")]
    [SerializeField] private TextMeshPro currentPlayerProfileText;

    protected void SetupUI()
    {
        Debug.Log("Setup UI Function called in the Main Menu");

        saveManager.GameSaved.AddListener(UpdatePlayerProfileText);
    }

    private void OnDestroy()
    {
        saveManager.GameSaved.RemoveListener(UpdatePlayerProfileText);
    }

    /// <summary>
    /// Update the Player Text 
    /// </summary>
    public void UpdatePlayerProfileText()
    {
        currentPlayerProfileText.text = saveManager.PlayerSave.playerName;
    }
}
