using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] GameObject _water;
    [SerializeField] Transform _objPosition;
    [SerializeField] float ThrowDelay;
    [SerializeField] float ObjLifeTime = 3f;
    [SerializeField] Transform player;

    //throw speed angle
    [SerializeField] float _initialSpeed = 2f; 
    [SerializeField] float _angle = 45f;

    float _initialVelocity;
    float _initialVelocityX;
    float _initialVelocityY;

    // Start is called before the first frame update
    void Start()
    {
        //_initialVelocity = _initialSpeed;
        //_initialVelocityX = _initialVelocity * Mathf.Cos(_angle * Mathf.Deg2Rad);
        //_initialVelocityY = _initialVelocity * Mathf.Sin(_angle * Mathf.Deg2Rad);

    }

    // Update is called once per frame
    void Update()
    {
        Throw();
        
    }

    private void Throw()
    {
        if (PlayerInputManager.Instance.ActionWasPressedOnce())
        {
            //start anim
            PlayerState.IsPlayerWaterSplash = true;

            //instantiate after anim play time
            StartCoroutine(ThrowCor());
        }
    }

    IEnumerator ThrowCor()
    {
        yield return new WaitForSeconds(ThrowDelay);

        GameObject water = Instantiate(_water, _objPosition.position, Quaternion.identity);
        Rigidbody waterRB = water.GetComponent<Rigidbody>();

        // Calculate the sign based on the player's forward direction in the x-axis
        float sign = Mathf.Sign(player.forward.x);

        // Calculate the initial velocity components based on the angle and player's forward direction
        float angleInRadians = _angle * Mathf.Deg2Rad;
        float cosAngle = Mathf.Cos(angleInRadians);
        float sinAngle = Mathf.Sin(angleInRadians);
        _initialVelocityX = sign * _initialSpeed * cosAngle;
        _initialVelocityY = _initialSpeed * sinAngle;

        waterRB.velocity = new Vector3(_initialVelocityX, _initialVelocityY, 0f);

        Destroy(water, ObjLifeTime);
    }


    
}
