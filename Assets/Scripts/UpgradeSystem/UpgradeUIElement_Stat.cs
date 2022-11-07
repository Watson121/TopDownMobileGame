using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUIElement_Stat : MonoBehaviour
{

    [Header("Stat Settings")]
    [SerializeField] private Slider stat_Slider;
    [SerializeField] private TextMeshProUGUI stat_SliderText;
    [SerializeField] private float originalValue;

    public void ShowUpgrade(float upgradeValue)
    {
        // Saving the original value of the stat
        originalValue = stat_Slider.value;

        stat_Slider.value = upgradeValue;
        stat_SliderText.text = upgradeValue.ToString();
    }

    public void HideUpgrade()
    {
        stat_Slider.value = originalValue;
        stat_SliderText.text = originalValue.ToString();
    }

    public void ConfirmUpgade(float upgradeValue)
    {
        stat_Slider.value = upgradeValue;
        stat_SliderText.text = upgradeValue.ToString();
        originalValue = upgradeValue;
    }

}
