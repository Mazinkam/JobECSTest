using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class PlanetSpawner
{
    static EntityManager planetManager;
    static MeshInstanceRenderer planetRenderer;
    static EntityArchetype planetArchtype;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        planetManager = World.Active.GetOrCreateManager<EntityManager>();
        planetArchtype = planetManager.CreateArchetype(typeof(Position),
                                                        typeof(Heading),
                                                        typeof(MoveForward),
                                                        typeof(LocalToWorld),
                                                        typeof(MoveSpeed));
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitWithScene()
    {
        planetRenderer = GameObject.FindObjectOfType<MeshInstanceRendererComponent>().Value;
        for(int i = 0; i < 40000; i++)
        {
            SpawnPlanet();
        }
    }

}
