using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An instance of this will be placed in every level to manage things unique to that level
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; //static instance of the player to be its position
    private Transform playerTransform;
    private Vector3 playerPosision;
    
    
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
    
    //Reference to the player position that can be grabbed from the static instance anywhere in the level to save on
    //cycles
    public Vector3 GetPlayerPosition()
    {
        return playerPosision;
    }
}
