using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    private Collider2D z_Collider;
    public List<Collider2D> nearbyColliders = new List<Collider2D>();

    public void Start()
    {
        z_Collider = GetComponent<Collider2D>();
    }

    public void Update()
    {
        // Clear the list of nearby colliders
        nearbyColliders.Clear();
        

        // Fill the list with colliders currently overlapping with this object
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        z_Collider.OverlapCollider(filter, nearbyColliders);
        
    }

    public bool IsPlayerNearby()
    {
        foreach (var collider in nearbyColliders)
        {
            if (collider.CompareTag("Player")) // Assuming the player has a tag "Player"
            {
                // UnityEngine.Debug.Log("Player is nearby the object: " + gameObject.name);
                return true;
            }
        }
        
        return false;
    }
}