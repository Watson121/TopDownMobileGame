using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIElement : MonoBehaviour
{

    [SerializeField] private EUpgradeType upgradeType;
    [SerializeField] private EUpgradeLevel upgradeLevel;
    [SerializeField] private Button UIButton;
    [SerializeField] private UpgradeManager upgradeManager;

    private void Awake()
    {
        UIButton = GetComponent<Button>();
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();

        if(UIButton.interactable == true) { 
            UIButton.onClick.AddListener(() => upgradeManager.SetAnUpgradeToResearched(upgradeLevel, upgradeType, UIButton));
        }
    }




}
