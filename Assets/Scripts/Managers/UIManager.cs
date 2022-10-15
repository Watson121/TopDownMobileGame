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
    [SerializeField] private TextMeshProUGUI pointsUI_GameUI;
    [SerializeField] private TextMeshProUGUI highScoreUI_GameUI;
    [SerializeField] private TextMeshProUGUI gearUI_GameUI;

    [SerializeField] private Image currentWeaponUI;
    private Sprite kethcupBottle_Texture;
    private Sprite mustardBottle_Texture;
    private Sprite mayoBottle_Texture;
    [SerializeField] private Slider playerHealth_UI;

    // Death Screen UI
    [Header("Death Screen UI")]
    [SerializeField] private GameObject deathScreen_UI;
    [SerializeField] private Button playAgainBtn_DeathScreen;
    [SerializeField] private Button quitToMenuBtn_DeathScreen;

    [Header("Pause Menu UI")]
    [SerializeField] private GameObject pauseMenu_UI;
    [SerializeField] private Button resumeGameBtn_PauseMenu;
    [SerializeField] private Button restartBtn_PauseMenu;
    [SerializeField] private Button quitToMenuBtn_PauseMenu;

    // Debugging controls
    [Header("Debugging")]
    [SerializeField] private bool skipMainMenu = false;

    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        FindResources();

        SceneManager.sceneLoaded += OnSceneLoaded;


        // If skip menu is true, then just load the level straight away
        if (skipMainMenu)
        {
            SceneManager.LoadScene(1);
        }


    }

    // When a scene is loaded, make sure that the approriate functions are called
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
    private void SettingUpInGameUI()
    {
        pointsUI_GameUI = GameObject.Find("PlayerScore_GameUI").GetComponent<TextMeshProUGUI>();
        highScoreUI_GameUI = GameObject.Find("HighScore_GameUI").GetComponent<TextMeshProUGUI>();
        gearUI_GameUI = GameObject.Find("GearAmount_GameUI").GetComponent<TextMeshProUGUI>();

        currentWeaponUI = GameObject.Find("Current_Weapon_Image").GetComponent<Image>();
        playerHealth_UI = GameObject.Find("PlayerHealth").GetComponent<Slider>();
    }

    // Find the Death Screen elements. 
    private void SettingUpDeathScreenUI()
    {
        deathScreen_UI = GameObject.Find("DeathScreenUI");

        // Finding and adding functionality to the play again btn
        playAgainBtn_DeathScreen = GameObject.Find("PlayAgainBtn_DeathScreen").GetComponent<Button>();
        playAgainBtn_DeathScreen.onClick.AddListener(RestartGame);

        // Finding and adding functionality to the quit to menu btn
        quitToMenuBtn_DeathScreen = GameObject.Find("QuitToMenu_DeathScreen").GetComponent<Button>();
        quitToMenuBtn_DeathScreen.onClick.AddListener(QuitToMainMenu);

        deathScreen_UI.SetActive(false);
    }

    private void SettingUpPauseMenUI()
    {
        pauseMenu_UI = GameObject.Find("PauseMenuUI");

        // Finding and setting up functionlity of the pause menu button
        resumeGameBtn_PauseMenu = GameObject.Find("ResumeBtn_PauseMenu").GetComponent<Button>();
        resumeGameBtn_PauseMenu.onClick.AddListener(ResumeGame);
        resumeGameBtn_PauseMenu.onClick.AddListener(OpenPauseMenu);

        // Finding and setting up functionality of the restart game btn
        restartBtn_PauseMenu = GameObject.Find("RestartBtn_PauseMenu").GetComponent<Button>();
        restartBtn_PauseMenu.onClick.AddListener(RestartGame);

        // Finding and adding functionality to the quit to menu btn
        quitToMenuBtn_PauseMenu = GameObject.Find("QuitToMenu_PauseMenu").GetComponent<Button>();
        quitToMenuBtn_PauseMenu.onClick.AddListener(QuitToMainMenu);

        pauseMenu_UI.SetActive(false);
    }

    // Find the in game managers
    private void FindManagers()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Finding the textures within the resources folder
    private void FindResources()
    {
        kethcupBottle_Texture = Resources.Load("Textures/Weapon Display/KetchupBottle", typeof(Sprite)) as Sprite;
        mustardBottle_Texture = Resources.Load("Textures/Weapon Display/MustardBottle", typeof(Sprite)) as Sprite;
        mayoBottle_Texture = Resources.Load("Textures/Weapon Display/MayoBottle", typeof(Sprite)) as Sprite;
    }

    private void StartGame()
    {
        SettingUpInGameUI();
        FindManagers();
        SettingUpDeathScreenUI();
        SettingUpPauseMenUI();
        ResetHealthBar();
    }


    #region Game HUD

    // Reseting the Health Bar
    private void ResetHealthBar()
    {
        if (playerHealth_UI && playerController)
        {
            playerHealth_UI.maxValue = playerController.Health;
            playerHealth_UI.value = playerHealth_UI.maxValue;
        }
    }

    // Updating Player Health Radial
    public void UpdatePlayerHealth_UI(float newHealth)
    {   
       playerHealth_UI.value = playerController.Health;
       playerHealth_UI.value = Mathf.Clamp(playerHealth_UI.value, newHealth, playerHealth_UI.maxValue);
    }

    // Updating the current points text
    public void UpdateCurrentPoints(float newValue)
    {
        pointsUI_GameUI.text = "Score: " + newValue;
    }

    // Updating the high score text
    public void UpdateHighScore(float newValue)
    {
        highScoreUI_GameUI.text = "High Score : " + newValue;
    }

    // Updating the number of gears collected
    public void UpadateGearsCollection(float newValue)
    {
        gearUI_GameUI.text = "Gear: " + newValue;
    }

    // Updating the current weapon text, to display what weapon is currently equiped
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

    #region Death Screen & Pause Menu Functionality

    public void ResumeGame()
    {
        Debug.Log("Resuming Game");
        Time.timeScale = 1;
    }

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

    // Opening or closing the pause menu
    public void OpenPauseMenu()
    {
        pauseMenu_UI.SetActive(!pauseMenu_UI.activeSelf);
    }

    #endregion




}
