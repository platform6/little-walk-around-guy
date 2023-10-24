using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    private bool z_Interacted = false;
    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {
        if (!z_Interacted)
        {
            z_Interacted=true;
            Debug.Log("interact with " + name);
        }
    }
}
