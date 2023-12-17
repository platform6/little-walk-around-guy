using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractionTracker : MonoBehaviour
{
    private HashSet<int> uniqueNpcInteractions = new HashSet<int>();
    private const int RequiredInteractions = 3;

    public void RegisterNPCInteraction(int npcId)

    {
        if (uniqueNpcInteractions.Add(npcId) && uniqueNpcInteractions.Count >= RequiredInteractions)
        {
            LoadNewScene();
        }
        Debug.Log("I have spoken to  " + uniqueNpcInteractions.Count.ToString() + " of " + RequiredInteractions.ToString() + " npcs");
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene("close_menu");
    }
}
