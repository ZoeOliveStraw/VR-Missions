using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This script will handle logic for the objective within a level
/// </summary>
public class LevelGoal : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private GameObject menu;
    [SerializeField] private TextMeshProUGUI levelTime;
    [SerializeField] private string nextLevelName;

    private void Start()
    {
        menu.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        menu.SetActive(true);
        CompleteLevel();
    }

    private void CompleteLevel()
    {
        Time.timeScale = 0;
        levelTime.text = "Time: ????";
        GameManager.instance.GetPlayerRig().GetComponent<PlayerRigController>().ActivateRayInteractors();
    }

    public void ReturnToMainMenu()
    {
        GameManager.instance.LoadSceneByName("Main Menu");
    }

    public void LoadNextLevel()
    {
        GameManager.instance.LoadSceneByName(nextLevelName);
    }
    
    public void RestartLevel()
    {
        GameManager.instance.ReloadCurrentLevel();
    }
}
