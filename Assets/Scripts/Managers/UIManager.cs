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
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController playerController;

    // In Game UI
    [Header("In Game UI")]
    [SerializeField] private TextMeshProUGUI pointsUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI currentWeaponUI;
    [SerializeField] private Slider playerHealth_UI;

    // Death Screen UI
    [Header("Death Screen UI")]
    [SerializeField] private GameObject deathScreen_UI;
    [SerializeField] private Button playAgainBtn;
    [SerializeField] private Button quitToMenuBtn;

    [Header("Debugging")]
    [SerializeField] private bool skipMainMenu = false;



    // Start is called before the first frame update
    void Start()
    { 
        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;


        // If skip menu is true, then just load the level straight away
        if (skipMainMenu)
        {
            SceneManager.LoadScene(1);
        }

        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "MainScene")
        {
            Debug.Log("On Scene Loaded: " + scene.name);            
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

    private void FindDeathScreenUIElements()
    {
        deathScreen_UI = GameObject.Find("DeathScreenUI");
        

        playAgainBtn = GameObject.Find("PlayAgainBtn").GetComponent<Button>();
        playAgainBtn.onClick.AddListener(RestartGame);

        quitToMenuBtn = GameObject.Find("QuitToMenu").GetComponent<Button>();
        quitToMenuBtn.onClick.AddListener(QuitToMainMenu);

        deathScreen_UI.SetActive(false);
    }

    private void FindManagers()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    private void StartGame()
    {
        FindingInGameUIElements();
        FindManagers();
        FindDeathScreenUIElements();
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

    public void UpdatePlayerHealth_UI(float newHealth)
    {
      
       playerHealth_UI.value = playerController.Health;
          
     
        

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
        highScoreUI.text = newValue + ": High Score";
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

    #region Death Screen Functionality

    public void RestartGame()
    {
        Debug.Log("Restarting Game!");
        SceneManager.LoadScene(1);
    }

    public void QuitToMainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene(0);
    }

    public void OpenDeathScreen()
    {
        deathScreen_UI.SetActive(true);
    }

    #endregion


}
