using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnInteract : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 targetPosition; 
    public float moveSpeed = 5f;  

    private Vector3 startPosition;
    private bool isMoving = false; 
    private bool movingToTarget = true; 

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // If the object is moving, move it towards the appropriate position
        if (isMoving)
        {
            MoveToPosition(movingToTarget ? targetPosition : startPosition);
        }
    }

   
    public void StartMoving()
    {
      
        if (!isMoving)
        {
            isMoving = true;
            movingToTarget = !movingToTarget; 
        }
    }

    private void MoveToPosition(Vector3 destination)
    {
       
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

      
        if (Vector3.Distance(transform.position, destination) <= 0.01f)
        {
            isMoving = false;
        }
    }
}