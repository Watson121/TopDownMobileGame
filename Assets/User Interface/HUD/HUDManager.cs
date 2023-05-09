using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{


    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI pointsUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI gearUI;
    [SerializeField] private Image currentWeaponUI;
    [SerializeField] private Slider playerHealthRadialUI;
    [SerializeField] private TextMeshProUGUI playerHealthTextUI;

    [Header("Menus")]
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private DeathMenu deathMenu;

    [Header("Weapon Sprites")]
    [SerializeField] private Sprite kethcupBottle_Texture;
    [SerializeField] private Sprite mustardBottle_Texture;
    [SerializeField] private Sprite mayoBottle_Texture;

    [Header("Event System")]
    [SerializeField] private EventSystem eventSystem;

    #region Health

    /// <summary>
    /// Updating the Player Health Radial and Text
    /// </summary>
    /// <param name="newHealth"></param>
    public void UpdatePlayerHealth(float newHealth)
    {
        playerHealthRadialUI.value = newHealth;
        playerHealthRadialUI.value = Mathf.Clamp(playerHealthRadialUI.value, newHealth, playerHealthRadialUI.maxValue);
        playerHealthTextUI.text = newHealth.ToString();
    }

    #endregion

    #region Points

    /// <summary>
    /// Updating the current points that the player has gained
    /// </summary>
    /// <param name="newValue"> new points value </param>
    public void UpdateCurrentPoints(float newValue)
    {
        pointsUI.text = "Score: " + newValue;
    }

    /// <summary>
    /// Updating the high score points that the player has gained
    /// </summary>
    /// <param name="newValue"> new high score value </param>
    public void UpdateHighScore(float newValue)
    {
        highScoreUI.text = "High Score : " + newValue;
    }

    #endregion

    #region Gears

    /// <summary>
    /// Updating the number of gears that the player has collected
    /// </summary>
    /// <param name="newValue"> new gear value </param>
    public void UpdateGearCollection(float newValue)
    {
        gearUI.text = "Gear: " + newValue;
    }

    #endregion

    #region Weapons

    /// <summary>
    /// Update the current weapon that the player has equiped
    /// </summary>
    /// <param name="type"> new current weapon </param>
    public void UpdateCurrentWeapon(SauceType type)
    {
        if (currentWeaponUI)
        {
            switch (type)
            {
                case SauceType.Ketchup:
                    Debug.Log("Ketchup Gun Equiped");
                    currentWeaponUI.sprite = kethcupBottle_Texture;
                    break;
                case SauceType.Musturd:
                    Debug.Log("Mustard Gun Equiped");
                    currentWeaponUI.sprite = mustardBottle_Texture;
                    break;
                case SauceType.Mayo:
                    Debug.Log("Mayo Gun Equiped");
                    currentWeaponUI.sprite = mayoBottle_Texture;
                    break;
            }
        }
    }

    #endregion

    #region Menus

    /// <summary>
    /// Toggling the Pause Menu open or close
    /// </summary>
    public void TogglePauseMenu()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        eventSystem.SetSelectedGameObject(pauseMenu.ResumeBtn.gameObject);
    }

    public void ToggleDeathMenu()
    {
        deathMenu.gameObject.SetActive(!deathMenu.gameObject.activeSelf);
        eventSystem.SetSelectedGameObject(deathMenu.RestartBtn.gameObject);
    }


    #endregion

}
