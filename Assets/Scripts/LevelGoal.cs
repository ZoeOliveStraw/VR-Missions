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

    private void OnTriggerEnter(Collider other)
    {
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
}
