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

    [SerializeField] private Image currentWeaponUI;
    [SerializeField] private Sprite kethcupBottle_Texture;
    [SerializeField] private Sprite mustardBottle_Texture;
    [SerializeField] private Sprite mayoBottle_Texture;

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
    private void FindingInGameUIElements()
    {
        pointsUI = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
        highScoreUI = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        currentWeaponUI = GameObject.Find("Current_Weapon_Image").GetComponent<Image>();
        playerHealth_UI = GameObject.Find("PlayerHealth").GetComponent<Slider>();
    }

    // Find the Death Screen elements. 
    private void FindDeathScreenUIElements()
    {
        deathScreen_UI = GameObject.Find("DeathScreenUI");

        // Finding and adding functionality to the play again btn
        playAgainBtn = GameObject.Find("PlayAgainBtn").GetComponent<Button>();
        playAgainBtn.onClick.AddListener(RestartGame);

        // Finding and adding functionality to the quit to menu btn
        quitToMenuBtn = GameObject.Find("QuitToMenu").GetComponent<Button>();
        quitToMenuBtn.onClick.AddListener(QuitToMainMenu);

        deathScreen_UI.SetActive(false);
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
