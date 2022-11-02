using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIElement : MonoBehaviour
{

    [SerializeField] private EUpgradeType upgradeType;
    [SerializeField] private EUpgradeLevel upgradeLevel;
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private Button UIButton;
    [SerializeField] private UpgradeManager upgradeManager;

    private void Awake()
    {
        UIButton = GetComponent<Button>();
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();

        upgrade = upgradeManager.GetAnUpgrade((int)upgradeLevel, upgradeType);

        UIButton.onClick.AddListener(() => upgradeManager.ResearchAnUpgrade((int)upgradeLevel, upgradeType, UIButton));
        
    }

    private void Update()
    {
        UIButton.interactable = upgradeManager.GetAnUpgrade((int)upgradeLevel, upgradeType).Unlocked;
    }
}
