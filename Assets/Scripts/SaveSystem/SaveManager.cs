using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// This manager is responisble for holding the save data for the game.
/// Singleton - Should only be one version of this save
/// </summary>
public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// Making sure that there is only one Save Manager in game
    /// </summary>
    public static SaveManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if(instance != null)
            {
                Destroy(value.gameObject);
                return;
            }

            instance = value;
        }
    }

    private static SaveManager instance;


    public SaveData PlayerSave
    {
        get { return playerSave; }
    }
    private SaveData playerSave;

    [Header("Events")]
    public UnityEvent NewSave;

    // Start is called before the first frame update
    void Start()
    {
        // When opening up the game, it should check if there is a save or not.
        // If there is no save, then create a new save and open up the profile creation menu
        if(LoadGame() == false)
        {
            NewSave.Invoke();
            CreateSave();
        }
    }

    /// <summary>
    /// Save the current game
    /// </summary>
    public void SaveGame()
    {
        SerialisationManager.Save(playerSave);
    }


    /// <summary>
    /// Load the current game, returns bool if there is a save or not
    /// </summary>
    public bool LoadGame()
    {
        playerSave = (SaveData)SerialisationManager.Load();

        if (playerSave != null)
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Create a brand new save, and save it
    /// </summary>
    public void CreateSave()
    {
        playerSave = new SaveData();
    }
}
