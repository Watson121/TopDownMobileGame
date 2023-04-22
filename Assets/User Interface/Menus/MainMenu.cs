using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : BaseUserInterface
{

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu_UI;
    [SerializeField] private Button startGameBtn_MainMenu;
    [SerializeField] private Button levelSelection_MainMenu;
    [SerializeField] private Button upgradeBtn_MainMenu;
    [SerializeField] private Button quitBtn_MainMenu;

}
