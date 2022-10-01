using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{




    // Game Managers
    [Header("Game Managers")]
    public GameManager gameManager;
    public PlayerController playerController;

    // In Game UI
    [Header("In Game UI")]
    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI currentWeaponUI;
    public Slider playerHealth_UI;

    [Header("Debugging")]
    public bool skipMainMenu = false;



    // Start is called before the first frame update
    void Start()
    {
        /*ResetHealthBar();*/

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (skipMainMenu)
        {
            SceneManager.LoadScene(1);
            //FindingInGameUIElements();
        }

        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "MainScene")
        {
            Debug.Log("On Scene Loaded: " + scene.name);
            FindingInGameUIElements();
            StartGame();
        }

    }

    /// <summary>
    /// Finding the In Game UI Elements, when the game loads into the main scene
    /// </summary>
    private void FindingInGameUIElements()
    {
        pointsUI = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
        highScoreUI = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        currentWeaponUI = GameObject.Find("CurrentWeapon").GetComponent<TextMeshProUGUI>();
        playerHealth_UI = GameObject.Find("PlayerHealth").GetComponent<Slider>();
    }

    private void StartGame()
    {
        ResetHealthBar();
    }

    private void ResetHealthBar()
    {
        if (playerHealth_UI && playerController)
        {
            playerHealth_UI.maxValue = playerController.Health;
            playerHealth_UI.value = playerHealth_UI.maxValue;
        }
    }

    public IEnumerator UpdatePlayerHealth_UI(float newHealth)
    {
        while(playerHealth_UI.value >= newHealth)
        {
            playerHealth_UI.value -= 10 * Time.deltaTime;
          
            yield return null; 
        }

        playerHealth_UI.value = Mathf.Clamp(playerHealth_UI.value, newHealth, playerHealth_UI.maxValue);
    }

    // Updating the current points text
    public void UpdateCurrentPoints(float newValue)
    {
        pointsUI.text = "Score: " + newValue;
    }

    // Updating the high score text
    public void UpdateHighScore(float newValue)
    {
        
    }

    // Updating the current weapon text, to display what weapon is currently equiped
    public void UpdateCurrentWeapon(SauceType type)
    {
        if (currentWeaponUI)
        {
            switch (type)
            {
                case SauceType.Ketchup:
                    currentWeaponUI.text = "KETCHUP GUN";
                    break;
                case SauceType.Musturd:
                    currentWeaponUI.text = "MUSTURD GUN";
                    break;
                case SauceType.Mayo:
                    currentWeaponUI.text = "MAYO GUN";
                    break;
            }
        }
    }

    #region Main Menu Functionality

    public void PlayGame()
    {
        Debug.Log("Playing Game");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

}
