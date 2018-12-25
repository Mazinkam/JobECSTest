using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class OrbitWithJob : MonoBehaviour
{
    public GameObject sun;
    GameObject[] planets;
    public int numPlanets = 50;

    Transform[] planetTransforms;
    TransformAccessArray planetTransfgormsAccessArray;
    PositionUpdateJob planetJob;
    JobHandle planetPositionJobHandle;

    struct PositionUpdateJob : IJobParallelForTransform
    {
        public Vector3 sunPos;

        public void Execute(int i, TransformAccess transform)
        {
            Vector3 direction = sunPos - transform.position;
            float gravity = Mathf.Clamp(direction.magnitude / 100.0f, 0, 1);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, gravity);

            float orbitalSpeed = Mathf.Sqrt(50 / direction.magnitude);
            transform.position += transform.rotation * Vector3.forward  * orbitalSpeed;
        }
    }

    // Use this for initialization
    void Start()
    {
        planets = new GameObject[numPlanets];
        planetTransforms = new Transform[numPlanets];

        for (int i = 0; i < numPlanets; i++)
        {
            planets[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planets[i].transform.position = sun.transform.position + Random.insideUnitSphere * 50;
            planetTransforms[i] = planets[i].transform;
        }

        planetTransfgormsAccessArray = new TransformAccessArray(planetTransforms);
    }

    // Update is called once per frame
    void Update()
    {
        planetJob = new PositionUpdateJob()
        {
            sunPos = sun.transform.position
        };

        planetPositionJobHandle = planetJob.Schedule(planetTransfgormsAccessArray);
    }

    private void LateUpdate()
    {
        planetPositionJobHandle.Complete();
    }

    private void OnDestroy()
    {
        planetTransfgormsAccessArray.Dispose();
    }
}
