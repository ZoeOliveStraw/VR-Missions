using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScene : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private Transform playerStart;
    [SerializeField] private Transform menuHolder;

    private void Start()
    {
        SetPlayerTransform();
        MovePlayerToSTart();
        GameManager.instance.GetPlayerRig().GetComponent<PlayerRigController>().ActivateRayInteractors();
    }

    private void SetPlayerTransform()
    {
        if (!GameManager.instance)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            playerTransform = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        RotateMenu();
    }

    private void MovePlayerToSTart()
    {
        if (playerStart != null)
        {
            StartCoroutine(MovePlayer());
        }
    }

    private IEnumerator MovePlayer()
    {
        GameObject playerRig = GameManager.instance.GetPlayerRig();
        Time.timeScale = 0;
        playerRig.transform.position = playerStart.position;
        yield return new WaitForEndOfFrame();
        Time.timeScale = 1;
    }

    private void RotateMenu()
    {
        Quaternion cameraRotation = Camera.main.transform.rotation;
        Vector3 cameraRotationEuler = cameraRotation.eulerAngles;
        cameraRotationEuler = new Vector3(0,cameraRotationEuler.y,0);
        
        menuHolder.rotation = Quaternion.Euler(cameraRotationEuler);
    }

    public void RestartLevel()
    {
        GameManager.instance.LoadPreviousLevel();
    }

    public void LoadMainMenu()
    {
        GameManager.instance.LoadSceneByName("Main Menu");
    }
}
