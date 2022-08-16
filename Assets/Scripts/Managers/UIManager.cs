using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Slider playerHealth_UI;
    public PlayerController playerController;
    


    // Start is called before the first frame update
    void Start()
    {
        playerHealth_UI.maxValue = playerController.Health;
        playerHealth_UI.value = playerHealth_UI.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
