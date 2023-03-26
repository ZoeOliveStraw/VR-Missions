using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRigController : MonoBehaviour
{
    [SerializeField] private GameObject lhGrabObject;
    [SerializeField] private GameObject lhRayObject;
    
    [SerializeField] private GameObject rhGrabObject;
    [SerializeField] private GameObject rhRayObject;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Transform pauseMenuHolder;
    [SerializeField] private float pauseMenuRotationSpeed;

    [SerializeField] private TextMeshProUGUI menuText;

    public bool isGameOver = false; //Is the game over state triggered

    // Start is called before the first frame update
    void Start()
    {
        SetMenuState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotatePauseMenu();
    }
    
    //Rotate the pause menu around the Y axis so it can keep up with the player
    private void RotatePauseMenu()
    {
        var cam = Camera.main.transform;
        Quaternion cameraRotation = cam.rotation;
        Vector3 cameraRotationEuler = cameraRotation.eulerAngles;
        cameraRotationEuler = new Vector3(0,cameraRotationEuler.y,0);
        
        pauseMenuHolder.rotation = Quaternion.Euler(cameraRotationEuler);
        pauseMenuHolder.position = cam.position;
    }

    public void SetPauseState(bool paused)
    {

        if (isGameOver && !paused) return;

        pauseMenu.SetActive(paused);

        menuText.text = isGameOver ? "GAME OVER" : "PAUSED";
    
        lhGrabObject.SetActive(!paused);
        rhGrabObject.SetActive(!paused);
    
        lhRayObject.SetActive(paused);
        rhRayObject.SetActive(paused);
        
    }

    public void SetMenuState()
    {
        pauseMenu.SetActive(false);
        ActivateRayInteractors();
    }
    
    public void ReturnToMainMenu()
    {
        GameManager.instance.LoadSceneByName("Main Menu");
        SetMenuState();
    }
    
    public void RestartLevel()
    {
        GameManager.instance.ReloadCurrentLevel();
        SetMenuState();
    }

    public void ActivateRayInteractors()
    {
        lhGrabObject.SetActive(false);
        rhGrabObject.SetActive(false);
        
        lhRayObject.SetActive(true);
        rhRayObject.SetActive(true);
    }
    
    public void DeactivateRayInteractors()
    {
        lhGrabObject.SetActive(true);
        rhGrabObject.SetActive(true);
        
        lhRayObject.SetActive(false);
        rhRayObject.SetActive(false);
    }
}
