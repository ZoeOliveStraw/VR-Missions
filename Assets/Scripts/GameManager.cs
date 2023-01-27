using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


/// <summary>
/// GameManager will be persistent across scenes and handle functions and information at the highest level
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private string currentSceneName;
    private bool paused = false;
    
    private PlayerRigController playerRigController;
    
    [SerializeField] private GameObject playerRig;

    [SerializeField] private InputActionReference pauseAction;

    private void Awake()
    {
        pauseAction.action.performed += ctx => TogglePause();
    }

    void Start()
    {
        var netReach = Application.internetReachability; //This line prevents the app from crashing. No really.
        playerRigController = playerRig.GetComponent<PlayerRigController>();
        
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        
        LoadSceneByName("Main Menu");
    }

    public void LoadSceneByName(string sceneName)
    {
        if(currentSceneName != null) SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentSceneName = sceneName;
        playerRigController.SetPauseState(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void MovePlayerRig(Vector3 location)
    {
        playerRig.transform.position = location;
    }

    public void TogglePause()
    {
        if(currentSceneName != "Main Menu")
        {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            playerRigController.SetPauseState(paused);
        }
    }

    public GameObject GetPlayerRig()
    {
        return playerRig;
    }
}
