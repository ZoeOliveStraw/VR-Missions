using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An instance of this will be placed in every level to manage things unique to that level
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private float alertLength;
    private float timeSincePlayerSeen;
    private bool alertActive;
    
    public static LevelManager instance; //static instance of the player to be its position
    private Transform playerTransform;
    private Vector3 playerPosision;
    private Vector3 lastKnownPlayerPosition;
    
    //References for the event system
    public delegate void TriggerAlertState();

    public static event TriggerAlertState AlertTriggered;
    
    public delegate void TriggerPatrolState();

    public static event TriggerPatrolState PatrolTriggered;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set the instance of the level manager
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (alertActive)
        {
            if (Vector3.Distance(playerPosision, lastKnownPlayerPosition) < 0.2f)
            {
                timeSincePlayerSeen = 0;
            }
            else
            {
                timeSincePlayerSeen += Time.deltaTime;
            }

            if (timeSincePlayerSeen >= alertLength)
            {
                LevelWidePatrol();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerTransform) //Keep checking for a reference to the player's tag until it is set
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        else
        {
            //Setting the player position here to avoid having to get the reference in a bunch of places
            playerPosision = playerTransform.position;
        }
    }

    public Vector3 GetLastKnownPlayerPosition()
    {
        return lastKnownPlayerPosition;
    }

    public void SetLastKnownPlayerPosition()
    {
        lastKnownPlayerPosition = playerPosision;
    }
    
    //Reference to the player position that can be grabbed from the static instance anywhere in the level to save on
    //cycles
    public Vector3 GetPlayerPosition()
    {
        return playerPosision;
    }

    public void LevelWideAlert()
    {
        if (AlertTriggered != null)
        {
            alertActive = true;
            AlertTriggered();
        }
    }

    public void LevelWidePatrol()
    {
        if (PatrolTriggered != null)
        {
            alertActive = false;
            PatrolTriggered();
        }
    }
}
