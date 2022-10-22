using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    EUpgradeType type;
    EUpgradeLevel level;
    int upgradeCost;
    bool researched;

    public Upgrade(EUpgradeType type, EUpgradeLevel level, int cost)
    {
        this.type = type;
        this.level = level;
        this.upgradeCost = cost;    
        this.researched = false;
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

    private Upgrade[] weaponUpgrades = { new Upgrade(EUpgradeType.Weapons, EUpgradeLevel.Level_1, 500),
                                         new Upgrade(EUpgradeType.Weapons, EUpgradeLevel.Level_2, 1500),
                                         new Upgrade(EUpgradeType.Weapons, EUpgradeLevel.Level_3, 2250)};

    private Upgrade[] shieldUpgradse = { new Upgrade(EUpgradeType.Shield, EUpgradeLevel.Level_1, 600),
                                         new Upgrade(EUpgradeType.Shield, EUpgradeLevel.Level_2, 1200),
                                         new Upgrade(EUpgradeType.Shield, EUpgradeLevel.Level_3, 1800)};

}
