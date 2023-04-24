using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrentPlayerProfile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentPlayerProfile;

    public void UpdateCurrentPlayerProfileText()
    {
        // currentPlayerProfile.text = ("Profile Name: " + SaveData.CurrentSave.playerName);
    }

}
