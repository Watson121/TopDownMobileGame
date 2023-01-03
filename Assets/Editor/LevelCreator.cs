using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System.Linq;
using System;

public class LevelCreator : EditorWindow
{



    [MenuItem("Window/UI Toolkit/LevelCreator")]
    public static void ShowExample()
    {
        LevelCreator wnd = GetWindow<LevelCreator>();
        wnd.titleContent = new GUIContent("LevelCreator");
    }

    private VisualElement m_RightPane;



    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
        List<Level> levels = levelManager.levels;
        
        Debug.Log(levelManager);

        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        root.Add(splitView);

        var leftPane = new ListView();
        splitView.Add(leftPane);

        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = levels[index].name; };
        leftPane.itemsSource = levels;
        leftPane.onSelectionChange += OnLevelChange;



        m_RightPane = new VisualElement();
        splitView.Add(m_RightPane);

    }

    private void OnLevelChange(IEnumerable<object> selectedItems)
    {
        m_RightPane.Clear();

        Level selectedLevel = (Level)selectedItems.First();


        LevelName(selectedLevel);
        LevelDescirption(selectedLevel);
        IsBossLevel(selectedLevel);
    }

    private void LevelName(Level selectedLevel, bool edit = false)
    {
        var levelName = new TextField("Level Name");
        levelName.value = selectedLevel.name;
        levelName.SetEnabled(edit);

        m_RightPane.Add(levelName);
    }

    private void LevelDescirption(Level selectedLevel, bool edit = false)
    {
        var levelDescription = new TextField("Level Description");
        levelDescription.value = selectedLevel.description;
        levelDescription.SetEnabled(edit);

        m_RightPane.Add(levelDescription);
    }

    private void IsBossLevel(Level selectedLevel, bool edit = false)
    {
        var bossToggle = new Toggle("Boss Level?");
        bossToggle.value = false;
        bossToggle.SetEnabled(edit);

        m_RightPane.Add(bossToggle);
    }
    
    private void EniemesToSpawn(Level selectedlevel, bool edit = false)
    {
        int itemCount = selectedlevel.enemiesToSpawn.Count;
        var enemies = selectedlevel.enemiesToSpawn;

        var border = new Bor

    }

}