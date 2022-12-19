using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Upgrade Structs and Enums

public enum EUpgradeType
{
    Health,
    Weapons,
    Shield
}

public enum EUpgradeLevel
{
    Level_1,
    Level_2,
    Level_3,
    Level_4
}

public struct Upgrade
{
    // Type of upgrade that this upgrade is
    public EUpgradeType Type
    {
        get { return type; }
    }
    EUpgradeType type;
    
    // Current Level of this upgrade
    public EUpgradeLevel Level
    {
        get { return level; }
    } 
    EUpgradeLevel level;
    
    // Cost of this upgrade
    public int UpgradeCost
    {
        get { return upgradeCost; }
    }
    int upgradeCost;

    // Has this Upgrade been researched
    public bool Researched
    {
        set { researched = value; }
        get { return researched;  }
    }
    bool researched;

    // Has this upgrade been unlocked
    public bool Unlocked
    {
        set { locked = value; }
        get { return locked; }
    }
    private bool locked;

    public bool FinalUpgradeInTree
    {
        get { return finalUpgradeInTree; }
    }
    private bool finalUpgradeInTree;

    public Upgrade(EUpgradeType type, EUpgradeLevel level, int cost, bool locked = false, bool finalUpgrade = false)
    {
        this.type = type;
        this.level = level;
        this.upgradeCost = cost;    
        this.researched = false;
        this.locked = locked;
        this.finalUpgradeInTree = finalUpgrade;
    }

    public static bool operator! (Upgrade upgradeA)
    {
        return false;
    }


}

#endregion

[DisallowMultipleComponent]
public class UpgradeManager : MonoBehaviour
{

    #region Stopping Multiple Instances Upgrade Manager

    public static UpgradeManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if (instance != null)
            {
                Destroy(value.gameObject);
                return;
            }

            instance = value;
        }
    }
    private static UpgradeManager instance;

    #endregion


    /// <summary>
    ///  The Game Manager, where which level the player is currently on is going to be stored
    /// </summary>
    private GameManager gameManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Researched an upgrade that the player wants. This also where it will heck if the player has enough resources to research an upgrade or not
    /// </summary>
    /// <param name="upgradeIndex"> The Level that this upgrade is at </param>
    /// <param name="type"> Type of upgrade </param>
    /// <param name="btnUsed"> Upgrade Button Pressed </param>
    public Upgrade ResearchAnUpgrade(Upgrade upgradeToResearch)
    {
        if(gameManager.NumberOfGearsCollected >= upgradeToResearch.UpgradeCost)
        {
            gameManager.NumberOfGearsCollected -= upgradeToResearch.UpgradeCost;

            upgradeToResearch.Researched = true;
            upgradeToResearch.Unlocked = false;


            switch (upgradeToResearch.Type)
            {
                case EUpgradeType.Health:
                    gameManager.HealthLevel++;
                    break;
                case EUpgradeType.Weapons:
                    gameManager.WeaponLevel++;
                    break;
                case EUpgradeType.Shield:
                    gameManager.ShieldLevel++;
                    break;
            }

            return upgradeToResearch;
        }
       
        Debug.Log("Player cannot afford upgrade!");
        return upgradeToResearch;

    }


}
