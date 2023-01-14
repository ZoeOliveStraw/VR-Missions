using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// GameManager will be persistent across scenes and handle functions and information at the highest level
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private string currentSceneName;

    [SerializeField] private GameObject playerRig;
    
    void Start()
    {
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
    }

    public void MovePlayerRig(Vector3 location)
    {
        playerRig.transform.position = location;
    }
}
