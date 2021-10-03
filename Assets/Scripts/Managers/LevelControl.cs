using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelControl : MonoBehaviour
{

    [Header("Settings")]
    public string levelMusic;

    [Header("Game States")]
    public bool paused;
    public bool inInventory;
    public bool inDialogue;
    public bool inDialogueMini;
    public bool inDatingGame;
    public string levelName;

    [Header("GameObjects")]
    public GameObject player;
    //private PlayerController playerController;

    [Header("Inventory-Related Values")]
    public int selectedMain;
    public int selectedSlot;
    public bool discarding;


    private void Awake()
    {
        var folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        var save0Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save0");
        var save1Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save1");
        var save2Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save2");
        var save3Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save3");
    }

    void Start()
    {
        player = GameObject.Find("Player");
        /*
        playerController = player.GetComponent<PlayerController>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        taskList = GetComponentInChildren<TaskList>();
        objectCheckTasks = FindObjectsOfType<ObjectCheckTask>();
        interactableObjects = FindObjectsOfType<Interactable>();
        */

        if(SoundManager.Instance.currentMusicPlaying != levelMusic && !string.IsNullOrEmpty(levelMusic))
        {
            SoundManager.Instance.FadeAndPlayMusic(levelMusic, 1f);
        }

        /*
        if (SaveGameManager.Instance.freshSave)
        {
            Invoke("SetStatesForObjectCheckTasks", 0.03f);
        }
        else
        {
            NonFreshLoad();
        }

        if (GameManager.Instance.pauseUI == null)
        {
            GameManager.Instance.pauseUI = Resources.FindObjectsOfTypeAll<PauseUI>()[0].gameObject;
        }
        */
    }

    public void NonFreshLoad()
    {
        //LoadPlayer();
        Invoke("LoadScene", 0.01f);
        Invoke("SaveAllGameData", 0.02f);
        Invoke("SetStatesForObjectCheckTasks", 0.03f);
        Invoke("UpdateStatesForEnvironmentalObjects", 0.04f);
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            //SaveAllGameData(); 
            print("DEBUG: GAME SAVED");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadScene();
            LoadPlayer();
            print("LOADED PLAYER");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGameSaveData();
            print("RESETTING GAME DATA");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SetTaskList();
            print("SETTING TASK LIST");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetAllTasksAsCompleted();
            print("SETTING ALL TASKS AS COMPLETE");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            ResetAllTasks();
            print("RESETTING ALL TASKS");    
        }

        */
    }

    /*
    public void LoadFromAnotherScene()
    {
        if (!(SaveSystem.LoadPlayer(SaveGameManager.Instance.saveIndex) == null))
        {

            PlayerData data = SaveSystem.LoadPlayer(SaveGameManager.Instance.saveIndex);

            SaveGameManager.Instance.fileSaveName = data.saveName;

            player.GetComponent<PlayerBehavior>().health = data.health;

            for (int i = 0; i < inventoryManager.inventoryItemsList.Count; i++)
            {
                inventoryManager.inventoryItemsList[i] = data.invent[i];
            }
        }

    }

    public void LoadPlayer()
    {
        if(!(SaveSystem.LoadPlayer(SaveGameManager.Instance.saveIndex) == null))
        {
            PlayerData data = SaveSystem.LoadPlayer(SaveGameManager.Instance.saveIndex);

            SaveGameManager.Instance.fileSaveName = data.saveName;

            player.GetComponent<PlayerBehavior>().health = data.health;
  
            for (int i = 0; i < inventoryManager.inventoryItemsList.Count; i++)
            {
                inventoryManager.inventoryItemsList[i] = data.invent[i];
            }

            TaskManager.Instance.LoadTasks(data.taskList);
            taskList.SetTasksListOnLoad();

            //Used only to show player data in the editor.
            PlayerDataExample dataExample = FindObjectOfType<PlayerDataExample>();
            if(dataExample != null)
            {
                dataExample.LoadPlayerExample(data);
            }
            
        }
    }

    public void LoadScene()
    {
        if(SaveSystem.LoadScene(SaveGameManager.Instance.saveIndex, player.GetComponent<PlayerBehavior>().currentSceneIndex) != null)
        {
            SceneData data = SaveSystem.LoadScene(SaveGameManager.Instance.saveIndex, playerController.currentSceneIndex);
          
            SaveGameManager.Instance.savedObjectAmount = data.savedObjectAmount;
            SaveGameManager.Instance.savedObjects = data.savedObjects;
            SaveGameManager.Instance.LoadSaveableObjects();

            //Used only to show scene data in the editor.
            SceneDataExample dataExample = FindObjectOfType<SceneDataExample>();
            if (dataExample != null)
            {
                dataExample.LoadSceneExample(data);
            }
        }
    }

    public void SaveAllGameData()
    {
        SaveGameManager.Instance.freshSave = false;
        ObjectiveManager.Instance.SaveObjectives();
        SaveSystem.SavePlayer(InventoryManager.Instance, playerController, TaskManager.Instance, SaveGameManager.Instance.saveIndex);
        SaveGameManager.Instance.SaveSaveableObjects();
        SaveSystem.SaveScene(playerController, SaveGameManager.Instance.saveIndex);
    }

    public void ResetGameSaveData()
    {
        taskList.ResetAllTasks();

        Interactable[] interactables = FindObjectsOfType<Interactable>();

        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].ResetStateToStart();
        }

        for (int i = 0; i < inventoryManager.inventoryItemsList.Count; i++)
        {
            inventoryManager.inventoryItemsList[i] = "";
        }

        SaveAllGameData();
    }

    public void SetTaskList()
    {
        taskList.SetTasksAsComplete();
        SaveAllGameData();
    }

    public void SetAllTasksAsCompleted()
    {
        taskList.SetAllTasksAsComplete();
        SaveAllGameData();
    }

    public void ResetAllTasks()
    {
        taskList.ResetAllTasks();
        SaveAllGameData();
    }

    public void SetStatesForObjectCheckTasks()
    {
        for (int i = 0; i < objectCheckTasks.Length; i++)
        {
            objectCheckTasks[i].ChangeStateBasedOnTasksCompleted();
        }
    }

    public void UpdateStatesForEnvironmentalObjects()
    {
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            interactableObjects[i].UpdateState();
        }
    }

    */
}
