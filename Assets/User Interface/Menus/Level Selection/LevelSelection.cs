using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : BaseUserInterface
{


    [SerializeField] GameObject[] levelButtons;

    /// <summary>
    /// Setting up the UI for the Level Selection
    /// </summary>
    protected override void SetupUI()
    {
        CreatingLevelButtons();

        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Creating the list of levels for the player to select from
    /// </summary>
    private void CreatingLevelButtons()
    {
        levelButtons = levelManager.GetListOfContents();

        foreach(GameObject levelButton in levelButtons)
        {
            Button button = levelButton.GetComponent<Button>();
            LevelSelectionBtn levelSelectionBtn = levelButton.GetComponent<LevelSelectionBtn>();

            button.onClick.AddListener(() => levelManager.PlayScene("MainScene"));
        }
    }

    /// <summary>
    /// When this object is destroyed make sure to clear the listeners
    /// </summary>
    private void OnDestroy()
    {
        foreach (GameObject levelButton in levelButtons)
        {
            Button button = levelButton.GetComponent<Button>();
            button.onClick.RemoveListener(() => levelManager.PlayScene("MainScene"));
        }
    }
}
