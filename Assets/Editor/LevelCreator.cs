/// Level Creator - Allows me to quickly create levels on the fly without having to do it all manually. 
/// Also allows me to easily edit and delete levels. 
/// 
///



using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelCreator : EditorWindow
{


    private LevelManager levelManager;

    #region Level Creator UI Elements

    private TwoPaneSplitView splitView;
    private ListView levelListView;
    private VisualElement m_leftPane;
    private VisualElement m_RightPane;
    private bool edit;

    #endregion
   
    #region Level Creator Parameters

    private TextField levelName;
    private TextField levelDescription;
    private EnumField levelBackgroundField;
    private Toggle bossToggle;
    private IntegerField croutonShipValue;
    private IntegerField colourChaningShipValue;
    private EnumField bossSelection;

    #endregion

    [MenuItem("Window/UI Toolkit/LevelCreator")]
    public static void ShowExample()
    {
        LevelCreator wnd = GetWindow<LevelCreator>();
        wnd.titleContent = new GUIContent("Level Creator");
    }


    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Finding the level manager
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        // Creating the split view
        splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        root.Add(splitView);

        // Creating a list view to hold the levels in the game
        levelListView = new ListView();
        splitView.Add(levelListView);
        PopulateLevelList();
        NewLevelButton();

        // Right Pane will be display the parameters that I can view/edit
        m_RightPane = new VisualElement();
        splitView.Add(m_RightPane);

    }

    /// <summary>
    /// Called when you change the level that you're looking at
    /// </summary>
    private void OnLevelChange(IEnumerable<object> selectedItems)
    {
        Level selectedLevel = (Level)selectedItems.First();
        LoadLevel(selectedLevel);
    }

    /// <summary>
    /// Populates the level list with all of the different levels currently in the game
    /// </summary>
    private void PopulateLevelList()
    {
        levelListView.Clear();

        List<Level> levels = levelManager.levels;

        levelListView.makeItem = () => new Label();
        levelListView.bindItem = (item, index) => { (item as Label).text = levels[index].name; };
        levelListView.itemsSource = levels;
        levelListView.onSelectionChange += OnLevelChange;

   
        
    }

    /// <summary>
    /// When a level is selected, it's parameters are loaded in the right pane so that you can view them.
    /// If you have selected edit mode you will be able to edit them.
    /// </summary>
    private void LoadLevel(Level selectedLevel, bool edit = false)
    {
        m_RightPane.Clear();

        LevelName(selectedLevel, edit);
        LevelDescirption(selectedLevel, edit);
        LevelBackground(selectedLevel, edit);
        IsBossLevel(selectedLevel, edit);

        if (bossToggle.value)
        {
            BossToSpawn(selectedLevel, edit);
        }
        
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

        DeleteLevel(selectedLevel);
    }


    /// <summary>
    /// Give the Level a Name
    /// </summary>
    private void LevelName(Level selectedLevel, bool edit = false)
    {
        levelName = new TextField("Level Name");
        levelName.value = selectedLevel.name;
        levelName.SetEnabled(edit);

        m_RightPane.Add(levelName);
    }

    /// <summary>
    /// Give the Level a Description
    /// </summary>
    private void LevelDescirption(Level selectedLevel, bool edit = false)
    {
        levelDescription = new TextField("Level Description");
        levelDescription.value = selectedLevel.description;
        levelDescription.SetEnabled(edit);

        m_RightPane.Add(levelDescription);
    }

 
    /// <summary>
    /// Choose what type of background that the level will have
    /// </summary>
    private void LevelBackground(Level selectedLevel, bool edit = false)
    {
        levelBackgroundField = new EnumField("Level Background", selectedLevel.levelBackground);
        levelBackgroundField.value = selectedLevel.levelBackground;
        levelBackgroundField.SetEnabled(edit);
        m_RightPane.Add(levelBackgroundField);
    }

 
    /// <summary>
    /// Is this level a boss level or not.
    /// </summary>
    private void IsBossLevel(Level selectedLevel, bool edit = false)
    {
        bossToggle = new Toggle("Boss Level?");
        bossToggle.value = selectedLevel.bossLevel;
        bossToggle.SetEnabled(edit);
        bossToggle.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {


            if (evt.newValue)
            {
                BossToSpawn(selectedLevel, true);
            }
            else
            {
                m_RightPane.RemoveAt(4);
            }
        }
        );

        m_RightPane.Add(bossToggle);
    }


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


   
    /// <summary>
    /// Only shows when boss toggle is set to true. The user can decide what boss the the player can face
    /// </summary>
    private void BossToSpawn(Level selectedLevel, bool edit = false)
    {
        bossSelection = new EnumField("Boss", selectedLevel.bossToSpawn);
        bossSelection.value = selectedLevel.bossToSpawn;
        bossSelection.SetEnabled(edit);
        m_RightPane.Insert(4, bossSelection);
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

    /// <tsummary>
    /// Allow me to save changes to currently selected level.
    /// </summary>
    /// <param name="selectedLevel"></param>
    private void SaveButton(Level selectedLevel)
    {
        Action saveLevel = () =>
        {
            string newName = levelName.value;
            string newDescription = levelDescription.value;
            ELevelBackground newBackground = (ELevelBackground)levelBackgroundField.value;
            bool isBoss = bossToggle.value;

            EBossType newBoss = EBossType.None;

            if (isBoss)
            {

                newBoss = (EBossType)bossSelection.value;
            }
            
            List<EnemySetting> newEnemySettings = new List<EnemySetting>();
            newEnemySettings.Add(new EnemySetting(EEnemyType.CroutonShip, croutonShipValue.value));
            newEnemySettings.Add(new EnemySetting(EEnemyType.ColourChangingShip, colourChaningShipValue.value));

         
            // A new updated level to hold the level changes
            Level updatedLevel = new Level(
                    newName,
                    newDescription,
                    isBoss,
                    newEnemySettings,
                    newBoss,
                    newBackground
            );

            // If a level does not initally exist, and this new level to the level list. 
            if (FindLevel(selectedLevel))
            {
                int index = levelManager.levels.FindIndex(level => level.name == selectedLevel.name);
                Debug.Log("Level Index is: " + index);
                levelManager.levels[index] = updatedLevel;
            }
            else
            {
                levelManager.levels.Add(updatedLevel);
            }

            // Refeshing the entire level editor
            PopulateLevelList();
            LoadLevel(updatedLevel, false);

            
        };


        var saveButton = new Button(saveLevel) { text = "Save Level" };
        m_RightPane.Add(saveButton);
    }

    /// <summary>
    /// Make a mistake, just cancel all changes made to the current level so you can start again
    /// </summary>
    private void CancelButton(Level selectedLevel)
    {
        Action cancelEdit = () =>
        {
            if (FindLevel(selectedLevel))
            {
                LoadLevel(selectedLevel, false);
            }
            else
            {
                m_RightPane.Clear();
            }
        };

        var cancelButton = new Button(cancelEdit) { text = "Cancel Edit" };
        m_RightPane.Add(cancelButton);
    }

    /// <summary>
    /// Deletes the current level, and clears the right pane
    /// </summary>
    private void DeleteLevel(Level selectedLevel)
    {
        Action deleteLevel = () =>
        {
            // If the level exists then remove it from the level list
            if (FindLevel(selectedLevel))
            {
                levelManager.levels.Remove(selectedLevel);          
            }
            m_RightPane.Clear();
            PopulateLevelList();
        };

        var deleteButton = new Button(deleteLevel) { text = "Delete Level" };
        m_RightPane.Add(deleteButton);
    }

    /// <summary>
    /// Creates a brand new level, that I can change depending on what I want
    /// </summary>
    private void NewLevelButton()
    {

        Action newLevel = () =>
        {

            // Create a basic level with the base constructor that the user can edit
            Level newLevel = new Level(new List<EnemySetting> { new EnemySetting(EEnemyType.CroutonShip, 0), new EnemySetting(EEnemyType.ColourChangingShip, 0) });

            // Load this new level into the right pane
            LoadLevel(newLevel, true);
        };

        var AddNewLevel = new Button(newLevel) { text = "Create New Level! " };
        rootVisualElement.Add(AddNewLevel);
    }

    /// <summary>
    /// Find if the Selected Level exists or not
    /// </summary>
    private bool FindLevel(Level selectedLevel)
    {
        List<Level> levels = levelManager.levels;
        return levels.Contains(selectedLevel);
    }
}