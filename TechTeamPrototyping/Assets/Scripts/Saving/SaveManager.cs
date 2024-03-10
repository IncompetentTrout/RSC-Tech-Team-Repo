using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {
    [SerializeField] private GameObject[] checkPoints; //list of checkpoints in the scene

    private GameObject
        player, //reference to the player
        startPos; //first child of GameManager, where the player spawns when they have no save data/saved checkpoints

    private PlayerSettings saveData = new(); //creates a saveData type initialised to default settings

    private int currentCheckPoint; //current checkPoint the player is at in the level

    private void Awake() {
        startPos = gameObject.transform.GetChild(0).gameObject; //gets the startPos from child
        player = GameObject.FindGameObjectWithTag("Player"); //gets a reference to the player in the scene 
        LoadFromJson(); //runs the load function to check if there is save data and to set the player's position
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) //if the player wants to load the save
            SceneManager.LoadScene(SceneManager.GetActiveScene()
                .buildIndex); //reload the scene to re-run the "loadFromJson()" in awake

        if (Input.GetKeyDown(KeyCode.C)) //if the player wants to clear a save
        {
            saveData = new PlayerSettings(); //overwrites the saveData variable with a new PlayerSettings class
            SaveToJson(); //overwrites the .json save data with the new saveData
            Debug.Log("SaveReset");
        }
    }

    private void SaveToJson() //when the call is made to save the game
    {
        var savDat = JsonUtility.ToJson(saveData); //saves the saveData class as a json formatted string
        var filePath =
            Application.persistentDataPath +
            "/PlayerData.json"; //creates/overwrites a PlayerData.json file (simplified explaination)
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, savDat); //writes the data to the file
        Debug.Log("Saved Successfully");
    }

    private void LoadFromJson() //when the call is made to load the game
    {
        var filepath = Application.persistentDataPath + "/PlayerData.json"; //gets the filepath to the save data file
        var savDat = System.IO.File.ReadAllText(filepath); //takes the file data into a string

        if (JsonUtility.FromJson<PlayerSettings>(savDat) != null) //if the .json isnt null
        {
            saveData = JsonUtility
                .FromJson<
                    PlayerSettings>(savDat); //converts from .json to PlayerSettings class and saves in in the saveData
            if (!saveData.isNewGame) //if it is not a new game/cleared save
                player.transform.position =
                    checkPoints[saveData.lastSavedPosition].transform
                        .position; //sets the players position to the last checkpoint
            else //if a cleared save/new game
                player.transform.position = startPos.transform.position; //sends player to the start position
        }
        else //if the .json is null/doesnt exist (brand new game, no saveData whatsoever)
        {
            player.transform.position = startPos.transform.position; //sends the player to the start position
        }

        Debug.Log("Save Loaded Successfully");
    }

    public void HitCheckPoint(GameObject checkPoint) //when the player hits a checkpoint this function is called
    {
        for (var i = 0;
             i < checkPoints.Length;
             i++) //uses a for loop to find what checkpoint was hit (so we can use the one we can use the index)
            if (checkPoints[i].gameObject == checkPoint) {
                Debug.Log(checkPoint);
                Debug.Log(checkPoints[i]);
                Debug.Log(saveData);
                saveData.lastSavedPosition = i; //set to the index that is equal to the checkpoint that was hit
                saveData.isNewGame = false; //if the player hit a checkPoint, its no longer considered a new game

                SaveToJson(); //saves the game
            }
    }
}

public class
    PlayerSettings //class that contains the data for the player, needs to be stored as a publically available class outside of this script
{
    public int lastSavedPosition; //index of the checkpoint the player hit
    public int playerHealth; //not used but here in case player health is needed as an example of what can be saved
    public bool isNewGame = true; //initialised to true so that fresh saveData is always considered a new game.
}