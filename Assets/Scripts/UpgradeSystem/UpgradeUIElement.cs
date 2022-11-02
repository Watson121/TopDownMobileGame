using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIElement : MonoBehaviour
{

    [Header("Upgrade Settings")]
    [SerializeField] private EUpgradeType upgradeType;
    [SerializeField] private EUpgradeLevel upgradeLevel;
    [SerializeField] private int upgradeCost;
    [SerializeField] private bool unlocked;
    [SerializeField] private bool finalUpgradeInTree;
    [SerializeField] private Button nextButton;

    private Upgrade upgrade;
    
    private Button UIButton;
    private TextMeshProUGUI UI_ButtonText;

    private UpgradeManager upgradeManager;

    private void Awake()
    {
        UIButton = GetComponent<Button>();
        UI_ButtonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();

        upgrade = new Upgrade(upgradeType, upgradeLevel, upgradeCost, unlocked, finalUpgradeInTree);

        SetupButton();
        InteractionUpdate();



    }

    private void SetupButton()
    {

        // Setting up button clicks
        UIButton.onClick.AddListener(() => upgrade = upgradeManager.ResearchAnUpgrade(upgrade));
        UIButton.onClick.AddListener(InteractionUpdate);

        // Updating Button Text
        UI_ButtonText.text = upgradeCost.ToString();

    }

    private void InteractionUpdate()
    {
        UIButton.interactable = upgrade.Unlocked;

        if (upgrade.Researched && nextButton != null)
        {
            nextButton.interactable = true;
        }
    }


}
