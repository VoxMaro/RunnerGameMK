using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerGameplayFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject m_playerPrefab = null;
    
    [SerializeField] Camera m_levelCamera = null;
    [SerializeField] Button buttonToStart = null;

    GameObject playerObject;

    float gameScore;

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
        playerObject = Instantiate(m_playerPrefab, Vector3.zero, Quaternion.identity);
        playerObject.GetComponentInChildren<RunnerPlayerController>().SetGameplayFunctions(this);
        // m_playerPrefab
        //((Camera)m_playerPrefab.GetComponentInChildren(typeof(Camera))).;
        SetState(GameState.Running);
        m_levelCamera.enabled = false;
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameScore += Time.deltaTime * 100;
    }

    public void ScorePickup()
    {

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
