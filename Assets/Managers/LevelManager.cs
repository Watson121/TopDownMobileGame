using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using System;

public enum ELevelBackground
{
    City
}

public enum EEnemyType
{
    CroutonShip,
    ColourChangingShip
}

public enum EBossType
{
    GeneralLoaf,
    None
}

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


[System.Serializable]
public struct Level
{
    public string name;
    public string description;
    public bool bossLevel;
    public List<EnemySetting> enemiesToSpawn;
    public EBossType bossToSpawn;
    public ELevelBackground levelBackground;


    public Level(List<EnemySetting> _enemiesToSpawn)
    {
        name = "NEW LEVEL";
        description = "LEVEL DESCRIPTION";
        bossLevel = false;
        enemiesToSpawn = _enemiesToSpawn;
        bossToSpawn = EBossType.None;
        levelBackground = ELevelBackground.City;

    }

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


    

[ExecuteInEditMode, Serializable]
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public List<Level> levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
