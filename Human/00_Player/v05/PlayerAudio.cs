using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio Instance;

    private string _lastStateAu;

    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Audio
    public void AnimAudio()
    {
        string _curState = PlayerState.CurState;

        if (_lastStateAu == _curState)
        { return; }

        switch (_curState)
        {
            case "isIdle":

                AnimEffect.Instance.StopAudio();

                if (_lastStateAu == "isJump" || _lastStateAu == "isFall")
                { AnimEffect.Instance.IsLandedAudio(); }

                _lastStateAu = _curState;
                break;

            case "isRun":

                if (_lastStateAu == "isJump" || _lastStateAu == "isFall")
                {
                    StartCoroutine(LandedThenRun());//Landed sound play first
                    _lastStateAu = _curState;
                    break;
                }
                else
                {
                    AnimEffect.Instance.IsRunAudio();
                    _lastStateAu = _curState;
                    break;
                }


            case "isWalk":

                if (_lastStateAu == "isJump" || _lastStateAu == "isFall")
                {
                    StartCoroutine(LandedThenWalk());//Landed sound play first
                    _lastStateAu = _curState;
                    break;
                }
                else
                {
                    AnimEffect.Instance.IsWalkAudio();
                    _lastStateAu = _curState;
                    break;
                }

            case "isJump":
                _lastStateAu = _curState;
                break;

            case "isFallStep":
                _lastStateAu = _curState;
                break;

            case "isFall":
                AnimEffect.Instance.StopAudio();
                _lastStateAu = _curState;
                break;

        }
    }


    IEnumerator LandedThenRun()
    {
        AnimEffect.Instance.IsLandedAudio();
        yield return new WaitForSeconds(.3f);

        AnimEffect.Instance.IsRunAudio();
    }

    IEnumerator LandedThenWalk()
    {
        AnimEffect.Instance.IsLandedAudio();
        yield return new WaitForSeconds(.3f);

        AnimEffect.Instance.IsWalkAudio();
    }
}
