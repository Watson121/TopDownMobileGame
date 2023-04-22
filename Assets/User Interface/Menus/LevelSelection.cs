using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class LevelSelection : BaseUserInterface
{

    private LevelManager levelManager;

    [Header("Level Selection")]
    [SerializeField] private GameObject levelSelection_UI;
    [SerializeField] private Button backButton_LevelSelection;
    [SerializeField] private Button playButton_LevelSelection;

    private void Start()
    {

        // Getting the managers
        base.Start();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();


        SetupUI();
     
    }


    /// <summary>
    /// Settting up the Level Selection Screen
    /// </summary>
    protected void SetupUI()
    {
        Debug.Log("Setup Level Selection Function Called");

        GameObject[] levelButtons = levelManager.GetListOfContents();

        foreach (GameObject levelButton in levelButtons)
        {
            Button button = levelButton.GetComponent<Button>();
            LevelSelectionBtn levelSelectionBtn = levelButton.GetComponent<LevelSelectionBtn>();

            button.onClick.AddListener(() => gameManager.LevelManager.SelectedLevelPanel(levelSelectionBtn.CurrentLevel));
            button.onClick.AddListener(() => gameManager.CurrentLevel = levelSelectionBtn.CurrentLevel);
        }

        levelSelection_UI.SetActive(false);

        base.SetupUI();
    }
}
