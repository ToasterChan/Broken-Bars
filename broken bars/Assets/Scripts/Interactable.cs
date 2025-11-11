using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Vector3 boxCastSize = new Vector3(2f, 2f, 2f);
    public Vector3 boxCastOffset = Vector3.zero;
    public LayerMask playerLayer;
    public KeyCode interactKey = KeyCode.E;

    [Header("Events")]
    public UnityEvent onInteract;

    private bool playerInRange = false;

    void Update()
    {
        
        Collider[] hits = Physics.OverlapBox(transform.position + boxCastOffset, boxCastSize * 0.5f, Quaternion.identity, playerLayer);
        playerInRange = false;
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerInRange = true;
                break;
            }
        }

       
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            onInteract?.Invoke();
        }
    }

   
    public void TestInteract()
    {
        Debug.Log("Interactable: TestInteract called!");
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxCastOffset, boxCastSize);
    }
}
