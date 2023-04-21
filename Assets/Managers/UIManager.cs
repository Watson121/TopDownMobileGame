using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class UIManager : MonoBehaviour
{

    #region Stopping Multiple Instances of UI Manager

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if(instance != null)
            {
                Destroy(value.gameObject);
                return;
            }

            instance = value;
        }
    }
    private static UIManager instance;

    #endregion

    [Header("Game Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HealthComponent playerHealth;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu_UI;
    [SerializeField] private Button startGameBtn_MainMenu;
    [SerializeField] private Button levelSelection_MainMenu;
    [SerializeField] private Button upgradeBtn_MainMenu;
    [SerializeField] private Button quitBtn_MainMenu;

    #region Level Selection
    private GameObject levelSelection_UI;
    private Button backButton_LevelSelection;
    private Button playButton_LevelSelection;
    #endregion

    [Header("In Game UI")]
    [SerializeField] private TextMeshProUGUI pointsUI_GameUI;
    [SerializeField] private TextMeshProUGUI highScoreUI_GameUI;
    [SerializeField] private TextMeshProUGUI gearUI_GameUI;
    [SerializeField] private Image currentWeaponUI;
    [SerializeField] private Slider playerHealth_UI;
    [SerializeField] private TextMeshProUGUI playerHealthText_UI;

    private Sprite kethcupBottle_Texture;
    private Sprite mustardBottle_Texture;
    private Sprite mayoBottle_Texture;

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
    [SerializeField] private TextMeshProUGUI gearUI_UpgradeScreen;

    [Header("Debugging")]
    [SerializeField] private bool skipMainMenu = false;

    [SerializeField] private EventSystem eventSystem;


    private void Awake()
    {
        Instance = this;
    }


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
            FindManagers();
            SettingUpLevelSelection();
            SettingUpUpgradeMenuUI();
            SettingUpMainMenu();
            
        }

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

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
        playerHealthText_UI = GameObject.Find("PlayerHealth_Text").GetComponent<TextMeshProUGUI>();
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
        doneBtn_UpgradeScreen.onClick.AddListener(() => ToggleMenu(mainMenu_UI));

        // Find the gear UI in upgrade Screen
        gearUI_UpgradeScreen = GameObject.Find("Gears_UpgradeScreen").GetComponent<TextMeshProUGUI>();
        SetGearsUpgradeScreen();

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

        // Setting up Level Selection Button in the main menu
        levelSelection_MainMenu = GameObject.Find("LevelSelection_MainMenu").GetComponent<Button>();
        levelSelection_MainMenu.onClick.AddListener(() => ToggleMenu(levelSelection_UI));
        levelSelection_MainMenu.onClick.AddListener(() => ToggleMenu(mainMenu_UI));

        // Setting up the upgrade button in the main menu, so that it can open the upgrade menu
        upgradeBtn_MainMenu = GameObject.Find("UpgradeBtn_MainMenu").GetComponent<Button>();
        upgradeBtn_MainMenu.onClick.AddListener(() => ToggleMenu(upgradeScreen_UI));
        upgradeBtn_MainMenu.onClick.AddListener(() => ToggleMenu(mainMenu_UI));
        upgradeBtn_MainMenu.onClick.AddListener(() => eventSystem.SetSelectedGameObject(doneBtn_UpgradeScreen.gameObject));

        // Setting up the Quit Button in the main menu
        quitBtn_MainMenu = GameObject.Find("ExitBtn_MainMenu").GetComponent<Button>();
        quitBtn_MainMenu.onClick.AddListener(ExitGame);

    }

    private void SettingUpLevelSelection()
    {
        // Getting the Level Selection Menu Object
        levelSelection_UI = GameObject.Find("LevelSelectionUI");

        // Setting up the Play Button in the Level Selection Menu
        playButton_LevelSelection = GameObject.Find("PlayButton_LevelSelectionUI").GetComponent<Button>();
        playButton_LevelSelection.onClick.AddListener(PlayGame);
        

        // Setting up the Back Button in the Level Selection Menu
        backButton_LevelSelection = GameObject.Find("BackButton_LevelSelectionUI").GetComponent<Button>();
        backButton_LevelSelection.onClick.AddListener(() => ToggleMenu(levelSelection_UI));
        backButton_LevelSelection.onClick.AddListener(() => ToggleMenu(mainMenu_UI));

        #region Setting Up Level Selection Buttons

        GameObject[] levelButtons = gameManager.LevelManager.GetListOfContents();

        foreach(GameObject levelButton in levelButtons)
        {
            Button button = levelButton.GetComponent<Button>();
            LevelSelectionBtn levelSelectionBtn = levelButton.GetComponent<LevelSelectionBtn>();

            button.onClick.AddListener(() => gameManager.LevelManager.SelectedLevelPanel(levelSelectionBtn.CurrentLevel));
            button.onClick.AddListener(() => gameManager.CurrentLevel = levelSelectionBtn.CurrentLevel);
        }

       

        #endregion


        levelSelection_UI.SetActive(false);
    }

    // Find the in game managers
    private void FindManagers()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    private void FindPlayer()
    {
        playerHealth = GameObject.Find("Player").GetComponent<HealthComponent>();
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
            FindPlayer();
            SettingUpInGameUI();
            SettingUpDeathScreenUI();
            SettingUpPauseMenUI();
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
        if (playerHealth_UI && playerHealth)
        {
            playerHealth_UI.maxValue = playerHealth.Health;
            playerHealth_UI.value = playerHealth_UI.maxValue;
            playerHealthText_UI.text = playerHealth.MaxHealth.ToString();
        }

        UpdateCurrentPoints(0);
        UpdateGearCollection(0);

    }

    // Updating Player Health Radial
    public void UpdatePlayerHealth_UI(float newHealth)
    {   
       playerHealth_UI.value = playerHealth.Health;
       playerHealth_UI.value = Mathf.Clamp(playerHealth_UI.value, newHealth, playerHealth_UI.maxValue);
       playerHealthText_UI.text = newHealth.ToString();
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

    public void LevelSelection()
    {
        Debug.Log("Level Selection Opened");
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
        gameManager.ResetLevel(true);
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
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

    #region Upgrade Screen

    public void SetGearsUpgradeScreen()
    {
        gearUI_UpgradeScreen.text = "Gears: " + gameManager.NumberOfGearsCollected.ToString();
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



}
