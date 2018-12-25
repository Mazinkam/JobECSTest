using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct Heading : IComponentData
{
    public float3 Value;

    public Heading(float3 heading)
    {
        Value = heading;
    }
}

[DisallowMultipleComponent]
public class HeadingComponent : ComponentDataWrapper<Heading> { }


/// <summary>
/// Store float speed. This component requests that if another component is moving the PositionComponent
/// it should respect this value and move the position at the constant speed specified.
/// </summary>
[Serializable]
public struct MoveSpeed : IComponentData
{
    public float speed;
}

[UnityEngine.DisallowMultipleComponent]
public class MoveSpeedComponent : ComponentDataWrapper<MoveSpeed> { }


public struct MoveForward : ISharedComponentData { }

public class MoveForwardComponent : SharedComponentDataWrapper<MoveForward> { }