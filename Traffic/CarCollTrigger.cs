using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollTrigger : MonoBehaviour
{
    public bool ObstacleAhead;
    public bool RedAhead;
    public bool ThisCarReachedDes;
    public float SpeedAhead = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
      

        //obstacles
        if (IsAheadObstacle(other))
        {
            ObstacleAhead = true;
        }

        //car ahead logic
        MatchSpeedAhead(other);

        if (IsCarAheadReachedDes(other))
        {
            ObstacleAhead = false;
        }


        //traffic light
        if (other.TryGetComponent(out TrafficLight trafficLight))
        {

            if (trafficLight.IsRed)
            {
                RedAhead = true;

            }
            else { RedAhead = false; }
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if ( IsAheadObstacle(other))
        {
            ObstacleAhead = false;
        }
    }

    //obstacles bool
    private bool IsAheadObstacle(Collider other)
    {
        if (
            other.TryGetComponent(out Player player)
            || other.TryGetComponent(out Car car)
            )
        {
            return true;
        }
        else return false;
    }

    private void MatchSpeedAhead(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            SpeedAhead = car.CarSpeed;
        }
    }


    //avoid trigger exit car dissapear bug
    private bool IsCarAheadReachedDes (Collider other)
    {
        if(other.TryGetComponent(out Car car))
        {
            return car.ReachedDes;
        }
        else
        {
            return false;
        }
    }
}
