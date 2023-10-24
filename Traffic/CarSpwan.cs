using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpwan : MonoBehaviour
{
    [SerializeField] private GameObject[] CarPrefabs;
    [SerializeField] private Transform[] Waypoints;

    [Header("Car Facing Angle")]
    [SerializeField] private float Angle;

    void Start()
    {
        StartCoroutine(SpawnEnum());
    }

    private void SpawnCar()
    {
        if (TrafficManager.CurCarNum < TrafficManager.TotalCarNum)
        {
            int randInt = Random.Range(0, CarPrefabs.Length);
            GameObject car = Instantiate(CarPrefabs[randInt], transform.position, Quaternion.identity);

            // Apply the rotation based on the selected angle
            car.transform.rotation = Quaternion.Euler(0f, Angle, 0f);

            Car carData = car.GetComponent<Car>();
            carData.SetupWaypointsRuntime(Waypoints);

            TrafficManager.CurCarNum += 1;
        }
    }

    IEnumerator SpawnEnum()
    {
        while (true)
        {
            SpawnCar();
            yield return new WaitForSeconds(Random.Range(1.5f, 4f));
        }
    }
}
