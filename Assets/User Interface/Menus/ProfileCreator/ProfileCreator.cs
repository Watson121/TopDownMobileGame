using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ProfileCreator : BaseUserInterface
{

    [Header("Profile Creater UI Elements")]
    [SerializeField] private GameObject backButton;
    [SerializeField] private TMP_InputField playerInput;

    UnityAction ToggleButton;

    protected override void SetupUI()
    {
        Debug.Log("Setup UI in Profile Creator Called!");

 
        saveManager.NewSave.AddListener(ToggleButton);
        saveManager.NewSave.AddListener(() => this.gameObject.SetActive(true));


        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        //saveManager.NewSave.RemoveListener(ToggleButton);
    }

    /// <summary>
    /// Toggling the Back Button on or off. Should be off when there is no profile
    /// </summary>
    /// <param name="active"></param>
    public void ToggleBackButton(bool active)
    {
        backButton.SetActive(active);
    }

    /// <summary>
    /// Saving the Profile Name to the Save File 
    /// </summary>
    public void SaveProfileName()
    {
        saveManager.PlayerSave.playerName = playerInput.text;
        saveManager.SaveGame();
    }
}
