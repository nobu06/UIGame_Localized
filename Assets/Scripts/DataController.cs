using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour
{
    public static DataController instance;

    private RoundData[] allRoundData;

    private PlayerProgress playerProgress;

    private bool isReady = false;

    /** 
     * turning this obj into a singleton
     */ 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    

    void Start()
    {
        LoadPlayerProgress();       // load highest score
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundData[0];
    }

    // submit a new score from the GameController
    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    /**
     * Loads the game data in the language selected
     */
    public void LoadGameData(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            // read all the text into a string
            string dataAsJson = File.ReadAllText(filePath);

            // deserialize it into a game data obj
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }

        isReady = true;

    }

    public bool GetIsReady()
    {
        return isReady;
    }

}



//using UnityEngine;
//using System.Collections;
//using UnityEngine.SceneManagement;
//using System.IO;

//public class DataController : MonoBehaviour 
//{
//	private RoundData[] allRoundData;

//    private PlayerProgress playerProgress;

//    //private string gameDataFileName = "data.json";



//	void Start ()  
//	{
//		DontDestroyOnLoad (gameObject);
//        LoadGameData();
//        LoadPlayerProgress();

//		SceneManager.LoadScene ("MenuScreen");
//	}
	
//	public RoundData GetCurrentRoundData()
//	{
//		return allRoundData [0];
//	}

//    // submit a new score from the GameController
//    public void SubmitNewPlayerScore(int newScore)
//    {
//        if (newScore > playerProgress.highestScore)
//        {
//            playerProgress.highestScore = newScore;
//            SavePlayerProgress();
//        }
//    }

//    public int GetHighestPlayerScore()
//    {
//        return playerProgress.highestScore;
//    }

//    private void LoadPlayerProgress()
//    {
//        playerProgress = new PlayerProgress();

//        if (PlayerPrefs.HasKey("highestScore"))
//        {
//            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
//        }
//    }

//    private void SavePlayerProgress()
//    {
//        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
//    }

//    /**
//     * Loads the game data in the language selected
//     */ 
//    private void LoadGameData(string fileName)
//    {

//        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

//        if (File.Exists(filePath))
//        {
//            // read all the text into a string
//            string dataAsJson = File.ReadAllText(filePath);

//            // deserialize it into a game data obj
//            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

//            allRoundData = loadedData.allRoundData;
//        }
//        else
//        {
//            Debug.LogError("Cannot load game data!");
//        }

//    }

//    //private void LoadGameData()
//    //{
//    //    string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

//    //    if (File.Exists(filePath))
//    //    {
//    //        // read all the text into a string
//    //        string dataAsJson = File.ReadAllText(filePath);

//    //        // deserialize it into a game data obj
//    //        GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

//    //        allRoundData = loadedData.allRoundData;
//    //    }
//    //    else
//    //    {
//    //        Debug.LogError("Cannot load game data!");
//    //    }
//    //}


//}