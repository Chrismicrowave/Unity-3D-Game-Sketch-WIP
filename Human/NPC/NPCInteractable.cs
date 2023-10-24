using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public Dialogue _dialogue;
    

    private void Awake()
    {
        
    }

    private void Update()
    {
       
    }


   

    public virtual void Interact()
    {
        
        DialogueHandle(_dialogue);
    }


    //Dialogue content Handled here

    
    //2.Camera and Dialogue Logic Handled by <DialogueManager>
    //3.Go to "Menu Type NPC" Menu after Dialogue Handled by <DialogueManager>

    public void DialogueHandle(Dialogue Dialogue)
    {
        
        if (!PlayerState.IsPlayerInDialogue)
        {
            
            
            DialogueManager.Instance.StartDialogue(Dialogue);
        }
        else if (PlayerState.IsPlayerInDialogue && !DialogueManager.DialogLastSen)
        {
            
            DialogueManager.Instance.NextSentence();
        }
    }


}

