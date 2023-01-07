using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;

//This script will assist the CharacterControllerDriver in adjusting the height and position
//of the character controller

public class ControllerDriverHelper : MonoBehaviour
{
    private XROrigin origin;
    private CharacterController controller;
    private CharacterControllerDriver driver;
    
    private void Start()
    {
        origin = GetComponent<XROrigin>();
        controller = GetComponent<CharacterController>();
        driver = GetComponent<CharacterControllerDriver>();
    }
    
    
    private void Update()
    {
        UpdateCharacterController();
    }

    protected virtual void UpdateCharacterController()
    {
        if (origin == null || controller == null)
            return;

        var height = Mathf.Clamp(origin.CameraInOriginSpaceHeight, driver.minHeight, driver.maxHeight);

        Vector3 center = origin.CameraInOriginSpacePos;
        center.y = height / 2f + controller.skinWidth;

        controller.height = height;
        controller.center = center;
    }
}
