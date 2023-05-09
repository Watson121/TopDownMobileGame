using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : BaseUserInterface
{
    public GameObject RestartBtn
    {
        get { return restartBtn; }
    }


    [Header("Death Menu UI Elements")]
    [SerializeField] private GameObject restartBtn; 

    /// <summary>
    /// Restart the game
    /// </summary>
    public void RestartGame()
    {
        gameManager.ResetLevel();
        this.gameObject.SetActive(false);
    }
}
