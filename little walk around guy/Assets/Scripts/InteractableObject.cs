using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    private bool z_Interacted = false;
    public Sprite newSprite;
    public AudioClip interactionSound;
    [SerializeField]
    private bool kill;

    protected override void OnCollided(GameObject gameObject)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }
    private void OnInteract()
    {
        Debug.Log("interact with " + name);

        if (!z_Interacted)
        {
            z_Interacted=true;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }

            if (interactionSound != null && audioSource != null) // Play the sound if the audio clip and AudioSource are set
            {
                audioSource.PlayOneShot(interactionSound);
            }

            if (kill) {

                Destroy(gameObject, 4f);

            };
        }
    }
}
