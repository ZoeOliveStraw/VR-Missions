using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class DemoMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    private DemoInput input;
    private Vector2 currentMoveVector;
    


    private void Awake()
    {
        input = new DemoInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (input != null)
        {
            currentMoveVector = input.Movement.Move.ReadValue<Vector2>();
        }

        Vector3 newPosition = new Vector3(currentMoveVector.x * moveSpeed * Time.deltaTime, 0,
            currentMoveVector.y * moveSpeed * Time.deltaTime);
        transform.position += newPosition;

        transform.LookAt( transform.position + newPosition);
    }
    
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
