using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Array of waypoints to walk from one to the next one
    // public Transform[] Waypoints;

    
    [SerializeField]
    public float CarSpeed = 4f;
    [SerializeField]
    public float AccRate = 0.1f;
    [SerializeField]
    private float StopRate = 2f;

    [SerializeField]
    GameObject Mesh;


    private float _moveSpeed;
    [SerializeField] private Transform[] _waypointsRuntime;
    // Index of current waypoint from which Enemy walks
    // to the next one
    public int waypointIndex = 0;


    public bool WaypointHasSetup;
    public bool IsStarting;
    public bool IsStoping;
    bool IsObjHit;

    public bool ReachedDes;

    
    [SerializeField]
    private CarCollTrigger CollTrigger;
    [SerializeField]
    GameObject SpeechBubble;


    // Use this for initialization
    private void Start()
    {
        CarSpeed = Random.Range(CarSpeed - 1 , CarSpeed + 1);

        

        ////set up in <CarSpwan> instantiate
        ///
        //SetupWaypointsRuntime(Waypoints);
        //StartCoroutine(CarMoveCO());


        if (WaypointHasSetup)
        {
            StartCoroutine(CarMoveCO());
        }

        //quick fix for bug
        //Destroy(gameObject, 20f);
    }


    private void FixedUpdate()
    {

    }


    private IEnumerator CarMoveCO()
    {
        Animator _anim = Mesh.GetComponent<Animator>();

        while (true)
        {
            yield return null;

            //stop for redlight or obstacles
            if (!CollTrigger.RedAhead && !CollTrigger.ObstacleAhead && !IsObjHit)
            {
                if (_moveSpeed!= CarSpeed)
                {
                    StartSpeed();
                    _anim.Play("Running");
                }
            }
            else 
            {
                if (_moveSpeed != 0 )
                {
                    StopSpeed();
                    _anim.Play("Idle");
                }
            }

            //Move with relative speed
            Move();

        }
    }

    


    private void Move()
    {
        

        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= _waypointsRuntime.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
               _waypointsRuntime[waypointIndex].transform.position,
               _moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == _waypointsRuntime[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
        else
        {
            ReachedDes = true;
            //Destroy(this.gameObject);
            StartCoroutine(DelayedDestroy());
            TrafficManager.CurCarNum -= 1;
        }
    }


    private void StopSpeed()
    {
        _moveSpeed -= StopRate;

        if (_moveSpeed <= 0)
        {
            _moveSpeed = 0;
        }
    }

    private void StartSpeed()
    {
        _moveSpeed += AccRate + Random.Range(AccRate * -.5f, AccRate * 2);

        if (CollTrigger.SpeedAhead != 0 && CarSpeed > CollTrigger.SpeedAhead)
        {
            if (_moveSpeed >= CollTrigger.SpeedAhead)
            {
                _moveSpeed = CollTrigger.SpeedAhead;
            }
        }
        else
        {
            if (_moveSpeed >= CarSpeed)
            {
                _moveSpeed = CarSpeed;
            }
        }
        
        

        
    }

    //loop thru waypoints inputs, add current postion to waypointRuntime[0], then inputs
    public void SetupWaypointsRuntime(Transform[] WaypointsIn)
    {
        _waypointsRuntime = new Transform[WaypointsIn.Length + 1];
        _waypointsRuntime[0] = transform;

        for (int i = 0; i < WaypointsIn.Length; i++)
        {
            _waypointsRuntime[i + 1] = WaypointsIn[i];
        }

        WaypointHasSetup = true;
    }

    //private void OnCollisionEnter(Collision coll)
    //{
    //    ReactToObjThrow(coll);

    //}

    ////if is hit by obj
    //void ReactToObjThrow(Collision coll)
    //{
    //    if (coll.collider.TryGetComponent(out ObjThrow obj))
    //    {
    //        StartCoroutine(Speech());
    //    }
    //}

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out ObjThrow obj))
        {
            StartCoroutine(Speech());
        }
    }

    

    IEnumerator Speech()
    {
        IsObjHit = true;
        SpeechBubble.SetActive(true);

        yield return new WaitForSeconds(2f);

        IsObjHit = false;
        SpeechBubble.SetActive(false);
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForFixedUpdate();
        Destroy(this.gameObject);
    }
}
