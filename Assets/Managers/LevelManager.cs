using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

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
public struct Level
{
    public string name;
    public string description;
    public bool bossLevel;
    public Dictionary<EEnemyType, int> enemiesToSpawn;
    public EBossType bossToSpawn;

    public Level(string _name, string _description, bool _bossLevel, Dictionary<EEnemyType, int> _enemiesToSpawn, EBossType _bossToSpawn)
    {
        name = _name;
        description = _description;
        bossLevel = _bossLevel;
        enemiesToSpawn = _enemiesToSpawn;

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


    

[ExecuteInEditMode]
public class LevelManager : MonoBehaviour
{

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
