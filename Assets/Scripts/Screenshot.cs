using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    private Debugging _debugging;

    void Awake()
    {
        _debugging = new Debugging();
        _debugging.Debug.Screenshot.performed += ctx => TakeScreenshot();
    }

    private void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot("PosterScreen.png");
        Debug.Log("Screenshot taken!");
    }

    private void OnEnable()
    {
        _debugging.Enable();
    }

    private void OnDisable()
    {
        _debugging.Disable();
    }
}
