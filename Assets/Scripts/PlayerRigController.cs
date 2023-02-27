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
        pauseMenuHolder.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, Time.deltaTime * pauseMenuRotationSpeed);
        //pauseMenuHolder.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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
}
