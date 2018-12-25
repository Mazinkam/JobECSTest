using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitNoJob : MonoBehaviour
{
    public GameObject sun;
    GameObject[] planets;
    public int numPlanets = 50;

	// Use this for initialization
	void Start ()
    {
        planets = new GameObject[numPlanets];
        for (int i = 0; i < numPlanets; i++)
        {
            planets[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planets[i].transform.position = sun.transform.position + Random.insideUnitSphere * 50;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < numPlanets; i++)
        {
            Vector3 direction = sun.transform.position - planets[i].transform.position;
            float gravity = Mathf.Clamp(direction.magnitude / 100.0f, 0, 1);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            planets[i].transform.rotation = Quaternion.Slerp(planets[i].transform.rotation, lookRotation, gravity);

            float orbitalSpeed = Mathf.Sqrt(50 / direction.magnitude);
            planets[i].transform.position += planets[i].transform.rotation * Vector3.forward * orbitalSpeed;
        }
	}
}
