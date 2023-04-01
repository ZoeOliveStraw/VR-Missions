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
    private bool hasBeenCompleted = false;

    private void Start()
    {
        Debug.Log("Deactivating canvas");
        menu.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if(!hasBeenCompleted)
        {
            menu.SetActive(true);
            levelTime.text = "Time: ????";
            GameManager.instance.GetPlayerRig().GetComponent<PlayerRigController>().ActivateRayInteractors();
            hasBeenCompleted = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GetPlayerRig().GetComponent<PlayerRigController>().DeactivateRayInteractors();
        }
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
