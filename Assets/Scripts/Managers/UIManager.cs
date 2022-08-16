using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Slider playerHealth_UI;
    public PlayerController playerController;
    public GameManager gameManager;
    public TextMeshProUGUI pointsUI;



    // Start is called before the first frame update
    void Start()
    {
        ResetHealthBar();
        DontDestroyOnLoad(this);
    }

    private void ResetHealthBar()
    {
        playerHealth_UI.maxValue = playerController.Health;
        playerHealth_UI.value = playerHealth_UI.maxValue;
    }

    public IEnumerator UpdatePlayerHealth_UI(float newHealth)
    {
        while(playerHealth_UI.value >= newHealth)
        {
            playerHealth_UI.value -= 10 * Time.deltaTime;
          
            yield return null; 
        }

        playerHealth_UI.value = Mathf.Clamp(playerHealth_UI.value, newHealth, playerHealth_UI.maxValue);
    }

    public void UpdatePoints(float newValue)
    {
        pointsUI.text = "Score: " + newValue;
    }

}
