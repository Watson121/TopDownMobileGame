using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIManager : MonoBehaviour
{

    [Header("Game Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController playerController;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu_UI;
    [SerializeField] private Button startGameBtn_MainMenu;
    [SerializeField] private Button upgradeBtn_MainMenu;
    [SerializeField] private Button quitBtn_MainMenu;

    [Header("In Game UI")]
    [SerializeField] private TextMeshProUGUI pointsUI_GameUI;
    [SerializeField] private TextMeshProUGUI highScoreUI_GameUI;
    [SerializeField] private TextMeshProUGUI gearUI_GameUI;
    [SerializeField] private Image currentWeaponUI;
    private Sprite kethcupBottle_Texture;
    private Sprite mustardBottle_Texture;
    private Sprite mayoBottle_Texture;
    [SerializeField] private Slider playerHealth_UI;

    [Header("Death Screen UI")]
    [SerializeField] private GameObject deathScreen_UI;
    [SerializeField] private Button playAgainBtn_DeathScreen;
    [SerializeField] private Button quitToMenuBtn_DeathScreen;

    [Header("Pause Menu UI")]
    [SerializeField] private GameObject pauseMenu_UI;
    [SerializeField] private Button resumeGameBtn_PauseMenu;
    [SerializeField] private Button restartBtn_PauseMenu;
    [SerializeField] private Button quitToMenuBtn_PauseMenu;

    [Header("Upgrade Screen")]
    [SerializeField] private GameObject upgradeScreen_UI;
    [SerializeField] private Button doneBtn_UpgradeScreen;


    // Debugging controls
    [Header("Debugging")]
    [SerializeField] private bool skipMainMenu = false;

    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        FindResources();

       
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
     
        // If skip menu is true, then just load the level straight away
        if (skipMainMenu)
        {

            //SceneManager.LoadSceneAsync(1);
            
        }


    }

    // When a scene is loaded, make sure that the approriate functions are called
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

       

        Debug.Log("On Scene Loaded: " + scene.name);
        if (scene.name == "MainScene")
        {
            StartGame();
        }else if(scene.name == "UpgradeScreen")
        {
            SettingUpMainMenu();
            SettingUpUpgradeMenuUI();
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

    // Finding the and Setting up the Pause Menu UI Elements
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

    // Finding and setting up Upgrade Screen UI Elements
    private void SettingUpUpgradeMenuUI()
    {
        upgradeScreen_UI = GameObject.Find("UpgradeMenuUI");

        // Finding the setting up button so that the menu can be closed
        doneBtn_UpgradeScreen = GameObject.Find("DoneBtn_UpgradeScreen").GetComponent<Button>();
        doneBtn_UpgradeScreen.onClick.AddListener(() => ToggleMenu(upgradeScreen_UI));

        upgradeScreen_UI.SetActive(false);
    }

    // Finding and setting up Main Menu
    private void SettingUpMainMenu()
    {
        // Getting the Main Menu Object
        mainMenu_UI = GameObject.Find("MainMenu_UI");

        // Setting up the Play Button in the main menu
        startGameBtn_MainMenu = GameObject.Find("PlayBtn_MainMenu").GetComponent<Button>();
        startGameBtn_MainMenu.onClick.AddListener(PlayGame);

        // Setting up the upgrade button in the main menu, so that it can open the upgrade menu
        upgradeBtn_MainMenu = GameObject.Find("UpgradeBtn_MainMenu").GetComponent<Button>();
        upgradeBtn_MainMenu.onClick.AddListener(() => ToggleMenu(upgradeScreen_UI));

        // Setting up the Quit Button in the main menu
        quitBtn_MainMenu = GameObject.Find("ExitBtn_MainMenu").GetComponent<Button>();
        quitBtn_MainMenu.onClick.AddListener(ExitGame);

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

    private void StartGame(bool restart = false)
    {
        if (!restart)
        {
         
            SettingUpInGameUI();
            FindManagers();
            SettingUpDeathScreenUI();
            SettingUpPauseMenUI();
            //gameManager.SetupGame();
        }
        else
        {
            CloseAllMenus();
        }


        ResetHUD();
    }

    // Making sure all Menus are closed
    private void CloseAllMenus()
    {
        pauseMenu_UI.SetActive(false);
        deathScreen_UI.SetActive(false);
    }


    #region Game HUD

    // Reseting the Game HUD
    private void ResetHUD()
    {
        if (playerHealth_UI && playerController)
        {
            playerHealth_UI.maxValue = playerController.Health;
            playerHealth_UI.value = playerHealth_UI.maxValue;
        }

        UpdateCurrentPoints(0);
        UpdateGearCollection(0);

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
    public void UpdateGearCollection(float newValue)
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
        gameManager.ResetLevel();
        StartGame(true);
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

    /// <summary>
    /// A Function that will toggle the menu on or off.
    /// </summary>
    /// <param name="menuToClose"></param>
    public void ToggleMenu(GameObject menuToClose)
    {
        Debug.Log("Pressed Toggle Menu");
        menuToClose.SetActive(!menuToClose.activeSelf);
    }

    private void Update()
    {
        
    }


}
