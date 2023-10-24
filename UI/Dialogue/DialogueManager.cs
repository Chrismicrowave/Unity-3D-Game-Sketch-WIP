using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public static bool DialogLastSen;
   
    public TMP_Text _nameText;
    public TMP_Text _dialogueText;

    [SerializeField] private float _textSpeed = .01f;

    private Queue<string> _sentences;



    void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _sentences = new Queue<string>();
    }

    private void Update()
    {
        

    }


    //Camera and Dialogue Logic handled here

    public void StartDialogue(Dialogue Dialogue)
    {
        //set name, player state
        DialogLastSen = false;

        _nameText.text = Dialogue.NPCName;
        PlayerInDialogue();

        //camera
        CameraManager.Instance.SwitchCamera(CameraManager.Instance.CloseUp);

        //start new sentences from dialogue
        _sentences.Clear();
        foreach (string sen in Dialogue.Sentences){_sentences.Enqueue(sen);}

        NextSentence();
    }


    public void NextSentence()
    {
        if(_sentences.Count == 0)
        {
            //go to MenuType NPC's menu, if not end dialogue
            GoToNPCMenu();

        }


        if (!DialogLastSen)
        {
            string sen = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sen));
        }
        else { return; }
    }


    //MenuType NPC contine to Menu
    public void GoToNPCMenu()
    {
        //more state to be integrated later, bool logic handled by PlayerState
        if (PlayerState.IsPlayerInBuy)
        {

            DialogLastSen = true;
            return;
        }
        else
        {
            
            EndDialogue();
            return;
        }
        
    }


    IEnumerator TypeSentence(string Sen)
    {
        _dialogueText.text = "";
        foreach (char letter in Sen.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
    }


    public void EndDialogue()
    {
        CameraManager.Instance.SwitchCamera(CameraManager.Instance.TopDown);
        PlayerExitDialogue();
    }



    //dialogue state
    public void PlayerInDialogue()
    {
        PlayerState.IsPlayerInDialogue = true;
    }

    public void PlayerExitDialogue()
    {
        PlayerState.IsPlayerInDialogue = false;
    }
}
