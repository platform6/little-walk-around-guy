using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    private bool hasInteracted = false;
    public Sprite newSprite;
    public AudioClip interactionSound;
    public DialogueTrigger dialogueTrigger;

    [SerializeField]
    private bool kill;

    public void Update()
    {
        base.Update();
        if (IsPlayerNearby() && Input.GetKeyDown(KeyCode.E) && !hasInteracted)
        {
            
            Interact();
        }
    }

    private void Interact()
    {
        hasInteracted = true;
        Debug.Log("Interacting with " + name);

        // Change sprite if applicable
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }

        // Play sound if applicable
        AudioSource audioSource = GetComponent<AudioSource>();
        if (interactionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(interactionSound);
        }

        // Trigger dialogue
        if (dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();
        }
    }
}