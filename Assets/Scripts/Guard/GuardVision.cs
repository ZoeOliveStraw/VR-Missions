using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// By Zoe Straw
/// This class will determine whether or not a guard can see the player, and how much they should be alerted
/// --CanSeePlayer is a bool that determines if the player is within a vision cone represented by the max distance and
/// inside the max horizontal and vertical angles
/// --GetAmountPlayerIsVisible() is designed to return a simple float that can be used to build the GuardAI script's
/// alertness amount
/// </summary>
public class GuardVision : MonoBehaviour
{
    private Vector3 _playerPosition;
    
    [SerializeField] private float maxPlayerDistaince = 5;
    [SerializeField] private float maxHorizontalAngle;
    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private Transform eyes;
    
    private LevelManager mgr;

    public bool canSeePlayer;
    private float currentDistanceToPlayer;

    private void Start()
    {
        mgr = LevelManager.instance;
    }

    private void FixedUpdate()
    {
        if (!mgr)
        {
            mgr = LevelManager.instance;
        }
        else
        {
            GetPlayerPositionAndDistance();
        }

        GetAmountPlayerIsVisible();
        SetCanSeePlayer();
        Debug.DrawLine(transform.position, _playerPosition, canSeePlayer ?Color.red : Color.green, Time.fixedDeltaTime);
    }
    
    /// <summary>
    /// Determines if the player is visible based on the guard's vision range (max horizontal and vertical angle) and raycasts
    /// </summary>
    private void SetCanSeePlayer()
    {
        //Horizontal angle to player calculation
        Vector3 playerHorizontalPosition = new Vector3(_playerPosition.x,0,_playerPosition.z);
        Vector3 myHorizontalPosition = new Vector3(eyes.position.x, 0, eyes.position.z);
        Vector3 angleToPlayer = playerHorizontalPosition - myHorizontalPosition;
        //Vector3 angleToPlayer = transform.position - _playerPosition;
        float horizontalAngle = Vector3.Angle(eyes.forward, angleToPlayer);
        
        //Vertical angle to player calculation
        float horizontalDistanceToPlayer = angleToPlayer.magnitude;
        float heightToPlayer = _playerPosition.y - eyes.position.y;
        heightToPlayer = Math.Abs(heightToPlayer);
        Vector2 heightAngle = new Vector2(horizontalDistanceToPlayer, heightToPlayer);
        float verticalAngle = Vector2.Angle(heightAngle, Vector2.right);

        //If the player is within our vision range we raycast to her and see if we can see her
        if (verticalAngle < maxVerticalAngle
            && horizontalAngle < maxHorizontalAngle
            && currentDistanceToPlayer < maxPlayerDistaince)
        {
            Vector3 trueAngleToPlayer = _playerPosition - eyes.position;
            RaycastHit hit;
            Ray playerDetectionRay = new Ray(eyes.position, trueAngleToPlayer);
            Debug.DrawRay(eyes.position, trueAngleToPlayer);

            if (Physics.Raycast(playerDetectionRay, out hit, currentDistanceToPlayer))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else
        {
            canSeePlayer = false;
        }
    }
    
    /// <summary>
    /// Return a float based on how much the player can currently be seen
    /// </summary>
    /// <returns></returns>
    public float GetAmountPlayerIsVisible()
    {
        if (canSeePlayer)
        {
            float portionOfDistance = (maxPlayerDistaince - currentDistanceToPlayer) / maxPlayerDistaince;
            return portionOfDistance;
        }
        else
        {
            return 0;
        }
    }
    

    private void GetPlayerPositionAndDistance()
    {
        _playerPosition = mgr.GetPlayerPosition();
        currentDistanceToPlayer = Vector3.Distance(transform.position, _playerPosition);
    }
}
