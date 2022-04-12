using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class GameData
    {
        public string currentPlayerName = "";
        public int highestScore = 0;
        public string highestScoreHolder = "";
    }

    public static GameManager Instance;
    public GameData gameData = new GameData();
    public Text bestScoreText;
    public InputField nameInputField;
    void Awake()
    {
        if (Instance == null)
        {   
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
        LoadData();
        bestScoreText.text = $"Best Score: {gameData.highestScore} - {gameData.highestScoreHolder}";
        nameInputField.text = gameData.currentPlayerName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SetCurrentPlayerName(nameInputField.text);
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Exit();
#endif
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/gamedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
    }

    private void SaveData()
    {
        string path = Application.persistentDataPath + "/gamedata.json";
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(path, json);
    }

    public void SetNewHighestScore(string playerName, int points)
    {
        gameData.highestScoreHolder = playerName;
        gameData.highestScore = points;
        SaveData();
    }

    private void SetCurrentPlayerName(string currentPlayerName)
    {
        gameData.currentPlayerName = currentPlayerName;
        SaveData();
    }
}
