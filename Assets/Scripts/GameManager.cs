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
    [SerializeField] private Renderer shade;
    [SerializeField] private float fadeTime;

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
        StartCoroutine(FadeScene(sceneName));
    }

    private IEnumerator FadeScene(string sceneName)
    {
        Time.timeScale = 1;
        Material material = shade.material;
        //Fade scene out
        float alpha = 0;
        while (alpha < 1)
        {
            material.color = new Color(0, 0, 0, alpha);
            alpha += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        alpha = 1;

        //Scene loading logic
        if(currentSceneName != null) SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentSceneName = sceneName;
        playerRigController.SetPauseState(false);
        paused = false;

        if (sceneName.Equals("Main Menu"))
        {
            playerRigController.ActivateRayInteractors();
        }
        
        //Fade scene in
        while (alpha > 0)
        {
            material.color = new Color(0, 0, 0, alpha);
            alpha -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        alpha = 0;
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
