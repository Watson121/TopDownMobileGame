using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : BaseUserInterface
{

    [Header("Upgrade Menu UI Elements")]
    [SerializeField] private TextMeshProUGUI gearUI;

    /// <summary>
    /// Setting up the UI in the Upgrade Menu
    /// </summary>
    protected override void SetupUI()
    {
        gearUI.text = "Gears: " + gameManager.NumberOfGearsCollected.ToString();
        this.gameObject.SetActive(false);
    }
}
