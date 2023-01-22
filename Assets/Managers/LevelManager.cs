using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using UnityEngine.UI;
using TMPro;
using System;

#region Level Settings

/// <summary>
/// The background that the level will have
/// </summary>
public enum ELevelBackground
{
    City
}

/// <summary>
/// Enemy To Spawn in the level. Multiple enemies can be choosen for the level
/// </summary>
public enum EEnemyType
{
    CroutonShip,
    ColourChangingShip
}

/// <summary>
/// Boss to Spawn in the level
/// </summary>
public enum EBossType
{
    GeneralLoaf,
    None
}

#endregion

/// <summary>
/// Enemy Setting Data Type that is used by the level creator when deciding what enemies you want to be in a level
/// </summary>
[System.Serializable]
public struct EnemySetting {
    public EEnemyType enemyType;
    public int numberToSpawn;

    public EnemySetting(EEnemyType _enemyType, int _numberToSpawn)
    {
        enemyType = _enemyType;
        numberToSpawn = _numberToSpawn;
    }

    public string GetName()
    {
        switch(enemyType)
        {
            case EEnemyType.CroutonShip:
                return "Crouton Ship";
            case EEnemyType.ColourChangingShip:
                return "Colour Changing Ship";
            default:
                return "";
        }
    }
}


/// <summary>
/// This is a new data type that holds all of the info for the current level
/// </summary>
[System.Serializable]
public struct Level
{
    public string name;
    public string description;
    public bool bossLevel;
    public List<EnemySetting> enemiesToSpawn;
    public EBossType bossToSpawn;
    public ELevelBackground levelBackground;


    /// <summary>
    /// Base Contstructor For a Level
    /// </summary>
    public Level(List<EnemySetting> _enemiesToSpawn)
    {
        name = "NEW LEVEL";
        description = "LEVEL DESCRIPTION";
        bossLevel = false;
        enemiesToSpawn = _enemiesToSpawn;
        bossToSpawn = EBossType.None;
        levelBackground = ELevelBackground.City;

    }

    /// <summary>
    /// Constructor that is used to create a brand new level
    /// </summary>
    public Level(string _name, string _description, bool _bossLevel, List<EnemySetting> _enemiesToSpawn, EBossType _bossToSpawn, ELevelBackground _levelBackground)
    {
        name = _name;
        description = _description;
        bossLevel = _bossLevel;
        enemiesToSpawn = _enemiesToSpawn;
        levelBackground = _levelBackground;

        if (bossLevel)
        {
            bossToSpawn = _bossToSpawn;
        }
        else
        {
            bossToSpawn = EBossType.None;
        }
    }

    
}


    

[ExecuteAlways, Serializable, DisallowMultipleComponent]
public class LevelManager : MonoBehaviour
{

    #region Stopping Multiple Instances of the Game Manager

    public static LevelManager Instance
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
    private static LevelManager instance;

    #endregion

    [SerializeField]
    public List<Level> levels;

    public Level CurrentLevel
    {
        set { currentLevel = value; }
        get { return currentLevel; }
    }
    /// <summary>
    /// Current Level being played
    /// </summary>
    private Level currentLevel;


    [SerializeField]
    private ScrollRect levelList_ui;

    [SerializeField]
    private GameObject levelButton;

    #region Level Selection Panel


    [SerializeField] private TextMeshProUGUI levelTitle;
    [SerializeField] private Image levelImage;
    [SerializeField] private TextMeshProUGUI levelDescription;


    #endregion


  

    /// <summary>
    /// Updating the level list of the buttons
    /// </summary>
    public void UpdateLevelList_UI()
    {
        foreach (Level level in levels)
        {
            GameObject newLevelButton = Instantiate(levelButton, levelList_ui.content);
            newLevelButton.GetComponent<LevelSelectionBtn>().UpdateLevelButton(level);
        }

        SelectedLevelPanel(levels[0]);
    }

    /// <summary>
    /// Clearing the Level List, and destroying the buttons
    /// </summary>
    public void ClearLevelList()
    {
        var tempArray = new GameObject[levelList_ui.content.childCount];

        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = levelList_ui.content.transform.GetChild(i).gameObject;
        }

        foreach (var child in tempArray)
        {
            DestroyImmediate(child);
        }
    }

    /// <summary>
    /// Getting the List of Buttons
    /// </summary>
    public GameObject[] GetListOfContents()
    {
        var tempArray = new GameObject[levelList_ui.content.childCount];

        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = levelList_ui.content.transform.GetChild(i).gameObject;
        }

        return tempArray;
    }

    /// <summary>
    /// Updating the Level Panel, with the currently selected level
    /// </summary>
    public void SelectedLevelPanel(Level level)
    {
        levelTitle.text = level.name;
        levelDescription.text = level.description; 
    }
    
}
