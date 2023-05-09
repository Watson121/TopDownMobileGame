using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseUserInterface
{
    public GameObject ResumeBtn 
    {
        get { return resumeBtn; }
    }


    [Header("Pause Menu UI Elements")]
    [SerializeField] private GameObject resumeBtn;


    /// <summary>
    /// Restarting the game
    /// </summary>
    public void RestartGame()
    {
        gameManager.ResetLevel();
        this.gameObject.SetActive(false);
    }



}
