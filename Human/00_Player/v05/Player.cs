using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    void Start()
    {
      
    }

    void Update()
    {
        
        //Debug.Log("state: " + PlayerState.CurState);
        //Debug.Log(PlayerMovement.CurMove.y);


        //speed and rotation 
        PlayerMovement.Instance.SpeedHandle();
        PlayerMovement.Instance.RotHandle();

        //state
        PlayerState.Instance.StateHandle();


        //PlayerAnim
        PlayerAnim.Instance.AnimHandle();

        //PlayerAnim Audio
        PlayerAudio.Instance.AnimAudio();

        //Actions
        PlayerActions.Instance.Actions();

        //Apply Gravity
        PlayerGravity.Instance.GravityHandle();

        //And Move relative to camera
        PlayerMovement.Instance.Move();
    }


    private void OnTriggerStay(Collider coll)
    {
        
        PlayerInteraction.Instance.ItemPickup(coll);
        PlayerInteraction.Instance.NPCTalk(coll);
    }


}
