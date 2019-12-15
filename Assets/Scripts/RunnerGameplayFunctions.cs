using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerGameplayFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject playerPrefab = null;
    
    [SerializeField] Camera levelCamera = null;
    [SerializeField] Button buttonToStart = null;
    [SerializeField] Button replayButton = null;
    [SerializeField] TMPro.TextMeshProUGUI scoreText = null;


    GameObject playerObject;

    int gameScore;
    float gameTimeElapsed;

    public enum GameState
    {
        Menu,
        Running,
        Dead
    } 
    GameState m_GameplayState;

    public void StartGame()
    {
        buttonToStart.gameObject.SetActive(false);
        playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerObject.GetComponentInChildren<RunnerPlayerController>().SetGameplayFunctions(this);
        // m_playerPrefab
        //((Camera)m_playerPrefab.GetComponentInChildren(typeof(Camera))).;
        SetState(GameState.Running);
        levelCamera.enabled = false;


        //testing, remove asap
       // QualitySettings.vSyncCount = 0;  // VSync must be disabled
       // Application.targetFrameRate = 20;


    }

    public void RestartGame()
    {
        replayButton.gameObject.SetActive(false);
        playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerObject.GetComponentInChildren<RunnerPlayerController>().SetGameplayFunctions(this);
        
        levelCamera.enabled = false;
        gameTimeElapsed = 0f;
        gameScore = 0;
        SetState(GameState.Running);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTimeElapsed += Time.deltaTime;
        switch (m_GameplayState)
        {
            case GameState.Menu:

                break;
            case GameState.Running:
                break;
            case GameState.Dead:
                replayButton.gameObject.SetActive(true);
                levelCamera.enabled = true;
                Destroy(playerObject);
                break;
            default:

                break;
        }
       
    }

    private void FixedUpdate()
    {
        switch (m_GameplayState)
        {
            case GameState.Menu:

                break;
            case GameState.Running:
                gameScore += (int)(10 * (1 + (gameTimeElapsed / 300)));
                scoreText.text = "Score: " + gameScore.ToString();
                break;
            default:
                break;
        }
    }

    public void ScorePickup()
    {
        gameScore += (int)(1000 * (1 + (gameTimeElapsed / 300)));
    }


    public GameState GetState()
    {
        return m_GameplayState;
    }
    public void SetState(GameState state)
    {
        m_GameplayState = state;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerObject.transform.position;
    }
}
