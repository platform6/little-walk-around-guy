using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    public float typingSpeed = 0.07f;

    public TMP_Text nameText;
    public TMP_Text dialougeText;
    public Animator animator;
    private Queue<string> sentences;
    public bool isOpen = false;

    public NPCInteractionTracker interactionTracker;
    private NPC currentNPC;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        // Debug.Log(isOpen.ToString());
        if (isOpen && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
        }
    }


    public void StartDialogue(Dialogue dialogue, NPC npc)
    {
        currentNPC = npc; // Store the NPC reference



        animator.SetBool("isOpen", true);
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }



    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();

        dialougeText.text = sentence;
        isOpen = true;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));


    }

    IEnumerator TypeSentence (string sentence)
    {
        dialougeText.text = "";

            foreach (char letter in sentence.ToCharArray())
            {
                dialougeText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        isOpen = false;


        // Register the interaction
        if (interactionTracker != null && currentNPC != null)
        {
            Debug.Log("I am registering " + currentNPC.npcId);
            interactionTracker.RegisterNPCInteraction(currentNPC.npcId);
        }

        currentNPC = null;

    }

}

