using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public static PlayerAnim Instance;

    [SerializeField] public StringValue CurWeapon;
    Animator _animator;
    private string _lastState;

    private void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }

        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //Animation
    public void AnimHandle()
    {
        //base state -> curstate
        string _curState = PlayerState.CurState;

        //check if it is holding item state
        if (CurWeapon.RuntimeValue != "")
        {_curState = _curState + "Itm";}


        //actions animation
        StartCoroutine(WaterSplashAnim());
        //StopCoroutine(WaterSplashAnim());

        //play animation
        if (_lastState == _curState) { return; }


        if (_curState == "isFallStep" || _curState == "isFallStepItm")
        {
            _animator.Play(_lastState);
        }
        else
        {
            _animator.Play(_curState);
        }

        _lastState = _curState;

    }

    IEnumerator WaterSplashAnim()
    {
        if (PlayerState.IsPlayerWaterSplash)
        {
            _animator.Play("isWaterSplash");
            yield return new WaitForSeconds(.3f);
            _lastState = "";
            PlayerState.IsPlayerWaterSplash = false;
        }
    }
}
