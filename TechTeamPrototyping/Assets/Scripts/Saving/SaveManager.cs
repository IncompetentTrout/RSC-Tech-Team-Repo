using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {

    #region Variables
    
    [SerializeField] private GameObject[] checkPoints;
    
    private GameObject _player, _startPos; 
    private PlayerSettings _saveData = new();

    private int _currentCheckPoint;
    
    #endregion

    #region Unity Methods
    
    private void Awake() {
        _startPos = gameObject.transform.GetChild(0).gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        LoadFromJson();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            _saveData = new PlayerSettings();
            SaveToJson();
            Debug.Log("SaveReset");
        }
    }
    
    #endregion

    #region Methods

    private void SaveToJson() {
        var savDat = JsonUtility.ToJson(_saveData);
        var filePath = Application.persistentDataPath + "/PlayerData.json";
        File.WriteAllText(filePath, savDat);
    }
    
    private void LoadFromJson() {
        var filepath = Path.Combine(Application.persistentDataPath, "PlayerData.json");

        if (File.Exists(filepath)) {
            var saveDataJson = File.ReadAllText(filepath);
            _saveData = JsonUtility.FromJson<PlayerSettings>(saveDataJson);

            Vector3 targetPosition = _saveData.isNewGame ? _startPos.transform.position : checkPoints[_saveData.lastSavedPosition].transform.position;

            _player.transform.position = targetPosition;

        } else {
            _player.transform.position = _startPos.transform.position; // Default position if no save file exists
        }

        Debug.Log("{Save Manager} Save Loaded Successfully");
    }
    
    public void HitCheckPoint(GameObject checkPoint) {
        for (var i = 0; i < checkPoints.Length; i++)
            if (checkPoints[i].gameObject == checkPoint) {
                Debug.Log(checkPoint);
                Debug.Log(checkPoints[i]);
                Debug.Log(_saveData);
                _saveData.lastSavedPosition = i;
                _saveData.isNewGame = false;

                SaveToJson();
            }
    }
    #endregion
}

public class PlayerSettings {
    public bool isNewGame = true;
    public int lastSavedPosition;
    public int playerHealth;
}
