using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System.Linq;
using System;
using log4net.Core;

public class LevelCreator : EditorWindow
{



    [MenuItem("Window/UI Toolkit/LevelCreator")]
    public static void ShowExample()
    {
        LevelCreator wnd = GetWindow<LevelCreator>();
        wnd.titleContent = new GUIContent("LevelCreator");
    }

    private TwoPaneSplitView splitView;
    private ListView levelListView;
    private VisualElement m_leftPane;
    private VisualElement m_RightPane;
    private bool edit;

    private LevelManager levelManager;

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        levelManager = GameObject.FindObjectOfType<LevelManager>();

        

        Debug.Log(levelManager);

        splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        root.Add(splitView);

        levelListView = new ListView();
        splitView.Add(levelListView);
        PopulateLevelList();
        


        m_RightPane = new VisualElement();
        splitView.Add(m_RightPane);

    }

    private void OnLevelChange(IEnumerable<object> selectedItems)
    {
        Level selectedLevel = (Level)selectedItems.First();
        LoadLevel(selectedLevel);
    }

    private void PopulateLevelList()
    {
        levelListView.Clear();

        List<Level> levels = levelManager.levels;

        levelListView.makeItem = () => new Label();
        levelListView.bindItem = (item, index) => { (item as Label).text = levels[index].name; };
        levelListView.itemsSource = levels;
        levelListView.onSelectionChange += OnLevelChange;

        //NewLevelButton();
    }

    private void LoadLevel(Level selectedLevel, bool edit = false)
    {
        m_RightPane.Clear();

        LevelName(selectedLevel, edit);
        LevelDescirption(selectedLevel, edit);
        LevelBackground(selectedLevel, edit);
        IsBossLevel(selectedLevel, edit);
        EniemesToSpawn(selectedLevel, edit);

        if (!edit)
        {
            EditButton(selectedLevel);
        }
        else
        {
            SaveButton(selectedLevel);
            CancelButton(selectedLevel);
        }
    }


    private TextField levelName;

    private void LevelName(Level selectedLevel, bool edit = false)
    {
        levelName = new TextField("Level Name");
        levelName.value = selectedLevel.name;
        levelName.SetEnabled(edit);

        m_RightPane.Add(levelName);
    }

    private TextField levelDescription;

    private void LevelDescirption(Level selectedLevel, bool edit = false)
    {
        levelDescription = new TextField("Level Description");
        levelDescription.value = selectedLevel.description;
        levelDescription.SetEnabled(edit);

        m_RightPane.Add(levelDescription);
    }

    private EnumField levelBackgroundField;

    private void LevelBackground(Level selectedLevel, bool edit = false)
    {
        levelBackgroundField = new EnumField("Level Background", selectedLevel.levelBackground);
        levelBackgroundField.value = selectedLevel.levelBackground;
        levelBackgroundField.SetEnabled(edit);
        m_RightPane.Add(levelBackgroundField);
    }

    private Toggle bossToggle;
    private void IsBossLevel(Level selectedLevel, bool edit = false)
    {
        bossToggle = new Toggle("Boss Level?");
        bossToggle.value = false;
        bossToggle.SetEnabled(edit);

        m_RightPane.Add(bossToggle);
    }

    private IntegerField croutonShipValue;
    private IntegerField colourChaningShipValue;

    private void EniemesToSpawn(Level selectedlevel, bool edit = false)
    {
        int itemCount = selectedlevel.enemiesToSpawn.Count;
        List<EnemySetting> enemies = selectedlevel.enemiesToSpawn;

        var EnemyList = new Label("Enemies To Spawn");
        m_RightPane.Add(EnemyList);

        for(int i = 0; i < enemies.Count(); i++)
        {
            EnemySetting currentEnemy = enemies[i];

            var enemyField = new IntegerField(currentEnemy.GetName());
            enemyField.value = currentEnemy.numberToSpawn;
            enemyField.SetEnabled(edit);

            switch (currentEnemy.enemyType)
            {
                case EEnemyType.CroutonShip:
                    croutonShipValue = enemyField;
                    break;
                case EEnemyType.ColourChangingShip:
                    colourChaningShipValue = enemyField;
                    break;
            }

            m_RightPane.Add(enemyField);
        }     
    }

    private void EditButton(Level selectedLevel)
    {
        Action startEdit = () =>
        {
            LoadLevel(selectedLevel, true);
        };

        var editButton = new Button(startEdit) { text = "Edit Level" };
        m_RightPane.Add(editButton);
    }

    private void SaveButton(Level selectedLevel)
    {
        Action saveLevel = () =>
        {
            List<Level> levels = levelManager.levels;

            if (levels.Contains(selectedLevel))
            {
                int index = levelManager.levels.FindIndex(level => level.name == selectedLevel.name);
                Debug.Log("Level Index is: " + index);

                string newName = levelName.value;
                string newDescription = levelDescription.value;
                ELevelBackground newBackground = (ELevelBackground)levelBackgroundField.value;
                bool isBoss = bossToggle.value;
                List<EnemySetting> newEnemySettings = new List<EnemySetting>();
                newEnemySettings.Add(new EnemySetting(EEnemyType.CroutonShip, croutonShipValue.value));
                newEnemySettings.Add(new EnemySetting(EEnemyType.ColourChangingShip, colourChaningShipValue.value));

                Debug.Log("New Enemy Settings at index 0 is " + newEnemySettings[0].numberToSpawn
                    );

                Level updatedLevel = new Level(
                    newName,
                    newDescription,
                    isBoss,
                    newEnemySettings,
                    EBossType.None,
                    newBackground
                );

                levelManager.levels[index] = updatedLevel;


                PopulateLevelList();
                LoadLevel(updatedLevel, false);
              
            }

        };


        var saveButton = new Button(saveLevel) { text = "Save Level" };
        m_RightPane.Add(saveButton);
    }

    private void CancelButton(Level selectedLevel)
    {
        Action cancelEdit = () =>
        {
            LoadLevel(selectedLevel, false);
        };

        var cancelButton = new Button(cancelEdit) { text = "Cancel Edit" };
        m_RightPane.Add(cancelButton);
    }

    private void NewLevelButton()
    {

        Action newLevel = () =>
        {
            Debug.Log("New Level Created!");
        };

        var AddNewLevel = new Button(newLevel) { text = "Create New Level! " };
        splitView.Add(AddNewLevel);
    }


}