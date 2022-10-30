using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Upgrade(EUpgradeType type, EUpgradeLevel level, int cost)
    {
        this.type = type;
        this.level = level;
        this.upgradeCost = cost;    
        this.researched = false;
    }

    public static bool operator! (Upgrade upgradeA)
    {
        return false;
    }


}


public class UpgradeManager : MonoBehaviour
{

    /// <summary>
    /// Health Upgrades
    /// </summary>
    private Upgrade[] healthUpgrades = { new Upgrade(EUpgradeType.Health, EUpgradeLevel.Level_1, 1000),
                                         new Upgrade(EUpgradeType.Health, EUpgradeLevel.Level_2, 2000),
                                         new Upgrade(EUpgradeType.Health, EUpgradeLevel.Level_3, 3000),
                                         new Upgrade(EUpgradeType.Health, EUpgradeLevel.Level_4, 4000)};

    /// <summary>
    /// Weapon Upgrades
    /// </summary>
    private Upgrade[] weaponUpgrades = { new Upgrade(EUpgradeType.Weapons, EUpgradeLevel.Level_1, 500),
                                         new Upgrade(EUpgradeType.Weapons, EUpgradeLevel.Level_2, 1500),
                                         new Upgrade(EUpgradeType.Weapons, EUpgradeLevel.Level_3, 2250)};

    /// <summary>
    /// Sheild Upgrades
    /// </summary>
    private Upgrade[] shieldUpgrades = { new Upgrade(EUpgradeType.Shield, EUpgradeLevel.Level_1, 600),
                                         new Upgrade(EUpgradeType.Shield, EUpgradeLevel.Level_2, 1200),
                                         new Upgrade(EUpgradeType.Shield, EUpgradeLevel.Level_3, 1800)};

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DontDestroyOnLoad(this);
    }


    private Upgrade GetAnUpgrade(int index, EUpgradeType type)
    {
        Upgrade upgradeToReturn = healthUpgrades[0];

        switch (type)
        {
            case EUpgradeType.Health:
                Debug.Log("Health Upgrade Retuned: " + healthUpgrades[index].Level);
                upgradeToReturn = healthUpgrades[index];
                break;
            case EUpgradeType.Shield:
                upgradeToReturn = shieldUpgrades[index];
                break;
            case EUpgradeType.Weapons:
                upgradeToReturn = weaponUpgrades[index];
                break;
        }

        return upgradeToReturn;
    }

    public void SetAnUpgradeToResearched(EUpgradeLevel index, EUpgradeType type, Button btnUsed)
    {
        Upgrade upgrade = GetAnUpgrade((int)index, type);

        if(gameManager.NumberOfGearsCollected >= upgrade.UpgradeCost)
        {
            Debug.Log("Player can afford upgrade");
            upgrade.Researched = true;
            btnUsed.interactable = false;
            return;
        }
        else
        {
            Debug.Log("Player cannot afford upgrade!");
            return;
        }

       
    }


}
