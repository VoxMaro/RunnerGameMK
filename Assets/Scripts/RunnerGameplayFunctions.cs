using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunnerGameplayFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject m_playerPrefab;
    [SerializeField] Camera m_levelCamera;
    [SerializeField] Button buttonToStart;
    [SerializeField] Canvas m_guiCanvas;
    bool started = false;
    //Add state management
    


    public void StartGame()
    {
        buttonToStart.gameObject.SetActive(false);
        Instantiate(m_playerPrefab, Vector3.zero, Quaternion.identity);
        // m_playerPrefab
        //((Camera)m_playerPrefab.GetComponentInChildren(typeof(Camera))).;
        m_levelCamera.enabled = false;
        started = true;
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GameStarted()
    {
        return started;
    }
}
