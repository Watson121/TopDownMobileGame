using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode, Serializable]
public class LevelSelectionBtn : MonoBehaviour
{

    [SerializeField]
    private Level currentLevel;

    [SerializeField]
    private TextMeshProUGUI levelButtonText;

    public void UpdateLevelButton(Level level)
    {
        currentLevel = level;
        levelButtonText.text = level.name;
    }

    
}
